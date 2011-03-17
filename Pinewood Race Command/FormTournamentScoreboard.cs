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
    public partial class FormTournamentScoreboard : Form
    {
        private TournamentRace _activeRace;

        public delegate void PrelimCompleteHandler();
        public event PrelimCompleteHandler OnPrelimCompleteEvent;

        public FormTournamentScoreboard()
        {
            InitializeComponent();
        }

        private void FormPrelimRaceCommand_Load(object sender, EventArgs e)
        {
            // NOTE: The first heat was already loaded by the Prelim Race Form

            // Get the next race
            _activeRace = RaceDataStore.RaceProc.GetNextTournamentRace();

            // Hook into the race complete event
            RaceDataStore.RaceProc.OnRaceComplete += new RaceProcessor.OnRaceCompleteHandler(RaceProc_OnRaceComplete);
            this.LoadRacerData();
        }

        delegate void RaceProc_OnRaceCompleteDelegate();
        
        void RaceProc_OnRaceComplete()
        {
            if (this.InvokeRequired == false)
            {
                // Load our times
                this.txtTime2.Text = _activeRace.Racer1.RaceTime.ToString("#.####");
                this.txtTime3.Text = _activeRace.Racer2.RaceTime.ToString("#.####");

                int winningLane = _activeRace.Racer1.RaceTime < _activeRace.Racer2.RaceTime ? 2 : 3;

                switch (winningLane)
                {
                    case 2:
                        this.picLane2.Visible = true;
                        break;

                    case 3:
                        this.picLane3.Visible = true;
                        break;
                }

            }
            else
            {
                RaceProc_OnRaceCompleteDelegate myDelegate = new RaceProc_OnRaceCompleteDelegate(RaceProc_OnRaceComplete);

                Invoke(myDelegate);
            }
        }

        private void LoadRacerData()
        {
            try
            {
                // Set our Racers
                this.txtCar1.Text = string.Empty;
                this.txtCar2.Text = _activeRace.Racer1.Racer.GetScoreboardDisplay();
                this.txtCar3.Text = _activeRace.Racer2.Racer.GetScoreboardDisplay();
                this.txtCar4.Text = string.Empty;

                this.picLane2.Visible = this.picLane3.Visible = false;
            }
            catch (Exception)
            { }

            // Clear out the Time boxes
            this.txtTime1.Text = this.txtTime2.Text = this.txtTime3.Text = this.txtTime4.Text = string.Empty;
            this.toolStripLabelHeat.Text = _activeRace.ToString();
        }

        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            RaceDataStore.RaceProc.RunTournamentRace(_activeRace);
        }

        private void toolStripButtonNextRace_Click(object sender, EventArgs e)
        {
            //TODO: Close race

            // Is the current race completed?
            if (this.IsRaceRecorded().Equals(false))
            {
                // Tell the user we can't continue
                MessageBox.Show(this,"Active race has not been completed.","Are you sure", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            try
            {
                // Advance the winner to the next round
                if (RaceDataStore.RaceProc.AdvanceWinner(_activeRace))
                {

                    // Get the next race
                    _activeRace = RaceDataStore.RaceProc.GetNextTournamentRace();

                    this.LoadRacerData();
                }
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

                    MessageBox.Show("The prelims have completed. Stay tuned for the posting of the Sweet 16 Racers.");

                    this.Close();
                }
                else
                {
                    this.toolStripLabelHeat.Text = String.Concat("Heat ", RaceDataStore.PrelimHeatNumber);

                    this.ClearAllTextBoxes(this);

                    RaceDataStore.RaceProc.LoadNextHeat();

                    // Get the next race
                    //_activeRace = RaceDataStore.RaceProc.GetNextRace();

                    this.LoadRacerData();
                }
            }
        }

        private bool IsRaceRecorded()
        {
            bool raceTimesRecorded = true;
            int heatIndex = RaceDataStore.PrelimHeatNumber - 1;

            if (_activeRace.Racer1.RaceTime == 0 ||
                _activeRace.Racer2.RaceTime == 0)
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
            _activeRace.Racer1.RaceTime = 0;
            _activeRace.Racer2.RaceTime = 0;

            // Refresh our window
            this.LoadRacerData();
        }
    }
}
