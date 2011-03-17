using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;

namespace Pinewood_Race_Command
{
    class TrackCommunication
    {
        /// <summary>
        /// Race Complete Event
        /// </summary>
        /// <returns></returns>
        public delegate void OnTrackCompleteHandler();
        public event OnTrackCompleteHandler OnTrackComplete;

        SerialPort _serialPort;
        private StringBuilder _serialResults;

        private double _lane1Time;
        private double _lane2Time;
        private double _lane3Time;
        private double _lane4Time;
        private int _winningLane;

        public TrackCommunication(SerialPort serialPort)
        {
            _serialPort = new SerialPort();

            // Assign properties from the TestTrack form
            _serialPort.BaudRate = serialPort.BaudRate;
            _serialPort.PortName = serialPort.PortName;

            _serialResults = new StringBuilder();

            _serialPort.ErrorReceived += new SerialErrorReceivedEventHandler(_serialPort_ErrorReceived);
        }

        void _serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            throw new ApplicationException("Error received from track");
        }

        void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.ProcessComDataStream(_serialPort.ReadExisting());
        }

        public double Lane1Time
        {
            get { return _lane1Time; }
        }

        public double Lane2Time
        {
            get { return _lane2Time; }
        }

        public double Lane3Time
        {
            get { return _lane3Time; }
        }

        public double Lane4Time
        {
            get { return _lane4Time; }
        }

        public int WinningLane
        {
            get { return _winningLane; }
        }

        public SerialPort SerialPort
        {
            get { return _serialPort; }
            set { _serialPort = value; }
        }

        public void OpenSerialPort()
        {
            try
            {
                _serialPort.Open();
            }
            catch (IOException ioExp)
            {
                throw new ApplicationException("Problem opening Serial COM Port. Did you mean to emulate a race?", ioExp);
            }
        }

        public void StartRace()
        {
            // Run the race
            this.RunRace();
        }

        private void RunRace()
        {
            // Hook into the return data event handler
            _serialPort.DataReceived +=new SerialDataReceivedEventHandler(_serialPort_DataReceived);

            // Clear out any exiting data
            //this.InitRaceData();
            _serialResults = new StringBuilder();

            // Start the race
            _serialPort.Write("G");
        }

        private void ProcessComDataStream(string input)
        {
            // Append this to our existing serial results
            _serialResults.Append(input);

            string raceResults = _serialResults.ToString();

            // Check to see if we are done?
            if (raceResults.IndexOf("Waiting to Start\r\n") > 0)
            {
                // Unhook our event
                _serialPort.DataReceived -=new SerialDataReceivedEventHandler(_serialPort_DataReceived);

                // Parse our race results
                this.ParseRaceTimes(_serialResults.ToString());

                // Notify the caller we are done
                this.OnTrackComplete();
            }
        }

        private void ParseRaceTimes(string results)
        {
            // Allocate an array to hold our lane results
            string[] laneArray = new string[8];

            // Initialize our Array index to the last item
            int laneArrayIndex = 7;

            // Iterate backward thru the results and extract
            // out the Lane results
            while (results.Length > 0)
            {
                // Find the last occurence of Lane
                int laneIndex = results.LastIndexOf("Lane");

                // Strip off from lane to the end
                laneArray[laneArrayIndex] = results.Substring(laneIndex, results.Length - laneIndex);

                // Remove what we striped off from the results string
                results = results.Remove(laneIndex, results.Length - laneIndex);

                // Decrement our array index
                laneArrayIndex--;
            }

            // Now we have an array of each lane result

            // Initialize our time index to 0
            int timeIndex = 0;

            // Allocate an array of 8 doubles to hold our times
            double[] timeArray = new double[8];

            // Now that we have our lane results in array items, analyze
            // our lane results and find the times
            foreach (string laneResult in laneArray)
            {
                // Do we have an '=' sign?
                if (laneResult.IndexOf("=") > 0)
                {
                    // Split the string 
                    string[] tempArray = laneResult.Split('=');

                    timeArray[timeIndex] = Convert.ToDouble(tempArray[1].Trim());
                }
                else
                {
                    // Assuming 'Did not finish' default to 
                    // 10.5 or some larger time
                    timeArray[timeIndex] = 10.50;
                }

                // Increment our index
                timeIndex++;
            }

            // Set our properties
            _lane1Time = timeArray[0];
            _lane2Time = timeArray[1];
            _lane3Time = timeArray[2];
            _lane4Time = timeArray[3];

            // Find the fastest time
            int fastestLane = 0;
            double fastestTime = 30;

            for (int lane = 0; lane < timeArray.Length; lane++)
            {
                // Is this the fastest time?
                if (timeArray[lane] < fastestTime)
                {
                    fastestLane = lane + 1;
                    fastestTime = timeArray[lane];
                }
            }

            _winningLane = fastestLane;
        }
           
    }
}
