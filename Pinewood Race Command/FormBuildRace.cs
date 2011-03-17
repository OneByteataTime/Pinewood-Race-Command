using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;

using Pinewood_Race_Command.dialogs;
using PinewoodDerby.DataAccess.Repository;

namespace Pinewood_Race_Command
{
    public partial class FormBuildRace : Form
    {
        private List<Racer> _racerList;
        private const string RACE_WATERMARK = "<enter race name>";

        // Properties
        public string RaceName { get; set; }

        public FormBuildRace()
        {
            InitializeComponent();

            // Store an internal reference to the Racer List
            _racerList = RaceDataStore.RacerList;

            this.FormatControls();
        }

        public void PromptForNewRacers()
        {
            // Ask the user how many racers to create
            FormNewUserDialog newUserDialog = new FormNewUserDialog();
            DialogResult result = newUserDialog.ShowDialog(this);

            switch (result)
            {
                case DialogResult.Yes:

                    int howMany = (int)newUserDialog.numericUpDown1.Value;

                    this.textBoxRaceName.Text = newUserDialog.Race.Name;
                    this.AddRacers(newUserDialog.Race, howMany);

                    // Auto-create default racer objects
                    _racerList = AddNewRacer(howMany);

                    break;

                case DialogResult.Cancel:

                    break;
            }

            // Load our data and set up our data bindings
            LoadAndBindData();
        }

        private void FormBuildRace_Load(object sender, EventArgs e)
        {
            // Load our Race Groups in List View
            for (int g = 1; g < 21; g++)
            {
                ListViewGroup lvg = new ListViewGroup(string.Concat("R", g), string.Concat("Race ", g));
                this.listView1.Groups.Add(lvg);
            }

            // Hook up all the textbox controls enter event
            this.textBoxLastName.GotFocus += new EventHandler(HighlightText);
            this.textBoxFirstName.GotFocus += new EventHandler(HighlightText);
            this.textBoxDen.GotFocus += new EventHandler(HighlightText);
            this.textBoxCarNumber.GotFocus += new EventHandler(HighlightText);
            this.textBoxWeight.GotFocus += new EventHandler(HighlightText);

            // Load our data ans set up our data bindings
            LoadAndBindData();
        }

