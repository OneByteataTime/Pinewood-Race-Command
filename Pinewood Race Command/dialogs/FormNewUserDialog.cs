using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PinewoodDerby.DataAccess.Repository;

namespace Pinewood_Race_Command.dialogs
{
    public partial class FormNewUserDialog : Form
    {
        public PinewoodDerby.DataAccess.Models.Race Race { get; internal set; }

        public FormNewUserDialog()
        {
            InitializeComponent();

            this.labelHelp.Text = "Create new racers for this race. Select a number that is a multiple of 4.";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            PinewoodDerby.DataAccess.Models.Race race = new PinewoodDerby.DataAccess.Models.Race()
            {
                Name = this.textBoxRaceName.Text,
                Description = string.Concat("Pinewood Derby Race ", DateTime.Now.ToShortDateString()),
                CreateDate = DateTime.Now
            };

            this.Race = race;

            this.DialogResult = DialogResult.Yes;
        }

        private void FormNewUserDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
