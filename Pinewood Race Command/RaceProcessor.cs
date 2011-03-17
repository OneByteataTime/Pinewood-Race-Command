using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Pinewood_Race_Command
{
    public class RaceProcessor
    {
        /// <summary>
        /// Race Complete Event
        /// </summary>
        /// <returns></returns>
        public delegate void OnRaceCompleteHandler();
        public event OnRaceCompleteHandler OnRaceComplete;

        private System.Media.SoundPlayer _wavePlayer;

        private string _appDataPath;

        private Race _activeRace;
        private TournamentRace _activeTournamentRace;

        private TrackCommunication _trackCommunication;
        private bool _isInitialized;


        public RaceProcessor()
        {
            // Store a reference to the Common application data folder
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            // Append our Application name
            _appDataPath = Path.Combine(appData, "Pinewood Race Command");

            // Create our application folder if it does not exist
            if (Directory.Exists(_appDataPath).Equals(false))
            {
                Directory.CreateDirectory(_appDataPath);
            }

            // Build the path to our backups
            string backupPath = Path.Combine(_appDataPath, "Backups");
            if (Directory.Exists(backupPath).Equals(false))
            {
                Directory.CreateDirectory(backupPath);
            }

            // Initialize our Heat number to 1
            RaceDataStore.PrelimHeatNumber = 1;

            // Create our media player
            _wavePlayer = new System.Media.SoundPlayer();

            _isInitialized = false; 

        }

        #region Properties
        
        public string DataStorePath
        {
            get { return _appDataPath; }
        }

        #endregion

        public void SerializeRacersToDisk(string filename)
        {
            // Build the path to our Application Data 
            string racerFilename = Path.Combine(_appDataPath, string.Concat(filename, ".xml"));

            // Default our File Mode to truncate
            FileMode fMode = FileMode.Truncate;

            // Do we have this file?
            if (File.Exists(racerFilename).Equals(false))
            {
                // Override our File Mode to create
                fMode = FileMode.Create;
            }

            // Create an Xml Serializer
            XmlSerializer xmlSaver = new XmlSerializer(typeof(List<Racer>));

            using (FileStream fs = new FileStream(racerFilename, fMode))
            {
                // Serialize our Racer data to disk
                xmlSaver.Serialize(fs, RaceDataStore.RacerList);
            }

            // Store our (possibly new) Race Name
            RaceDataStore.LoadedRaceName = Path.GetFileNameWithoutExtension(filename);
        }

        public void DeserializeRacersToObject(string filename)
        {
            XmlSerializer xmlLoader = new XmlSerializer(typeof(List<Racer>));

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                RaceDataStore.RacerList = xmlLoader.Deserialize(fs) as List<Racer>;

                // Store the name of the Loaded race
                RaceDataStore.LoadedRaceName = Path.GetFileNameWithoutExtension(filename);
            }
        }

        public void BackupRaceData()
        {
            // Build the path to our backups
            string backupFolder = Path.Combine(_appDataPath, "Backups");

           
            // Build the path to our Application Data 
            string backupFilename = Path.Combine(backupFolder, "Race_Backup.xml");

            // Default our File Mode to truncate
            FileMode fMode = FileMode.Truncate;

            // Do we have this file?
            if (File.Exists(backupFilename).Equals(false))
            {
                // Override our File Mode to create
                fMode = FileMode.Create;
            }

            // Create an Xml Serializer
            XmlSerializer xmlSaver = new XmlSerializer(typeof(List<Racer>));

            using (FileStream fs = new FileStream(backupFilename, fMode))
            {
                // Serialize our Racer data to disk
                xmlSaver.Serialize(fs, RaceDataStore.RacerList);
            }
        }

        public void LoadNextHeat()
        {
            switch (RaceDataStore.PrelimHeatNumber)
            {
                case 1:
                    LoadHeat1();
                    break;

                case 2:
                    LoadHeat2();
                    break;

                case 3:
                    LoadHeat3();
                    break;

                case 4:
                    LoadHeat4();
                    break;
            }
        }

        private void LoadHeat1()
        {
            // Mark the Heat as active
            RaceDataStore.PrelimHeatCompleted = false;

            // Load our lane stacks
            this.LoadLaneStacks(RaceDataStore.RacerList);
        }

        private void LoadHeat2()
        {
            // Mark the Heat as active
            RaceDataStore.PrelimHeatCompleted = false;

            // Clear out our active race
            _activeRace = null;
            
            // Sort the racers by last name
            List<Racer> racerList = RaceDataStore.RacerList;
            racerList.Sort(delegate(Racer racer1, Racer racer2)
            {
                return Comparer<string>.Default.Compare(racer1.LastName, racer2.LastName);
            });

            // Load our Lane stacks
            this.LoadLaneStacks(racerList);
        }

        private void LoadHeat3()
        {
            // Mark the Heat as active
            RaceDataStore.PrelimHeatCompleted = false;

            // Clear out our active race
            _activeRace = null;

            // Sort the racers by last name
            List<Racer> racerList = RaceDataStore.RacerList;
            racerList.Sort(delegate(Racer racer1, Racer racer2)
            {
                return Comparer<string>.Default.Compare(racer1.FirstName, racer2.FirstName);
            });

            // Load our Lane stacks
            this.LoadLaneStacks(racerList);
        }

        private void LoadHeat4()
        {
            // Mark the Heat as active
            RaceDataStore.PrelimHeatCompleted = false;

            // Clear out our active race
            _activeRace = null;

            // Sort the racers by last name
            List<Racer> racerList = RaceDataStore.RacerList;
            racerList.Sort(delegate(Racer racer1, Racer racer2)
            {
                return Comparer<string>.Default.Compare(racer1.Heats[0].Time.ToString(), racer2.Heats[0].Time.ToString());
            });

            // Load our Lane stacks
            this.LoadLaneStacks(racerList);
        }

        private void LoadLaneStacks(List<Racer> racerList)
        {
            // Our heat index will be 1 less than the heat number
            // because our array is zero based
            int activeHeat = RaceDataStore.PrelimHeatNumber - 1;

            // Allocate a Stack that we will assign to the appropriate 
            // lane stack
            Queue<Racer> laneStack = null;

            // Iterate over the users and assign the appropriate lane stack
            foreach (Racer racer in racerList)
            {
                switch (racer.Heats[activeHeat].Lane)
                {
                    case 1:
                        laneStack = RaceDataStore.LanePool.Lane1Stack;
                        break;

                    case 2:
                        laneStack = RaceDataStore.LanePool.Lane2Stack;
                        break;

                    case 3:
                        laneStack = RaceDataStore.LanePool.Lane3Stack;
                        break;

                    case 4:
                        laneStack = RaceDataStore.LanePool.Lane4Stack;
                        break;
                }

                // Push this racer onto the stack
                laneStack.Enqueue(racer);

                // Record the race number for display purposes
                racer.RaceInfo.AssignedRaceCode = String.Format("Prelim - Heat {0}:Race {1}", activeHeat, laneStack.Count);
                racer.RaceInfo.Number = laneStack.Count;
            }
        }

        public Race GetNextRace()
        {
            // Is the Heat completed?
            if (RaceDataStore.PrelimHeatCompleted)
            {
                throw new NoMoreRacersException();
            }

            // Create a new Race object
            Race nextRace = new Race();

            // Do we have an active Race?
            if (_activeRace == null)
            {
                // Assign a new active race object
                _activeRace = new Race();

                // Load the on deck racers
                _activeRace.OnDeckLane1 = RaceDataStore.LanePool.Lane1Stack.Dequeue();
                _activeRace.OnDeckLane2 = RaceDataStore.LanePool.Lane2Stack.Dequeue();
                _activeRace.OnDeckLane3 = RaceDataStore.LanePool.Lane3Stack.Dequeue();
                _activeRace.OnDeckLane4 = RaceDataStore.LanePool.Lane4Stack.Dequeue();
            }

            // Copy the current on deck racers to the lanes
            nextRace.Lane1Racer = _activeRace.OnDeckLane1;
            nextRace.Lane2Racer = _activeRace.OnDeckLane2;
            nextRace.Lane3Racer = _activeRace.OnDeckLane3;
            nextRace.Lane4Racer = _activeRace.OnDeckLane4;

            // Do we have any more racers?
            if (RaceDataStore.LanePool.Lane1Stack.Count > 0)
            {
                // Get the next racers from our lane pool queues
                nextRace.OnDeckLane1 = RaceDataStore.LanePool.Lane1Stack.Dequeue();
                nextRace.OnDeckLane2 = RaceDataStore.LanePool.Lane2Stack.Dequeue();
                nextRace.OnDeckLane3 = RaceDataStore.LanePool.Lane3Stack.Dequeue();
                nextRace.OnDeckLane4 = RaceDataStore.LanePool.Lane4Stack.Dequeue();
            }
            else
            {
                // Build a default racer object
                Racer tempRacer = new Racer(string.Empty, string.Empty);
                tempRacer.PinewoodCar.Number = "last race of heat";

                nextRace.OnDeckLane1 = nextRace.OnDeckLane2 = nextRace.OnDeckLane3 = nextRace.OnDeckLane4 = null;

                // There are no more racers so mark the Heat as complete
                RaceDataStore.PrelimHeatCompleted = true;
            }

            // Assign this to our Active Race member variable
            _activeRace = nextRace;

            return nextRace;
        }

        public TournamentRace GetNextTournamentRace()
        {
            TournamentRace tournyRace = null;

            if (RaceDataStore.RaceKeyQueue.Count > 0)
            {
                string key = RaceDataStore.RaceKeyQueue.Dequeue();

                // Get the next race to run from the Queue
                tournyRace = RaceDataStore.TournamentRaces[key];

                tournyRace.Key = key;
            }

            return tournyRace;
        }

        public void RunRace(Race activeRace)
        {
            // Play our Rev Start wave
            _wavePlayer.SoundLocation = "StartRev.wav";
            _wavePlayer.PlaySync();

            // Are we in emulate mode?
            if (RaceDataStore.EmulateRace)
            {
                RunEmulatedRace(activeRace);
            }
            else
            {
                // Store a reference to the active Race
                _activeRace = activeRace;

                // Make sure the tournament race object is null
                _activeTournamentRace = null;

                _trackCommunication.StartRace(); 
            }

            _wavePlayer.SoundLocation = "EndRev.wav";
            _wavePlayer.PlaySync();
        }

        public void RunTournamentRace(TournamentRace activeRace)
        {
            // Store a reference to the tournament race
            _activeTournamentRace = activeRace;

            // Make sure the active race object is null
            _activeRace = null;

            // Play our Rev Start wave
            _wavePlayer.SoundLocation = "StartRev.wav";
            _wavePlayer.PlaySync();

            if (RaceDataStore.EmulateRace)
            {
                Random randomTimeGenerator = new Random();

                double time1 = randomTimeGenerator.NextDouble();
                double time2 = randomTimeGenerator.NextDouble();

                time1 += randomTimeGenerator.Next(3, 6);
                time2 += randomTimeGenerator.Next(3, 6);

                activeRace.Racer1.RaceTime = time1;
                activeRace.Racer2.RaceTime = time2;

                if (time1 < time2)
                {
                    activeRace.Racer1.RecordRaceResult(true);
                    activeRace.Racer2.RecordRaceResult(false);
                }
                else
                {
                    activeRace.Racer1.RecordRaceResult(false);
                    activeRace.Racer2.RecordRaceResult(true);
                }

                this.OnRaceComplete();

                //// Advance the winner to the next round
                //this.AdvanceWinner(activeRace);
            }
            else
            {
                _trackCommunication.StartRace();
            }

            // Play the tire wave
            _wavePlayer.SoundLocation = "EndRev.wav";
            _wavePlayer.PlaySync();
        }

        public bool AdvanceWinner(TournamentRace activeRace)
        {
            bool moreRaces = true;

            string winnerKey = activeRace.WinnerKey;
            
            Sweet16Racer winningRacer = activeRace.Racer1.IsEliminated == false ? activeRace.Racer1 : activeRace.Racer2;

            if (winnerKey == string.Empty)
            {
                System.Windows.Forms.MessageBox.Show(winningRacer.Racer.GetScoreboardDisplay(), "We have a winner!");

                moreRaces = false;

                return moreRaces;
            }

            // Get a reference to the tournament race mathing the 
            // winnners key
            TournamentRace race = RaceDataStore.TournamentRaces[winnerKey];

            // Is racer1 empty?
            if (race.Racer1 == null)
            {
                race.Racer1 = winningRacer;
            }
            else
            {
                race.Racer2 = winningRacer;
            }

            if (race.Racer1 != null && race.Racer2 != null)
            {
                RaceDataStore.RaceKeyQueue.Enqueue(winnerKey);
            }

            return moreRaces;
        }

        public void InitializeTrack()
        {

            // Do we have overridden track settings?
            if (RaceDataStore.TrackSettings == null)
            {
                RaceDataStore.TrackSettings = new System.IO.Ports.SerialPort();
            }

            // Do we have track settings?
            if (_trackCommunication == null)
            {
                _trackCommunication = new TrackCommunication(RaceDataStore.TrackSettings);

                // Hook into the track's events
                _trackCommunication.OnTrackComplete += new TrackCommunication.OnTrackCompleteHandler(_trackCommunication_OnTrackComplete);
            }

            // Have we opened our serial port
            if (_trackCommunication.SerialPort.IsOpen == false)
            {
                _trackCommunication.OpenSerialPort();
            }
        }


        private void RunEmulatedRace(Race activeRace)
        {
            int activeHeat = RaceDataStore.PrelimHeatNumber - 1;

            Random randomTimeGenerator = new Random();

            double time = randomTimeGenerator.NextDouble();
            activeRace.Lane1Racer.Heats[activeHeat].Time = randomTimeGenerator.Next(3, 6) + time;

            time = randomTimeGenerator.NextDouble();
            activeRace.Lane2Racer.Heats[activeHeat].Time = randomTimeGenerator.Next(3, 6) + time;

            time = randomTimeGenerator.NextDouble();
            activeRace.Lane3Racer.Heats[activeHeat].Time = randomTimeGenerator.Next(3, 6) + time;

            time = randomTimeGenerator.NextDouble();
            activeRace.Lane4Racer.Heats[activeHeat].Time = randomTimeGenerator.Next(3, 6) + time;

            // Raise an event so the Scoreboard form knows we are done
            this.OnRaceComplete();
        }

        /// <summary>
        /// Event fired when the COM Communication with the track is
        /// complete and all times have been parsed.
        /// </summary>
        void _trackCommunication_OnTrackComplete()
        {
            // Process results
            if (_activeRace != null)
            {
                int activeHeat = RaceDataStore.PrelimHeatNumber - 1;

                _activeRace.Lane1Racer.Heats[activeHeat].Time = _trackCommunication.Lane1Time;
                _activeRace.Lane2Racer.Heats[activeHeat].Time = _trackCommunication.Lane2Time;
                _activeRace.Lane3Racer.Heats[activeHeat].Time = _trackCommunication.Lane3Time;
                _activeRace.Lane4Racer.Heats[activeHeat].Time = _trackCommunication.Lane4Time;
            }

            // Process the tournament results
            if (_activeTournamentRace != null)
            {
                double time1 = _trackCommunication.Lane2Time;
                double time2 = _trackCommunication.Lane3Time;

                _activeTournamentRace.Racer1.RaceTime = _trackCommunication.Lane2Time;
                _activeTournamentRace.Racer2.RaceTime = _trackCommunication.Lane3Time;

                if (time1 < time2)
                {
                    _activeTournamentRace.Racer1.RecordRaceResult(true);
                    _activeTournamentRace.Racer2.RecordRaceResult(false);
                }
                else
                {
                    _activeTournamentRace.Racer1.RecordRaceResult(false);
                    _activeTournamentRace.Racer2.RecordRaceResult(true);
                }

                // Advance the winner to the next round
                // this.AdvanceWinner(_activeTournamentRace);
            }

            // Rainse an event back to the form
            this.OnRaceComplete();
        }
    }
}
