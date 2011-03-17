using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pinewood_Race_Command.controls
{
    public partial class CommandButton : Button
    {
        public CommandButton()
        {
            InitializeComponent(); 
 
            // Format our new button
            this.FormatButton();
        }

        private void FormatButton()
        {
            this.BackColor = Color.White;
            this.Size = new Size(125, 100);
            this.ImageAlign = ContentAlignment.TopCenter;
            this.TextAlign = ContentAlignment.BottomCenter;

            this.Margin = new Padding(10);
            this.Padding = new Padding(3);

            this.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold);
            this.UseVisualStyleBackColor = false;
        }
    }
}
