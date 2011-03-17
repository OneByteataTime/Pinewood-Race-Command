using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Pinewood_Race_Command
{
    public partial class FormPrelimRaceScoreboard : Form
    {
        private Race _activeRace;

        public delegate void PrelimCompleteHandler();
        public event PrelimCompleteHandler OnPrelimCompleteEvent;

        public FormPrelimRaceScoreboard()
        {
            InitializeComponent();

            RaceDataStore.RaceProc.OnRaceComplete += new RaceProcessor.OnRaceCompleteHandler(RaceProc_OnRaceComplete);
        }

        private void FormPrelimRaceCommand_Load(object sender, EventArgs e)
        {
            //*******************************************************************
            // NOTE: The first heat was already loaded by the Prelim Race Form
            //*******************************************************************

            // Get the next race
            _activeRace = RaceDataStore.RaceProc.GetNextRace();

            this.LoadRacerData();

            // Hook into the RaceComplete event
        }

        private void LoadRacerData()
        {
            // Set our Racers
            this.txtCar1.Text = _activeRace.Lane1Racer.GetScoreboardDisplay();
            this.txtCar2.Text = _activeRace.Lane2Racer.GetScoreboardDisplay();
            this.txtCar3.Text = _activeRace.Lane3Racer.GetScoreboardDisplay();
            this.txtCar4.Text = _activeRace.Lane4Racer.GetScoreboardDisplay();

            // Set the Racers in the next race
            if (_activeRace.OnDeckLane1 == null)
            {
                this.txtNextCar1.Text = this.txtNextCar2.Text = this.txtNextCar3.Text = this.txtNextCar4.Text = string.Empty;
            }
            else
            {
                this.txtNextCar1.Text = _activeRace.OnDeckLane1.GetOnDeckDisplay();
                this.txtNextCar2.Text = _activeRace.OnDeckLane2.GetOnDeckDisplay();
                this.txtNextCar3.Text = _activeRace.OnDeckLane3.GetOnDeckDisplay();
                this.txtNextCar4.Text = _activeRace.OnDeckLane4.GetOnDeckDisplay();
            }

            int heatIndex = RaceDataStore.PrelimHeatNumber - 1;

            // Clear out the Time boxes
            this.txtTime1.Text = _activeRace.Lane1Racer.Heats[heatIndex].Time.ToString("#.####");
            this.txtTime2.Text = _activeRace.Lane2Racer.Heats[heatIndex].Time.ToString("#.####");
            this.txtTime3.Text = _activeRace.Lane3Racer.Heats[heatIndex].Time.ToString("#.####");
            this.txtTime4.Text = _activeRace.Lane4Racer.Heats[heatIndex].Time.ToString("#.####");

            // Clear our winner picture
            this.picLane4.Visible = this.picLane3.Visible = this.picLane2.Visible = this.picLane1.Visible = false;

            // Set our Race state
            _activeRace.RaceState = RaceStateType.OnStartingLine;
        }

        /// <summary>
        /// Run the Pinewood Race
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            // Are we in the correct state for starting a race?
            if (_activeRace.RaceState == RaceStateType.OnStartingLine)
            {
                _activeRace.RaceState = RaceStateType.Racing;

                // Run the race
                RaceDataStore.RaceProc.RunRace(_activeRace);
            }
        }

        /// <summary>
        /// Event raised when the race processor recieves results
        /// from the Track Communication 
        /// </summary>
        delegate void RaceProc_OnRaceCompleteDelegate();

        void RaceProc_OnRaceComplete()
        {
            if (this.InvokeRequired == false)
            {
                int heatIndex = RaceDataStore.PrelimHeatNumber - 1;

                // Load our times
                this.txtTime1.Text = _activeRace.Lane1Racer.Heats[heatIndex].Time.ToString("#.####");
                this.txtTime2.Text = _activeRace.Lane2Racer.Heats[heatIndex].Time.ToString("#.####");
                this.txtTime3.Text = _activeRace.Lane3Racer.Heats[heatIndex].Time.ToString("#.####");
                this.txtTime4.Text = _activeRace.Lane4Racer.Heats[heatIndex].Time.ToString("#.####");

                // Find the winning lane
                int winningLane = FindWinningLane();

                switch (winningLane)
                {
                    case 1:
                        this.picLane1.Visible = true;
                        break;

                    case 2:
                        this.picLane2.Visible = true;
                        break;

                    case 3:
                        this.picLane3.Visible = true;
                        break;

                    case 4:
                        this.picLane4.Visible = true;
                        break;
                }

                // Change our Race State
                _activeRace.RaceState = RaceStateType.ReviewingResults;

                // Backup our results
                RaceDataStore.RaceProc.BackupRaceData();
            }
            else
            {
                RaceProc_OnRaceCompleteDelegate myDelegate = new RaceProc_OnRaceCompleteDelegate(RaceProc_OnRaceComplete);

                Invoke(myDelegate);
            }
        }

        private int FindWinningLane()
        {
            // Who won?
            double fastestTime;
            int winningLane;

            // Start by making lane 1 the winner
            fastestTime = Convert.ToDouble(this.txtTime1.Text);
            winningLane = 1;

            // Is Lane 2 faster?
            double lane2Time = Convert.ToDouble(this.txtTime2.Text);

            if (lane2Time < fastestTime)
            {
                winningLane = 2;
                fastestTime = lane2Time;
            }

            // Is Lane 2 faster?
            double lane3Time = Convert.ToDouble(this.txtTime3.Text);

            if (lane3Time < fastestTime)
            {
                winningLane = 3;
                fastestTime = lane3Time;
            }

            // Is Lane 2 faster?
            double lane4Time = Convert.ToDouble(this.txtTime4.Text);

            if (lane4Time < fastestTime)
            {
                winningLane = 4;
                fastestTime = lane4Time;
            }

            return winningLane;
        }

        /// <summary>
        /// Load the next Race
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonNextRace_Click(object sender, EventArgs e)
        {
            //TODO: Close race

            // Is the current race completed?
            if (this.IsRaceRecorded().Equals(false))
            {
                // Tell the user we can't continue
                MessageBox.Show(this, "Active race has not been completed.", "Are you sure", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            try
            {
                _activeRace = RaceDataStore.RaceProc.GetNextRace();

                this.LoadRacerData();
            }
            catch (NoMoreRacersException)
            {
                // Mark our heat as complete and increment the 
                // heat number
                RaceDataStore.PrelimHeatCompleted = false;
                RaceDataStore.PrelimHeatNumber++;

                // Are we done with the prelims?
                if (RaceDataStore.PrelimHeatNumber > 4)
                {
                    if (OnPrelimCompleteEvent != null)
                    {
                        OnPrelimCompleteEvent();
                    }

                    MessageBox.Show(this,"The prelims have completed. Stay tuned for the posting of the Sweet 16 Racers.","Prelims Complete",MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                else
                {
                    this.toolStripLabelHeat.Text = String.Concat("Heat ", RaceDataStore.PrelimHeatNumber);

                    this.ClearAllTextBoxes(this);

                    RaceDataStore.RaceProc.LoadNextHeat();

                    // Get the next race
                    _activeRace = RaceDataStore.RaceProc.GetNextRace();

                    this.LoadRacerData();
                }
            }
        }

        private bool IsRaceRecorded()
        {
            bool raceTimesRecorded = true;
            int heatIndex = RaceDataStore.PrelimHeatNumber - 1;

            if ((_activeRace.Lane1Racer.Heats[heatIndex].Time == 0) ||
               (_activeRace.Lane1Racer.Heats[heatIndex].Time == 0) ||
               (_activeRace.Lane1Racer.Heats[heatIndex].Time == 0) ||
               (_activeRace.Lane1Racer.Heats[heatIndex].Time == 0))
            {
                raceTimesRecorded = false;
            }

            return raceTimesRecorded;
        }

        /// <summary>
        /// Find all TextBox controls contained within the 
        /// Parent control passed as the parameter and clear
        /// the Text property
        /// </summary>
        /// <param name="parent"></param>
        private void ClearAllTextBoxes(Control parent)
        {
            Control[] controls = this.GetAllControls(parent);

            foreach (Control ctl in controls)
            {
                if (ctl is TextBox)
                {
                    ctl.Text = string.Empty;
                }
            }
        }

        private Control[] GetAllControls(Control parent)
        {
            ArrayList allControls = new ArrayList();

            Queue queue = new Queue();
            queue.Enqueue(parent.Controls);

            while (queue.Count > 0)
            {
                Control.ControlCollection controls = (Control.ControlCollection)queue.Dequeue();

                if (controls == null || controls.Count == 0)
                    continue;

                foreach (Control ctl in controls)
                {
                    allControls.Add(ctl);
                    queue.Enqueue(ctl.Controls);
                }
            }

            return (Control[])allControls.ToArray(typeof(Control));
        }

        private void toolStripButtonRerun_Click(object sender, EventArgs e)
        {
            int heatIndex = RaceDataStore.PrelimHeatNumber - 1;

            // Clear out the times in the active race
            _activeRace.Lane1Racer.Heats[heatIndex].Time = 0;
            _activeRace.Lane2Racer.Heats[heatIndex].Time = 0;
            _activeRace.Lane3Racer.Heats[heatIndex].Time = 0;
            _activeRace.Lane4Racer.Heats[heatIndex].Time = 0;

            // Refresh our window
            this.LoadRacerData();
        }

        private void FormPrelimRaceScoreboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            RaceDataStore.RaceProc.OnRaceComplete -= new RaceProcessor.OnRaceCompleteHandler(RaceProc_OnRaceComplete);

        }
    }
}