        private void LoadAndBindData()
        {
            // Do we have any racers?
            if (_racerList.Count == 0)
            {
                // Disable all the text boxes
                this.DisableTextBoxes();

                string noRacersHTML = Properties.Resources.rsNoRacersMsg;

                this.webBrowser1.Navigate("about:blank");
                this.webBrowser1.Document.OpenNew(false);
                this.webBrowser1.Document.Write(noRacersHTML);

                this.buttonAddNewRacers.Visible = true;
            }
            else
            {
                // Make sure our text boxes are enabled
                this.EnableTextBoxes();

                string editRacersHTML = Properties.Resources.rsEditRacersMsg;

                this.webBrowser1.Navigate("about:blank");
                this.webBrowser1.Document.OpenNew(false);
                this.webBrowser1.Document.Write(editRacersHTML);

                // Set our Data Sources
                this.racerBindingSource.DataSource = _racerList;
                this.carBindingSource.DataSource = _racerList[0].PinewoodCar;
            }

            // Load our listview control
            this.LoadListView();

            if (RaceDataStore.LoadedRaceName.Length > 0)
            {
                textBoxRaceName.Text = RaceDataStore.LoadedRaceName;

                // Force a Key Down event
                textBoxRaceName_KeyDown(null, null);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.SmallIcon;
        }

        private void AddRacers(PinewoodDerby.DataAccess.Models.Race race, int howMany)
        {
            for (int i = 0; i < howMany; i++)
            {
                PinewoodDerby.DataAccess.Models.Racer racer = new PinewoodDerby.DataAccess.Models.Racer()
                {
                    Firstname = "Racer",
                    Lastname = i.ToString(),
                    Den = "0",
                    Race = race
                };

                race.Racers.Add(racer);
            }

            using (NHibernateUnitOfWork.UnitOfWork.Start())
            {
                IRaceRepository repository = new RaceRepository();
                repository.Add(race);
            }
        }

        private List<Racer> AddNewRacer(int howMany)
        {
            List<Racer> racerList = new List<Racer>();

            for (int i = 0; i < howMany; i++)
            {
                Racer racer = new Racer("Racer", i.ToString());
                racerList.Add(racer);
            }

            return racerList;
        }

        private void LoadListView()
        {
            this.listView1.Items.Clear();

            int raceGroup = 0;
            int raceCount = 1;

            foreach (Racer racer in _racerList)
            {
                ListViewItem listItem = new ListViewItem(this.listView1.Groups[0]);
                listItem.Text = racer.ToString();
                listItem.ImageIndex = 0;
                listItem.Group = this.listView1.Groups[raceGroup];
                listItem.Tag = racer;

                this.listView1.Items.Add(listItem);

                // Increment our race count
                raceCount++;

                if (raceCount > 4)
                {
                    raceCount = 1;
                    raceGroup++;
                }
            }

            // Do we have any racers in our list?
            if (this.listView1.Items.Count > 0)
            {
                // Select the first item
                this.listView1.Focus();
                this.listView1.Items[0].Selected = true;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.LargeIcon;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // What is the index of the selected item?
                int index = listView1.SelectedItems[0].Index;

                // Manually set the position of racer data source
                this.racerBindingSource.Position = index;
                this.carBindingSource.DataSource = _racerList[index].PinewoodCar;
            }
        }

        private void HighlightText(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void FormBuildRace_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Should we save?
            if (this.DialogResult.Equals(DialogResult.OK))
            {
                this.racerBindingSource.EndEdit();

                if (Verify())
                {
                    // Store our data back in the Race Data Store
                    RaceDataStore.RacerList = _racerList;

                    // Save our data 
                    RaceDataStore.RaceProc.SerializeRacersToDisk(textBoxRaceName.Text);

                    // Record the race name so the main page can display it
                    this.RaceName = textBoxRaceName.Text;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Verify that we have enough data to save
        /// </summary>
        /// <returns></returns>
        private bool Verify()
        {
            bool isValid = true;

            // For simplicity when assigning lanes, we 
            // are forcing the race to have racers in multiples
            // of 4
            double remainder = _racerList.Count % 4;

            if (remainder != 0)
            {
                string message = String.Format("Please add '{0}' more racers. To make lane assignments work correctly, we need to have even multiples of 4 racers.", (4 - remainder));

                MessageBox.Show(this,message, "Race is incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            // Do we have any racers in the race?
            if (_racerList.Count == 0)
            {
                MessageBox.Show(this,"No racers have been added.", "Race is incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (textBoxRaceName.Text == RACE_WATERMARK)
            {
                MessageBox.Show(this, "Please give this race a name.", "Race is incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            return isValid;
        }

        private void DisableTextBoxes()
        {
            // Should loop thru control collection, but for now just
            // disable the text boxes we know of
            this.textBoxLastName.Enabled = false;
            this.textBoxFirstName.Enabled = false;
            this.textBoxDen.Enabled = false;
            this.textBoxCarNumber.Enabled = false;
            this.textBoxWeight.Enabled = false; 
        }

        private void EnableTextBoxes()
        {
            // Should loop thru control collection, but for now just
            // disable the text boxes we know of
            this.textBoxLastName.Enabled = true;
            this.textBoxFirstName.Enabled = true;
            this.textBoxDen.Enabled = true;
            this.textBoxCarNumber.Enabled = true;
            this.textBoxWeight.Enabled = true;

            // Hide the Add Racers button if it is showing
            this.buttonAddNewRacers.Visible = false;
        }

        private void FormatControls()
        {
            this.textBoxRaceName.Text = RACE_WATERMARK;
            this.textBoxRaceName.BackColor = Color.Yellow;
            this.textBoxRaceName.ForeColor = Color.Red;

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = racerBindingSource;
        }

        private void buttonAddNewRacers_Click(object sender, EventArgs e)
        {
            this.PromptForNewRacers();
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            //TODO: Prompt for save 
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBoxRaceName_KeyDown(object sender, KeyEventArgs e)
        {
            textBoxRaceName.BackColor = SystemColors.Window;
            textBoxRaceName.ForeColor = Color.Black;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Get the selected index of the ListView
            int index = this.listView1.SelectedItems[0].Index;

            // Increment our index
            index++;

            this.listView1.SelectedItems.Clear();

            if (index < this.listView1.Items.Count)
            {
                this.listView1.Items[index].Selected = true;
            }
            else
            {
                index = 0;
                this.listView1.Items[index].Selected = true; 
            }
        }

        /// <summary>
        /// Add new racer to the race
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonAddRacer_Click(object sender, EventArgs e)
        {
            int racerCount = _racerList.Count;

            // Create a new racer
            Racer racer = new Racer("Racer", racerCount.ToString());

            // Add this new racer to our List
            _racerList.Add(racer);
            this.racerBindingSource.Add(racer);

            // Reload our list view
            this.LoadListView();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            // Get the selected racer
            Racer selectedRacer = this.listView1.SelectedItems[0].Tag as Racer;

            // Remove from both the List and the BindingSource
            _racerList.Remove(selectedRacer);
            this.racerBindingSource.Remove(selectedRacer);

            this.LoadListView();
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (racerBindingSource.Position > 0)
            {
                this.racerBindingSource.MovePrevious();
                int index = racerBindingSource.Position;
                this.carBindingSource.DataSource = _racerList[index].PinewoodCar;
            }

            this.textBoxFirstName.Focus();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (racerBindingSource.Position < _racerList.Count)
            {
                this.racerBindingSource.MoveNext();
                int index = racerBindingSource.Position;
                this.carBindingSource.DataSource = _racerList[index].PinewoodCar;
            }

            this.textBoxFirstName.Focus();
        }
    }
}
