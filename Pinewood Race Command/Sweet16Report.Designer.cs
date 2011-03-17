namespace Pinewood_Race_Command
{
    partial class Sweet16Report
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonBracket = new System.Windows.Forms.RadioButton();
            this.radioButtonList = new System.Windows.Forms.RadioButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderSeed = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRacer = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCar = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAvgTime = new System.Windows.Forms.ColumnHeader();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonBracket);
            this.groupBox1.Controls.Add(this.radioButtonList);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 44);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View";
            // 
            // radioButtonBracket
            // 
            this.radioButtonBracket.AutoSize = true;
            this.radioButtonBracket.Location = new System.Drawing.Point(179, 16);
            this.radioButtonBracket.Name = "radioButtonBracket";
            this.radioButtonBracket.Size = new System.Drawing.Size(115, 17);
            this.radioButtonBracket.TabIndex = 1;
            this.radioButtonBracket.TabStop = true;
            this.radioButtonBracket.Text = "Show race bracket";
            this.radioButtonBracket.UseVisualStyleBackColor = true;
            // 
            // radioButtonList
            // 
            this.radioButtonList.AutoSize = true;
            this.radioButtonList.Location = new System.Drawing.Point(32, 18);
            this.radioButtonList.Name = "radioButtonList";
            this.radioButtonList.Size = new System.Drawing.Size(94, 17);
            this.radioButtonList.TabIndex = 0;
            this.radioButtonList.TabStop = true;
            this.radioButtonList.Text = "Show racer list";
            this.radioButtonList.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSeed,
            this.columnHeaderRacer,
            this.columnHeaderCar,
            this.columnHeaderAvgTime});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(12, 62);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(760, 495);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderSeed
            // 
            this.columnHeaderSeed.Text = "Seed";
            // 
            // columnHeaderRacer
            // 
            this.columnHeaderRacer.Text = "Racer\'s Name";
            this.columnHeaderRacer.Width = 444;
            // 
            // columnHeaderCar
            // 
            this.columnHeaderCar.Text = "Car #";
            this.columnHeaderCar.Width = 95;
            // 
            // columnHeaderAvgTime
            // 
            this.columnHeaderAvgTime.Text = "Qualifying Time";
            this.columnHeaderAvgTime.Width = 101;
            // 
            // Sweet16Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 564);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Sweet16Report";
            this.ShowInTaskbar = false;
            this.Text = "Sweet16Report";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonBracket;
        private System.Windows.Forms.RadioButton radioButtonList;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeaderSeed;
        private System.Windows.Forms.ColumnHeader columnHeaderRacer;
        private System.Windows.Forms.ColumnHeader columnHeaderCar;
        private System.Windows.Forms.ColumnHeader columnHeaderAvgTime;
    }
}