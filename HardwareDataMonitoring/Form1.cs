using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HardwareDataMonitoring
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            List<string> port_names = SerialPort.GetPortNames().ToList();

            string[] baud_rates = { /*"110", "300", "600",*/ "1200", "2400", "4800", "9600", "14400", "19200", "38400", /*"56000",*/ "57600", "115200", "230400", "460800", "921600"/*"128000", "256000"*/ };

            string[] paritys = { "None", "Odd", "Even", "Mark", "Space" };
            string[] data_bits = { "5", "6", "7", "8" };

            string[] stop_bits = { "1", "1.5", "2" };

            cbComPort1.Items.AddRange(port_names.ToArray());
            cbComPort1.SelectedIndex = 0;

            cbBaudRate1.Items.AddRange(baud_rates);
            cbBaudRate1.Text = "9600";

            cbParity1.Items.AddRange(paritys);
            cbParity1.Text = "None";

            cbByteSize1.Items.AddRange(data_bits);
            cbByteSize1.Text = "8";

            cbStopBits1.Items.AddRange(stop_bits);
            cbStopBits1.Text = "1";
        }

        private void btnOpen1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }
    }
}
