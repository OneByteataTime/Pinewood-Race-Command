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
    public partial class Sweet16Report : Form
    {
        private List<Sweet16Racer> _racers;

        public Sweet16Report()
        {
            InitializeComponent();
        }

        public List<Sweet16Racer> Sweet16Racers
        {
            set 
            {
                _racers = value;
                this.LoadListView();
            }
        }

        private void LoadListView()
        {
            foreach (Sweet16Racer racer in _racers)
            {
                ListViewItem racerItem = new ListViewItem(racer.Seed.ToString());

                ListViewItem.ListViewSubItem nameItem = new ListViewItem.ListViewSubItem(racerItem,String.Concat(racer.Racer.FirstName, " ", racer.Racer.LastName));
                ListViewItem.ListViewSubItem carItem = new ListViewItem.ListViewSubItem(racerItem, racer.Racer.PinewoodCar.Number.ToString());
                ListViewItem.ListViewSubItem avgItem = new ListViewItem.ListViewSubItem(racerItem, racer.Racer.AverageHeatTime.ToString("0.####"));

                racerItem.SubItems.AddRange(new ListViewItem.ListViewSubItem[] { nameItem, carItem, avgItem });

                this.listView1.Items.Add(racerItem);
            }
        }
    }
}
