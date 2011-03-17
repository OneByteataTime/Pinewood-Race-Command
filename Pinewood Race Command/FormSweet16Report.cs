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
    public partial class FormSweet16Report : Form
    {
        public FormSweet16Report()
        {
            InitializeComponent();

            this.InitializeWebBrowser();
        }

        public void ShowSweet16(string title)
        {            
            this.webBrowser1.Document.Write(String.Format("<h1>{0}</h1>", title));

            List<Racer> sweet16Racers = RaceDataStore.RacerList;

            sweet16Racers.Sort(delegate(Racer racer1, Racer racer2)
            {
                return Comparer<double>.Default.Compare(racer1.AverageHeatTime, racer2.AverageHeatTime);
            });


            this.WriteULStart();

            int index = 0;
            // Loop thru the racers and write out list item tags
            foreach (Racer racer in sweet16Racers)
            {
                string lineItem = String.Format("<LI>{0}</LI>", racer.GetScoreboardDisplay());

                this.webBrowser1.Document.Write(lineItem);

                index++;

                if (index > 16)
                {
                    break;
                }
            }

            this.WriteULEnd();
        }

        private void WriteULStart()
        {
            this.webBrowser1.Document.Write("<DIV ID=\"Sweet16Block\">");
            this.webBrowser1.Document.Write("<OL>");
        }

        private void WriteULEnd()
        {
            this.webBrowser1.Document.Write("</OL>");
            this.webBrowser1.Document.Write("</DIV>");
        }

        private void InitializeWebBrowser()
        {
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Navigate("about:blank");

            this.webBrowser1.Document.OpenNew(false);

            string headerText = String.Format("<div style=\"text-align:right;\">{0}</div>", DateTime.Today.ToLongDateString());

            this.webBrowser1.Document.Write(headerText);
        }
    }
}
