using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

namespace Pinewood_Race_Command.controls
{
    public partial class CommandGroupControl : UserControl
    {
#region Member variables
        
        private string _title;
        private CommandButton[] _buttonArray;
        private Color _backColor1;
        private Color _backColor2;

#endregion

        #region Properties
        [Category("Custom")]
        public string Title
        {
            get { return _title; }
            set 
            {
                // Set our title value
                _title = value;

                // Force a repaint of the title bar
                this.Invalidate(new Rectangle(0, 0, this.Width, 25));
            }
        }

        [Category("Custom")]
        public CommandButton[] Buttons
        {
            get { return _buttonArray; }
            set {
                //Set our button array 
                _buttonArray = value;

                // Load these buttons in the flow panel
                LoadButtons(_buttonArray);
            }
        }

        #endregion


        public CommandGroupControl()
        {
            InitializeComponent();

            // Initialize our properties
            _title = "Group Title...";

            // Format our controls
            this.FormatControls();

            this.BuildColorPalette();

            // Handle the resize event
            this.SizeChanged += new EventHandler(CommandGroupControl_SizeChanged);
        }

        void CommandGroupControl_SizeChanged(object sender, EventArgs e)
        {
            // Size our flow control panel
            this.flowLayoutPanel1.Size = new Size(this.Width, this.Height - 25);
            this.Location = new Point(0);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {   
            using (Brush backBrush = new LinearGradientBrush(e.ClipRectangle, _backColor1, _backColor2, LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(backBrush, e.ClipRectangle);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            // Do we have a valid rectangle to paint
            if (e.ClipRectangle.IsEmpty)
            {
                return;
            }

            // Draw title block on the top 
            Rectangle titleRect = new Rectangle(0, 0, this.Width, 25);

            Brush gradientTitleBrush = new LinearGradientBrush(titleRect, Color.Blue, Color.White, 270);
            e.Graphics.FillRectangle(gradientTitleBrush, titleRect);

            e.Graphics.DrawString(_title, this.Font, Brushes.White, 10, 8);

            // Draw a border around the contol
            Rectangle borderRect = this.DisplayRectangle;
            borderRect.Inflate(-1, -1);
            e.Graphics.DrawRectangle(Pens.DarkGray, borderRect);
        }

        private void FormatControls()
        {
            // Size and position our Flow Panel
            this.flowLayoutPanel1.Location = new Point(0, 25);
            this.flowLayoutPanel1.Size = new Size(this.Width, this.Height - 25);
        }

        private void LoadButtons(CommandButton[] buttons)
        {
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                flowLayoutPanel1.Controls.Clear();
            }

            // Load the current button array in the flow panel
            this.flowLayoutPanel1.Controls.AddRange(buttons);
        }

        private void BuildColorPalette()
        {
            _backColor1 = ColorTranslator.FromHtml("#2f82be");
            _backColor2 = ColorTranslator.FromHtml("#2fbe93");
        }
    }
}
