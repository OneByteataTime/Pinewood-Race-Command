namespace Pinewood_Race_Command
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.commandGroupControl1 = new Pinewood_Race_Command.controls.CommandGroupControl();
            this.commandButtonBuildRace = new Pinewood_Race_Command.controls.CommandButton();
            this.commandButtonLoadRace = new Pinewood_Race_Command.controls.CommandButton();
            this.commandButtonTestTrack = new Pinewood_Race_Command.controls.CommandButton();
            this.commandGroupControl2 = new Pinewood_Race_Command.controls.CommandGroupControl();
            this.commandButton1 = new Pinewood_Race_Command.controls.CommandButton();
            this.commandButton2 = new Pinewood_Race_Command.controls.CommandButton();
            this.commandButton6 = new Pinewood_Race_Command.controls.CommandButton();
            this.commandGroupControl3 = new Pinewood_Race_Command.controls.CommandGroupControl();
            this.commandButton3 = new Pinewood_Race_Command.controls.CommandButton();
            this.commandButton4 = new Pinewood_Race_Command.controls.CommandButton();
            this.commandButton5 = new Pinewood_Race_Command.controls.CommandButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Navy;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 518);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(488, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Navy;
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.commandGroupControl1);
            this.flowLayoutPanel1.Controls.Add(this.commandGroupControl2);
            this.flowLayoutPanel1.Controls.Add(this.commandGroupControl3);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(14, 14);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(458, 486);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // commandGroupControl1
            // 
            this.commandGroupControl1.Buttons = new Pinewood_Race_Command.controls.CommandButton[] {
        this.commandButtonBuildRace,
        this.commandButtonLoadRace,
        this.commandButtonTestTrack};
            this.commandGroupControl1.Location = new System.Drawing.Point(3, 3);
            this.commandGroupControl1.Name = "commandGroupControl1";
            this.commandGroupControl1.Size = new System.Drawing.Size(442, 159);
            this.commandGroupControl1.TabIndex = 5;
            this.commandGroupControl1.Title = "Race setup commands";
            // 
            // commandButtonBuildRace
            // 
            this.commandButtonBuildRace.BackColor = System.Drawing.Color.Lavender;
            this.commandButtonBuildRace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.commandButtonBuildRace.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.commandButtonBuildRace.FlatAppearance.BorderSize = 2;
            this.commandButtonBuildRace.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.commandButtonBuildRace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.commandButtonBuildRace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.commandButtonBuildRace.Image = ((System.Drawing.Image)(resources.GetObject("commandButtonBuildRace.Image")));
            this.commandButtonBuildRace.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.commandButtonBuildRace.Location = new System.Drawing.Point(10, 10);
            this.commandButtonBuildRace.Margin = new System.Windows.Forms.Padding(10);
            this.commandButtonBuildRace.Name = "commandButtonBuildRace";
            this.commandButtonBuildRace.Padding = new System.Windows.Forms.Padding(3);
            this.commandButtonBuildRace.Size = new System.Drawing.Size(125, 100);
            this.commandButtonBuildRace.TabIndex = 0;
            this.commandButtonBuildRace.Text = "Build New Race";
            this.commandButtonBuildRace.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.commandButtonBuildRace.UseVisualStyleBackColor = false;
            this.commandButtonBuildRace.Click += new System.EventHandler(this.commandButtonBuildRace_Click);
            // 
            // commandButtonLoadRace
            // 
            this.commandButtonLoadRace.BackColor = System.Drawing.Color.Lavender;
            this.commandButtonLoadRace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.commandButtonLoadRace.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.commandButtonLoadRace.FlatAppearance.BorderSize = 2;
            this.commandButtonLoadRace.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.commandButtonLoadRace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.commandButtonLoadRace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.commandButtonLoadRace.Image = ((System.Drawing.Image)(resources.GetObject("commandButtonLoadRace.Image")));
            this.commandButtonLoadRace.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.commandButtonLoadRace.Location = new System.Drawing.Point(155, 10);
            this.commandButtonLoadRace.Margin = new System.Windows.Forms.Padding(10);
            this.commandButtonLoadRace.Name = "commandButtonLoadRace";
            this.commandButtonLoadRace.Padding = new System.Windows.Forms.Padding(3);
            this.commandButtonLoadRace.Size = new System.Drawing.Size(125, 100);
            this.commandButtonLoadRace.TabIndex = 1;
            this.commandButtonLoadRace.Text = "Load Existing Race";
            this.commandButtonLoadRace.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.commandButtonLoadRace.UseVisualStyleBackColor = false;
            this.commandButtonLoadRace.Click += new System.EventHandler(this.commandButtonLoadRace_Click);
            // 
            // commandButtonTestTrack
            // 
            this.commandButtonTestTrack.BackColor = System.Drawing.Color.Lavender;
            this.commandButtonTestTrack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.commandButtonTestTrack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.commandButtonTestTrack.FlatAppearance.BorderSize = 2;
            this.commandButtonTestTrack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.commandButtonTestTrack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.commandButtonTestTrack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.commandButtonTestTrack.Image = ((System.Drawing.Image)(resources.GetObject("commandButtonTestTrack.Image")));
            this.commandButtonTestTrack.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.commandButtonTestTrack.Location = new System.Drawing.Point(300, 10);
            this.commandButtonTestTrack.Margin = new System.Windows.Forms.Padding(10);
            this.commandButtonTestTrack.Name = "commandButtonTestTrack";
            this.commandButtonTestTrack.Padding = new System.Windows.Forms.Padding(3);
            this.commandButtonTestTrack.Size = new System.Drawing.Size(125, 100);
            this.commandButtonTestTrack.TabIndex = 2;
            this.commandButtonTestTrack.Text = "Test Track";
            this.commandButtonTestTrack.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.commandButtonTestTrack.UseVisualStyleBackColor = false;
            this.commandButtonTestTrack.Click += new System.EventHandler(this.commandButtonTestTrack_Click);
            // 
            // commandGroupControl2
            // 
            this.commandGroupControl2.Buttons = new Pinewood_Race_Command.controls.CommandButton[] {
        this.commandButton1,
        this.commandButton2,
        this.commandButton6};
            this.commandGroupControl2.Location = new System.Drawing.Point(3, 168);
            this.commandGroupControl2.Name = "commandGroupControl2";
            this.commandGroupControl2.Size = new System.Drawing.Size(442, 156);
            this.commandGroupControl2.TabIndex = 6;
            this.commandGroupControl2.Title = "Race Commands";
            // 
            // commandButton1
            // 
            this.commandButton1.BackColor = System.Drawing.Color.Lavender;
            this.commandButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.commandButton1.FlatAppearance.BorderSize = 2;
            this.commandButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.commandButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.commandButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.commandButton1.Image = ((System.Drawing.Image)(resources.GetObject("commandButton1.Image")));
            this.commandButton1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.commandButton1.Location = new System.Drawing.Point(10, 10);
            this.commandButton1.Margin = new System.Windows.Forms.Padding(10);
            this.commandButton1.Name = "commandButton1";
            this.commandButton1.Padding = new System.Windows.Forms.Padding(3);
            this.commandButton1.Size = new System.Drawing.Size(125, 100);
            this.commandButton1.TabIndex = 0;
            this.commandButton1.Text = "Run Prelim";
            this.commandButton1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.commandButton1.UseVisualStyleBackColor = false;
            this.commandButton1.Click += new System.EventHandler(this.commandButton1_Click);
            // 
            // commandButton2
            // 
            this.commandButton2.BackColor = System.Drawing.Color.Lavender;
            this.commandButton2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.commandButton2.FlatAppearance.BorderSize = 2;
            this.commandButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.commandButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.commandButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.commandButton2.Image = ((System.Drawing.Image)(resources.GetObject("commandButton2.Image")));
            this.commandButton2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.commandButton2.Location = new System.Drawing.Point(155, 10);
            this.commandButton2.Margin = new System.Windows.Forms.Padding(10);
            this.commandButton2.Name = "commandButton2";
            this.commandButton2.Padding = new System.Windows.Forms.Padding(3);
            this.commandButton2.Size = new System.Drawing.Size(125, 100);
            this.commandButton2.TabIndex = 1;
            this.commandButton2.Text = "Run Sweet 16";
            this.commandButton2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.commandButton2.UseVisualStyleBackColor = false;
            this.commandButton2.Click += new System.EventHandler(this.commandButton2_Click);
            // 
            // commandButton6
            // 
            this.commandButton6.BackColor = System.Drawing.Color.Lavender;
            this.commandButton6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.commandButton6.FlatAppearance.BorderSize = 2;
            this.commandButton6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.commandButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.commandButton6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.commandButton6.Image = ((System.Drawing.Image)(resources.GetObject("commandButton6.Image")));
            this.commandButton6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.commandButton6.Location = new System.Drawing.Point(300, 10);
            this.commandButton6.Margin = new System.Windows.Forms.Padding(10);
            this.commandButton6.Name = "commandButton6";
            this.commandButton6.Padding = new System.Windows.Forms.Padding(3);
            this.commandButton6.Size = new System.Drawing.Size(125, 100);
            this.commandButton6.TabIndex = 2;
            this.commandButton6.Text = "Restore Race";
            this.commandButton6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.commandButton6.UseVisualStyleBackColor = false;
            this.commandButton6.Click += new System.EventHandler(this.commandButton6_Click);
            // 
            // commandGroupControl3
            // 
            this.commandGroupControl3.Buttons = new Pinewood_Race_Command.controls.CommandButton[] {
        this.commandButton3,
        this.commandButton4,
        this.commandButton5};
            this.commandGroupControl3.Location = new System.Drawing.Point(3, 330);
            this.commandGroupControl3.Name = "commandGroupControl3";
            this.commandGroupControl3.Size = new System.Drawing.Size(442, 147);
            this.commandGroupControl3.TabIndex = 7;
            this.commandGroupControl3.Title = "Reports";
            // 
            // commandButton3
            // 
            this.commandButton3.BackColor = System.Drawing.Color.White;
            this.commandButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.commandButton3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.commandButton3.Location = new System.Drawing.Point(10, 10);
            this.commandButton3.Margin = new System.Windows.Forms.Padding(10);
            this.commandButton3.Name = "commandButton3";
            this.commandButton3.Padding = new System.Windows.Forms.Padding(3);
            this.commandButton3.Size = new System.Drawing.Size(125, 100);
            this.commandButton3.TabIndex = 0;
            this.commandButton3.Text = "Average Times";
            this.commandButton3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.commandButton3.UseVisualStyleBackColor = true;
            this.commandButton3.Click += new System.EventHandler(this.commandButton3_Click);
            // 
            // commandButton4
            // 
            this.commandButton4.BackColor = System.Drawing.Color.White;
            this.commandButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.commandButton4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.commandButton4.Location = new System.Drawing.Point(155, 10);
            this.commandButton4.Margin = new System.Windows.Forms.Padding(10);
            this.commandButton4.Name = "commandButton4";
            this.commandButton4.Padding = new System.Windows.Forms.Padding(3);
            this.commandButton4.Size = new System.Drawing.Size(125, 100);
            this.commandButton4.TabIndex = 1;
            this.commandButton4.Text = "Sweet 16";
            this.commandButton4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.commandButton4.UseVisualStyleBackColor = true;
            this.commandButton4.Click += new System.EventHandler(this.commandButton4_Click);
            // 
            // commandButton5
            // 
            this.commandButton5.BackColor = System.Drawing.Color.White;
            this.commandButton5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.commandButton5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.commandButton5.Location = new System.Drawing.Point(300, 10);
            this.commandButton5.Margin = new System.Windows.Forms.Padding(10);
            this.commandButton5.Name = "commandButton5";
            this.commandButton5.Padding = new System.Windows.Forms.Padding(3);
            this.commandButton5.Size = new System.Drawing.Size(125, 100);
            this.commandButton5.TabIndex = 2;
            this.commandButton5.Text = "Final Results";
            this.commandButton5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.commandButton5.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 540);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pinewood Derby Race  Command";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Pinewood_Race_Command.controls.CommandGroupControl commandGroupControl1;
        private Pinewood_Race_Command.controls.CommandButton commandButtonBuildRace;
        private Pinewood_Race_Command.controls.CommandButton commandButtonLoadRace;
        private Pinewood_Race_Command.controls.CommandButton commandButtonTestTrack;
        private Pinewood_Race_Command.controls.CommandGroupControl commandGroupControl2;
        private Pinewood_Race_Command.controls.CommandButton commandButton1;
        private Pinewood_Race_Command.controls.CommandButton commandButton2;
        private Pinewood_Race_Command.controls.CommandGroupControl commandGroupControl3;
        private Pinewood_Race_Command.controls.CommandButton commandButton3;
        private Pinewood_Race_Command.controls.CommandButton commandButton4;
        private Pinewood_Race_Command.controls.CommandButton commandButton5;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Pinewood_Race_Command.controls.CommandButton commandButton6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

