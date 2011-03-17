using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Pinewood_Race_Command
{
    public partial class FormTournamentCommand : Form
    {
        public FormTournamentCommand()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Load Tournament Bracket objects
            this.LoadTournamentBrackets();

            // Load our UI
            this.LoadRaceView();
        }

        private void LoadTournamentBrackets()
        {
            XmlDocument raceTemplateXml = this.LoadTournamentTemplate();

            // Iterate over the brackets and build tournement objects
            XmlNodeList bracketList = raceTemplateXml.SelectNodes("/tournamentTemplate/raceTemplate/race");

            foreach (XmlNode raceNode in bracketList)
            {
                TournamentRace tournamentRace = new TournamentRace(raceNode.Attributes["winnerKey"].Value.ToString());

                string key = raceNode.Attributes["key"].Value.ToString();


                RaceDataStore.TournamentRaces.Add(key, tournamentRace);
            }

            this.LoadRacersInTournametBracket(raceTemplateXml);
        }

        private void LoadRacersInTournametBracket(XmlDocument raceTemplateXml)
        {
            // Get a node list of all the Bracket seeds
            XmlNodeList bracketSeedList = raceTemplateXml.SelectNodes("/tournamentTemplate/bracketTemplate/bracket");

            foreach (XmlNode bracketnode in bracketSeedList)
            {
                int seed = Convert.ToInt32(bracketnode.FirstChild.Attributes["index"].Value);
                int seed2 = Convert.ToInt32(bracketnode.ChildNodes[1].Attributes["index"].Value);

                string key = bracketnode.Attributes["raceKey"].Value;

                TournamentRace tourRace = RaceDataStore.TournamentRaces[key];

                //HACK: 
                if (seed < RaceDataStore.Sweet16Racers.Count)
                {
                    tourRace.Racer1 = RaceDataStore.Sweet16Racers[seed];
                }

                //HACK:
                if (seed2 < RaceDataStore.Sweet16Racers.Count)
                {
                    tourRace.Racer2 = RaceDataStore.Sweet16Racers[seed2];
                }

                // Load the Race Keys in our Queue
                RaceDataStore.RaceKeyQueue.Enqueue(key);
            }
        }

        private void LoadRaceView()
        {
            // Do we have any nodes in our tree?
            if (this.treeView1.Nodes.Count > 0)
            {
                this.treeView1.Nodes.Clear();
            }

            // Add our root node
            TreeNode rootNode = new TreeNode("Sweet 16 Tournament");

            foreach (string key in RaceDataStore.TournamentRaces.Keys)
            {
                TournamentRace tourRace = RaceDataStore.TournamentRaces[key];

                // Make our key look nicer
                string keyExpanded = key;
                keyExpanded = keyExpanded.Replace("R", "Round ");
                keyExpanded = keyExpanded.Replace("B", "Bracket ");

                TreeNode raceNode = new TreeNode(keyExpanded);

                //HACK:
                string racerNodeText = tourRace.Racer1 == null ? "Empty" : tourRace.Racer1.Racer.GetScoreboardDisplay();
                string racer2NodeText = tourRace.Racer2 == null ? "Empty" : tourRace.Racer2.Racer.GetScoreboardDisplay();

                TreeNode racer1Node = new TreeNode(racerNodeText);
                if (tourRace.Racer1 != null)
                    racer1Node.Tag = tourRace.Racer1.Racer;

                TreeNode racer2Node = new TreeNode(racer2NodeText);
                
                if (tourRace.Racer2 != null)
                    racer2Node.Tag = tourRace.Racer2.Racer;

                raceNode.Nodes.AddRange(new TreeNode[] { racer1Node, racer2Node });

                rootNode.Nodes.Add(raceNode);
            }

            this.treeView1.Nodes.Add(rootNode);

        }

        private XmlDocument LoadTournamentTemplate()
        {
            XmlDocument dom = new XmlDocument();
            dom.Load("TournamentTemplate.xml");

            return dom;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                this.propertyGrid1.SelectedObject = (Racer)e.Node.Tag;
            }
        }

        private void toolStripButtonRunTournament_Click(object sender, EventArgs e)
        {
            if (RaceDataStore.EmulateRace == false)
            {
                RaceDataStore.RaceProc.InitializeTrack();
            }

            FormTournamentScoreboard scoreboardForm = new FormTournamentScoreboard();
            scoreboardForm.Show();


        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            this.LoadRaceView();
        }
    }
}
