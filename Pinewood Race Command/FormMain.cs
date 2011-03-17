using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pinewood_Race_Command
{
    public partial class FormMain : Form
    {
        private FormBuildRace _buildRaceForm;
        private FormTestTrack _testTrackForm;
        private FormPrelimRace _prelimRaceForm;
        private FormSweet16Report _sweet16Form;
        private AverageTimes _averageHeatTimesForm;

        public FormMain()
        {
            InitializeComponent();

            this.toolStripStatusLabel1.Text = "Please create a new race or load an existing file.";
        }

        void _buildRaceForm_Disposed(object sender, EventArgs e)
        {
            _buildRaceForm = null;
        }

        void _testTrackForm_Disposed(object sender, EventArgs e)
        {
            _testTrackForm = null;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void commandButtonTestTrack_Click(object sender, EventArgs e)
        {
            if (_testTrackForm == null)
            {
                _testTrackForm = new FormTestTrack();
                _testTrackForm.Show();

                _testTrackForm.Disposed += new EventHandler(_testTrackForm_Disposed);
            }
            else
            {
                _testTrackForm.BringToFront();
                _testTrackForm.Focus();
            }
        }

        private void commandButtonBuildRace_Click(object sender, EventArgs e)
        {
            // Show the Build Race form
            ShowBuildRaceForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Create a new Racer List
            RaceDataStore.RacerList = new List<Racer>();
            RaceDataStore.Sweet16Racers = new List<Sweet16Racer>();
            RaceDataStore.TournamentRaces = new Dictionary<string, TournamentRace>();
            RaceDataStore.RaceKeyQueue = new Queue<string>();

            // Create a new Race Processor object
            RaceDataStore.RaceProc = new RaceProcessor();

            // Default our Race Name
            RaceDataStore.LoadedRaceName = string.Empty;

            RaceDataStore.LanePool = new LanePool();
            RaceDataStore.PrelimHeatNumber = 1;
            RaceDataStore.PrelimHeatCompleted = false;
        }

        private void commandButtonLoadRace_Click(object sender, EventArgs e)
        {
            // Setup our Open File dialog
            this.openFileDialog1.Title = "Select a saved race to load...";
            this.openFileDialog1.InitialDirectory = RaceDataStore.RaceProc.DataStorePath;

            DialogResult result = this.openFileDialog1.ShowDialog(this);

            switch (result)
            {
                case DialogResult.Cancel:

                    break;
                case DialogResult.OK:

                    // Deserialize race data back to Racer List
                    RaceDataStore.RaceProc.DeserializeRacersToObject(this.openFileDialog1.FileName);

                    // Show the user the Racers
                    ShowBuildRaceForm();
                    break;
            }
        }

        private void ShowBuildRaceForm()
        {
            // Do we need to create a build race form?
            if (_buildRaceForm == null)
            {
                _buildRaceForm = new FormBuildRace();
                _buildRaceForm.Show();

                // Do we have any Racers loaded?
                if (RaceDataStore.RacerList.Count == 0)
                {
                    // Prompt the user to add new racers
                    _buildRaceForm.PromptForNewRacers();
                }

                // Hook up any events we are interested in
                _buildRaceForm.FormClosing += new FormClosingEventHandler(_buildRaceForm_FormClosing);
                _buildRaceForm.Disposed += new EventHandler(_buildRaceForm_Disposed);

            }
            else
            {
                _buildRaceForm.BringToFront();
                _buildRaceForm.Focus();
            }
        }

        /// <summary>
        /// Build race form is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _buildRaceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string loadedRaceName = String.Concat("Loaded Race: ", _buildRaceForm.RaceName);

            // Set the name of the loaded race in the status bar
            this.toolStripStatusLabel1.Text = loadedRaceName;
        }

        private void commandButton1_Click(object sender, EventArgs e)
        {
            if (_prelimRaceForm == null)
            {
                _prelimRaceForm = new FormPrelimRace();
                _prelimRaceForm.Show();
            }
            else
            {
                if (_prelimRaceForm.IsDisposed)
                {
                    _prelimRaceForm = new FormPrelimRace();
                    _prelimRaceForm.Show();
                }
                else
                {
                    _prelimRaceForm.Focus();
                }
            }
        }

        private void commandButton2_Click(object sender, EventArgs e)
        {
            FormTournamentCommand tournamentCommandForm = new FormTournamentCommand();
            tournamentCommandForm.Show();
        }

        /// <summary>
        /// Restore an existing race 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commandButton6_Click(object sender, EventArgs e)
        {
            // Setup our Open File dialog
            this.openFileDialog1.Title = "Select a race backup to restore...";

            string backupFolder = System.IO.Path.Combine(RaceDataStore.RaceProc.DataStorePath, "Backups");
            this.openFileDialog1.InitialDirectory = backupFolder;

            this.openFileDialog1.ShowDialog(this);

            // Deserialize race data back to Racer List
            RaceDataStore.RaceProc.DeserializeRacersToObject(this.openFileDialog1.FileName);

            // Load all our Heats
            RaceDataStore.RaceProc.LoadNextHeat();

            // Store the number of Races in each heat - we have 
            // an even number of racers so each stack represents the 
            // number of races in the heat.
            RaceDataStore.RaceCount = RaceDataStore.LanePool.Lane1Stack.Count;

            //// Show the user the Racers
            //ShowBuildRaceForm();            
        }

        /// <summary>
        /// Show the Sweet 16 Racers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commandButton4_Click(object sender, EventArgs e)
        {
            _sweet16Form = new FormSweet16Report();
            _sweet16Form.Show(this);
            _sweet16Form.ShowSweet16("2009 Sweet 16 Racers");

            _sweet16Form.Disposed += new EventHandler(_sweet16Form_Disposed);
        }

        void _sweet16Form_Disposed(object sender, EventArgs e)
        {
            _sweet16Form = null;
        }

        private void commandButton3_Click(object sender, EventArgs e)
        {
            _averageHeatTimesForm = new AverageTimes();

            _averageHeatTimesForm.Show(this);
            _averageHeatTimesForm.ShowAverageHeatTimes();

            _averageHeatTimesForm.Disposed += new EventHandler(_averageHeatTimesForm_Disposed);
        }

        void _averageHeatTimesForm_Disposed(object sender, EventArgs e)
        {
            _averageHeatTimesForm = null;
        }
    }
}
