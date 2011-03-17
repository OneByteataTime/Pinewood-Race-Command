using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Pinewood_Race_Command
{
    static class RaceDataStore
    {
        private static List<Racer> _racerList;
        private static RaceProcessor _raceProc;
        private static string _loadedRaceName;
        private static LanePool _lanePool;
        private static int _prelimHeatNumber;
        private static bool _prelimHeatCompleted;
        private static int _raceCount;
        private static List<Sweet16Racer> _sweet16Racers;
        private static Dictionary<string, TournamentRace> _tournamentRaces;
        private static Queue<string> _raceKeyQueue;
        private static SerialPort _serialPort;
        private static bool _emulate;

        internal static List<Racer> RacerList
        {
            get { return _racerList; }
            set { _racerList = value; }
        }

        internal static List<Sweet16Racer> Sweet16Racers
        {
            get { return _sweet16Racers; }
            set { _sweet16Racers = value; }
        }

        internal static Dictionary<string, TournamentRace> TournamentRaces
        {
            get { return _tournamentRaces; }
            set { _tournamentRaces = value; }
        }

        internal static Queue<string> RaceKeyQueue
        {
            get { return _raceKeyQueue; }
            set { _raceKeyQueue = value; }
        }

        internal static RaceProcessor RaceProc
        {
            get { return _raceProc; }
            set { _raceProc = value; }
        }

        internal static string LoadedRaceName
        {
            get { return _loadedRaceName; }
            set { _loadedRaceName = value; }
        }

        internal static LanePool LanePool
        {
            get { return _lanePool; }
            set { _lanePool = value; }
        }
        
        internal static int PrelimHeatNumber
        {
            get { return _prelimHeatNumber; }
            set { _prelimHeatNumber = value; }
        }

        internal static bool PrelimHeatCompleted
        {
            get { return _prelimHeatCompleted; }
            set { _prelimHeatCompleted = value; }
        }

        internal static int RaceCount
        {
            get { return _raceCount; }
            set { _raceCount = value; }
        }

        internal static SerialPort TrackSettings
        {
            get { return _serialPort; }
            set { _serialPort = value; }
        }

        internal static bool EmulateRace
        {
            get { return _emulate; }
            set { _emulate = value; }
        }
    }

    public enum RaceStateType
    {
        OnStartingLine,
        Racing,
        ReviewingResults,
        Official
    }
        
    public class Race
    {
        private Racer _lane1Racer;
        private Racer _lane2Racer;
        private Racer _lane3Racer;
        private Racer _lane4Racer;

        private Racer _onDeckLane1Racer;
        private Racer _onDeckLane2Racer;
        private Racer _onDeckLane3Racer;
        private Racer _onDeckLane4Racer;

        private int _winningLane;
        private RaceStateType _raceStateType;

        public Race()
        {

        }

        public RaceStateType RaceState
        {
            get { return _raceStateType; }
            set { _raceStateType = value; }
        }

        public Racer Lane1Racer
        {
            get { return _lane1Racer; }
            set { _lane1Racer = value; }
        }

        public Racer Lane2Racer
        {
            get { return _lane2Racer; }
            set { _lane2Racer = value; }
        }

        public Racer Lane3Racer
        {
            get { return _lane3Racer; }
            set { _lane3Racer = value; }
        }

        public Racer Lane4Racer
        {
            get { return _lane4Racer; }
            set { _lane4Racer = value; }
        }

        public Racer OnDeckLane1
        {
            get { return _onDeckLane1Racer; }
            set { _onDeckLane1Racer = value; }
        }

        public Racer OnDeckLane2
        {
            get { return _onDeckLane2Racer; }
            set { _onDeckLane2Racer = value; }
        }

        public Racer OnDeckLane3
        {
            get { return _onDeckLane3Racer; }
            set { _onDeckLane3Racer = value; }
        }

        public Racer OnDeckLane4
        {
            get { return _onDeckLane4Racer; }
            set { _onDeckLane4Racer = value; }
        }

        public int WinningLane
        {
            get { return _winningLane; }
            set { _winningLane = value; }
        }
    }
}
