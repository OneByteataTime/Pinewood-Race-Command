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
    public partial class FormPrelimRace : Form
    {
        public FormPrelimRace()
        {
            InitializeComponent();

            CheckBox cb = new CheckBox();
            cb.Text = "Emulate Race";
            
            cb.Name = "CheckBoxEmulate";
            cb.Size = new Size(100, toolStrip1.Height - 2);
            cb.CheckedChanged += new EventHandler(cb_CheckedChanged);

            cb.Checked = false;

            ToolStripItem tsi = new ToolStripControlHost(cb);
            tsi.Name = "toolStripEmulateRace";
            tsi.Size = new Size(100, toolStrip1.Height - 2);
            tsi.Alignment = ToolStripItemAlignment.Right;

            this.toolStrip1.Items.Add(tsi);

            // Set our first instruction
            this.toolStripStatusLabel1.Text = "Step 1: Assign lanes to racers.";
        }

        void cb_CheckedChanged(object sender, EventArgs e)
        {
            RaceDataStore.EmulateRace = ((CheckBox)sender).Checked;
        }

        private void toolStripButtonBuildRaces_Click(object sender, EventArgs e)
        {
            
            int racerCount = 1;

            // Set up the lane rotation for each lane
            int [] laneAssignment1 = new int[] { 1, 2, 3, 4};
            int [] laneAssignment2 = new int[] { 2, 3, 4, 1};
            int [] laneAssignment3 = new int[] { 3, 4, 1, 2};
            int [] laneAssignment4 = new int[] { 4, 1, 2, 3};

            int[] laneAssignment = null;

            foreach (Racer racer in RaceDataStore.RacerList)
            {
                // Clear any existing Heat records
                racer.Heats.Clear();

                racer.Heats = new List<Heat>(4);
                Heat[] heatList = new Heat[] { new Heat(1), new Heat(2), new Heat(3), new Heat(4) };
                racer.Heats.AddRange(heatList);

                // Select the appropriate Lane Assignment array
                switch (racerCount)
                {
                    case 1:
                        laneAssignment = laneAssignment1;
                        break;

                    case 2:
                        laneAssignment = laneAssignment2;
                        break;

                    case 3:
                        laneAssignment = laneAssignment3;
                        break;

                    case 4:
                        laneAssignment = laneAssignment4;
                        break;
                }

                // Loop thru heats and assign lanes
                for (int x = 0; x < 4; x++)
                {
                    racer.Heats[x].Lane = laneAssignment[x];
                }

                if (racerCount == 4)
                {
                    racerCount = 1;
                }
                else
                {
                    racerCount++;
                }
            }

            // Load Heat the first heat (Heat # should
            // be 1 at this point)
            RaceDataStore.RaceProc.LoadNextHeat();

            // Store the number of Races in each heat - we have 
            // an even number of racers so each stack represents the 
            // number of races in the heat.
            RaceDataStore.RaceCount = RaceDataStore.LanePool.Lane1Stack.Count;

            // Reload our TreeView
            this.LoadTreeView();

            if (RaceDataStore.RacerList.Count > 0)
            {
                this.toolStripStatusLabel1.Text = "Step 2: Lane assignments complete, we are go for racing";
            }
            else
            {
                this.toolStripStatusLabel1.Text = "Warning: Lane assignments are not complete.";
            }
        }

        private void FormPrelimRace_Load(object sender, EventArgs e)
        {
            // Load our TreeView 
            this.LoadTreeView();
        }

        private void LoadTreeView()
        {
            // If we have nodes remove them
            this.treeView1.Nodes.Clear();

            // Make our root node the Race Name
            TreeNode rootNode = this.treeView1.Nodes.Add(RaceDataStore.LoadedRaceName);

            // Get the number of races we have
            int raceCount = RaceDataStore.RaceCount;

            // Build a Race node for each race
            for (int r = 0; r < raceCount; r++)
            {
                TreeNode tnode = new TreeNode(string.Concat("Race", r + 1));
               
                // Append to the root node
                rootNode.Nodes.Add(tnode);
            }

            // Now iterate thru the Racers and add to Race nodes
            foreach (Racer racer in RaceDataStore.RacerList)
            {
                // Our array is zero based so....
                int index = (racer.RaceInfo.Number - 1);

                // Compute the Average heat times
                racer.UpdateAverageHeatTime();

                // Has this racer been assigned a race
                if (index > -1)
                {
                    TreeNode node = this.treeView1.Nodes[0].Nodes[index].Nodes.Add(racer.ToString());

                    // Iterate thru the heats and record the times
                    foreach (Heat heat in racer.Heats)
                    {
                        TreeNode heatNode = node.Nodes.Add(string.Concat("Heat ", heat.Number, " - ", heat.Time.ToString("0.###")));

                        // Add the Heat object so our property grid
                        // can display the details
                        heatNode.Tag = heat;
                    }

                    node.Nodes.Add(string.Concat("Avg Heat Time: ", racer.AverageHeatTime.ToString("0.####")));

                    // Save this node in the tag property in case we 
                    // need it later
                    node.Tag = racer;
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // Do we have everything we need to run a race?
            if (this.ReadyToRace())
            {
                // Are we emulate mode?
                if (RaceDataStore.EmulateRace == false)
                {
                    try
                    {
                        // Initialize the track
                        RaceDataStore.RaceProc.InitializeTrack();
                    }
                    catch (ApplicationException appExp)
                    {
                        string message = String.Concat(appExp.Message, " \r\n If you want to emulate, click the emulate checkbox at the top right corner of this page");

                        DialogResult result = MessageBox.Show(message, "Did you hear that crash?", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }

                // Show the Prelim Race Command Center 
                FormPrelimRaceScoreboard prelimScoreboardForm = new FormPrelimRaceScoreboard();
                prelimScoreboardForm.Show(this);

                // Hook up the Prelim Complete event so we can update our tree
                // with results
                prelimScoreboardForm.OnPrelimCompleteEvent += new FormPrelimRaceScoreboard.PrelimCompleteHandler(prelimScoreboardForm_OnPrelimCompleteEvent);
            }
        }

        void prelimScoreboardForm_OnPrelimCompleteEvent()
        {
            this.LoadTreeView();
        }

        /// <summary>
        /// Compute the Sweet 16
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonSweet16_Click(object sender, EventArgs e)
        {
            this.treeViewSweet16.Location = this.treeView1.Location;
            this.treeViewSweet16.Size = this.treeView1.Size;
            this.treeViewSweet16.BringToFront();
            this.treeViewSweet16.Visible = true;

            // Calculate the official Heat Times
            this.CalculateHeatAverages();

            // Sort the racers by Avg Heat Time
            List<Racer> racerList = RaceDataStore.RacerList;
            racerList.Sort(delegate(Racer racer1, Racer racer2)
            {
                return Comparer<double>.Default.Compare(racer1.AverageHeatTime, racer2.AverageHeatTime);
            });

            this.LoadSweet16Treeview(racerList);

            // Show the Sweet 16 Report 
            Sweet16Report sweet16Form = new Sweet16Report();
            sweet16Form.Sweet16Racers = RaceDataStore.Sweet16Racers;
            sweet16Form.Show();
        }

        private bool ReadyToRace()
        {
            bool isValid = true;

            if (RaceDataStore.RacerList.Count == 0)
            {
                MessageBox.Show(this, "We seem to be missing racers, please check that you loaded a Race and have racers displayed in the list.", "Not quite ready to race!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                isValid = false;
            }
            else if (RaceDataStore.RacerList[0].Heats.Count == 0)
            {
                this.toolStripButtonBuildRaces_Click(null, new EventArgs());
            }

            return isValid;
        }

        private void CalculateHeatAverages()
        {
            // Loop thru the Racers
            foreach (Racer racer in RaceDataStore.RacerList)
            {
                racer.UpdateAverageHeatTime();
            }
        }

        private void LoadSweet16Treeview(List<Racer> racers)
        {
            this.treeViewSweet16.Nodes.Clear();

            // Build the Root Node
            TreeNode rootNode = this.treeViewSweet16.Nodes.Add("Sweet 16 Qualifiers");

            int placeIndex = 1;

            // Iterate over the racers and display the top 16
            foreach (Racer racer in racers)
            {
                TreeNode node = new TreeNode(racer.GetScoreboardDisplay());

                // Build a node for the average heat time
                TreeNode avgHeatNode = new TreeNode(racer.AverageHeatTime.ToString("0.####"));
                avgHeatNode.Tag = racer;

                node.Nodes.Add(avgHeatNode);
                rootNode.Nodes.Add(node);

                // Build a new Sweet16 object
                Sweet16Racer sweet16 = new Sweet16Racer(racer, placeIndex);
                
                // Add to our Sweet 16 collection
                RaceDataStore.Sweet16Racers.Add(sweet16);

                placeIndex++;

                if (placeIndex > 16)
                    break;
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                object prelimObj = e.Node.Tag;

                if (prelimObj is Racer)
                {
                    this.propertyGrid1.SelectedObject = (Racer)e.Node.Tag;
                }
                else if (prelimObj is Heat)
                {
                    this.propertyGrid1.SelectedObject = (Heat)e.Node.Tag;
                }
            }
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            this.LoadTreeView();
        }

        private void toolStripButtonToggleView_Click(object sender, EventArgs e)
        {
            this.treeView1.BringToFront();
            this.LoadTreeView();
        }

        private void treeViewSweet16_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                object prelimObj = e.Node.Tag;

                if (prelimObj is Racer)
                {
                    this.propertyGrid1.SelectedObject = (Racer)e.Node.Tag;
                }
                else if (prelimObj is Heat)
                {
                    this.propertyGrid1.SelectedObject = (Heat)e.Node.Tag;
                }
            }
        }
    }
}
