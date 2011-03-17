using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.IO.Ports;

namespace Pinewood_Race_Command
{
    public partial class FormTestTrack : Form
    {
        private StringBuilder _serialResults;

        public FormTestTrack()
        {
            InitializeComponent();
            _serialResults = new StringBuilder();
        }

        private void FormTestTrack_Load(object sender, EventArgs e)
        {
            // Default the serial comminications to the values 
            // that work for our existing track
            this.serialPort1.BaudRate = 1200;
            this.serialPort1.PortName = "COM4";

            this.propertyGrid1.SelectedObject = this.serialPort1;

            // Hook into the read event
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort1_DataReceived);
            this.serialPort1.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(serialPort1_ErrorReceived);

            // Initialize our status bar text
            this.toolStripStatusLabel1.Text = "Ready to test track communications";
        }

        private void buttonInit_Click(object sender, EventArgs e)
        {
            try
            {
                this.serialPort1.Open();

                this.toolStripStatusLabel1.Text = "Serial Port communication established...";
            }
            catch (Exception exp)
            {
                this.toolStripStatusLabel1.Text = exp.Message;

                MessageBox.Show(this, exp.Message, "Hardware problem...",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void serialPort1_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            this.toolStripStatusLabel1.Text = "Error event trapped from Serial Port";

            MessageBox.Show(this,"Error received", "Serial Port Event", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            this.ProcessComDataStream(this.serialPort1.ReadExisting());
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            // if our COM port is open close it
            if (this.serialPort1.IsOpen)
            {
                this.serialPort1.Close();
            }

            // Store our active serial port settings
            RaceDataStore.TrackSettings = this.serialPort1;

            this.Close();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            // Hook into the return data event handler
            this.serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);

            // Clear out any exiting data
            //this.InitRaceData();
            _serialResults = new StringBuilder();

            // Have we opened the serial port?
            if (this.serialPort1.IsOpen == false)
            {
                this.buttonInit_Click(null, new EventArgs());
            }

            // Is the Serial port open now?
            if (this.serialPort1.IsOpen)
            {
                // Start the race
                this.serialPort1.Write("G");

                this.toolStripStatusLabel1.Text = "Sent 'Start Race' command";
            }
            else
            {
                MessageBox.Show(this, "There is a problem opening the serial port for communication. \n\r\n\rPlease adjust the serial port communication properties and try again.", "Serial communication error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessComDataStream(string input)
        {
            // Append this to our existing serial results
            _serialResults.Append(input);

            string raceResults = _serialResults.ToString();

            // Check to see if we are done?
            if (raceResults.IndexOf("Waiting to Start") > 0)
            {
                // Unhook our event
                this.serialPort1.DataReceived -= new SerialDataReceivedEventHandler(serialPort1_DataReceived);

                this.toolStripStatusLabel1.Text = "Test Complete!";

                // Parse our race results
                MessageBox.Show(this, _serialResults.ToString(), "Serial Port Results", MessageBoxButtons.OK, MessageBoxIcon.Information);


                Console.WriteLine(_serialResults.ToString());
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (this.serialPort1.IsOpen)
            {
                this.serialPort1.DiscardInBuffer();
                this.serialPort1.DiscardOutBuffer();
                
                this.serialPort1.Close();
            }

            this.toolStripStatusLabel1.Text = "Serial port has been reset.";
        }
    }
}
