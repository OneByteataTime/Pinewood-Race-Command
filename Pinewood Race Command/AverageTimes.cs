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
    public partial class AverageTimes : Form
    {
        public AverageTimes()
        {
            InitializeComponent();

            this.InitializeWebBrowser();
        }

        public void ShowAverageHeatTimes()
        {
            this.webBrowser1.Document.Write("<table border='1'>");
            this.webBrowser1.Document.Write("<tr><th>Racer Name</th><th>Average Heat Time</th></tr>");

            foreach (Racer racer in RaceDataStore.RacerList)
            {
                racer.UpdateAverageHeatTime();

                this.webBrowser1.Document.Write("<tr>");

                string tableRow = String.Format("<td>{0}</td><td>{1}</td>", racer.GetScoreboardDisplay(), racer.AverageHeatTime);
                this.webBrowser1.Document.Write(tableRow);

                this.webBrowser1.Document.Write("</tr>");
            }

            this.webBrowser1.Document.Write("</table>");
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
