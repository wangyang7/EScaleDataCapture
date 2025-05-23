using EScaleDataCapture.Control;
using NPOI.SS.Formula.Functions;
using SixLabors.ImageSharp.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EScaleDataCapture
{
    public partial class Form1 : Form
    {
        Dictionary<string, System.IO.Ports.SerialPort> serialPorts =new Dictionary<string, System.IO.Ports.SerialPort> ();
        Dictionary<string, string> PortNames = new Dictionary<string, string>();
        Dictionary<string,EScaleModel> Weights = new Dictionary<string, EScaleModel>();
        Dictionary<string, DateTime?> Operatimes = new Dictionary<string, DateTime?>();
        Dictionary<string, int> waitflags = new Dictionary<string, int>();
        Dictionary<string, StringBuilder> receivedDataBuffers = new Dictionary<string, StringBuilder>();
        int cbComPortIndex = 7;

        int tn_show_time = 0;

        public Form1()
        {
            InitializeComponent();

            timer1.Interval = ConfigHelper.OutTimerInterval;


            List<string> port_names = SerialPort.GetPortNames().ToList();
            port_names.Sort(new CustomComparer());

            string[] baud_rates = { /*"110", "300", "600",*/ "1200", "2400", "4800", "9600", "14400", "19200", "38400", /*"56000",*/ "57600", "115200", "230400", "460800", "921600"/*"128000", "256000"*/ };

            List<System.IO.Ports.SerialPort> aryserialPorts =new List<SerialPort>() { serialPort1, serialPort2, serialPort3, serialPort4, serialPort5, serialPort6, serialPort7, serialPort8 };

            string[] paritys = { "None", "Odd", "Even", "Mark", "Space" };

            string[] data_bits = { "5", "6", "7", "8" };

            string[] stop_bits = { "1", "1.5", "2" };

            foreach(System.IO.Ports.SerialPort sp  in aryserialPorts)
            {
                sp.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            }


            foreach (System.Windows.Forms.Control groupc in this.splitContainer1.Panel1.Controls)
            {
                CheckableGroupBox tempchkgroup;

                if(groupc.Tag == null || groupc.Tag.ToString() !=  "checkableGroupBox")
                {
                    continue;
                }

                tempchkgroup = groupc as  CheckableGroupBox;

                serialPorts.Add(tempchkgroup.Name, aryserialPorts[0]);
                aryserialPorts.RemoveAt(0);


                foreach (System.Windows.Forms.Control control in tempchkgroup.Controls)
                {
                    ComboBox tempcb;
                    CheckBox tempbtn;
                    if (control.Tag == null)
                    {

                    }
                    else if (control.Tag.ToString() == "cbComPort")
                    {
                        tempcb = control as ComboBox;
                        tempcb.Items.AddRange(port_names.ToArray());
                        tempcb.SelectedIndex = tempcb.Items.Count > cbComPortIndex ? cbComPortIndex : -1;
                        cbComPortIndex--;
                    }
                    else if (control.Tag.ToString() == "cbBaudRate")
                    {
                        tempcb = control as ComboBox;
                        tempcb.Items.AddRange(baud_rates);
                        //tempcb.Text = "9600";
                        tempcb.Text = ConfigHelper.BaudRate;
                    }
                    else if (control.Tag.ToString() == "cbParity")
                    {
                        tempcb = control as ComboBox;
                        tempcb.Items.AddRange(paritys);
                        //tempcb.Text = "None";
                        tempcb.Text = ConfigHelper.Parity;
                    }
                    else if (control.Tag.ToString() == "cbByteSize")
                    {
                        tempcb = control as ComboBox;
                        tempcb.Items.AddRange(data_bits);
                        //tempcb.Text = "8";
                        tempcb.Text = ConfigHelper.ByteSize;
                    }
                    else if (control.Tag.ToString() == "cbStopBits")
                    {
                        tempcb = control as ComboBox;
                        tempcb.Items.AddRange(stop_bits);
                        //tempcb.Text = "1";
                        tempcb.Text = ConfigHelper.StopBit;
                    }
                    else if (control.Tag.ToString() == "btnOpen")
                    {
                        tempbtn = control as CheckBox;
                        tempbtn.CheckedChanged += new System.EventHandler(btnOpens_CheckedChanged);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            BL.CreateTable();
        }

        private void btnOpens_CheckedChanged(object sender, EventArgs e)
        {
            
            SerialPort tmpSerialPort = null;
            ComboBox chktempComPort=null;
            ComboBox chktempBaudRate = null;
            ComboBox chktempParity = null;
            ComboBox chktempByteSize = null;
            ComboBox chktempStopBits = null;

            CheckBox chk = sender as CheckBox;


            tmpSerialPort = serialPorts[chk.Parent.Name];

            foreach (System.Windows.Forms.Control control in chk.Parent.Controls)
            {
                if (control.Tag == null)
                {

                }
                else if (control.Tag.ToString() == "cbComPort")
                {
                    chktempComPort = control as ComboBox;
                }
                else if (control.Tag.ToString() == "cbBaudRate")
                {
                    chktempBaudRate = control as ComboBox;
                }
                else if (control.Tag.ToString() == "cbParity")
                {
                    chktempParity = control as ComboBox;

                }
                else if (control.Tag.ToString() == "cbByteSize")
                {
                    chktempByteSize = control as ComboBox;
                }
                else if (control.Tag.ToString() == "cbStopBits")
                {
                    chktempStopBits = control as ComboBox;
                }
            }

            if(chktempComPort==null || chktempBaudRate==null || chktempParity == null || chktempByteSize == null || chktempStopBits == null)
            {
                return;
            }

            if (chk.Text == "Open")
            {

                if(chktempComPort.Text==string.Empty)
                {
                    MessageBox.Show("​​Please select a ComPort");
                    return;
                }

                PortOpen(tmpSerialPort,
                         chktempComPort.Text,
                         chktempBaudRate.Text,
                         chktempParity.Text,
                         chktempByteSize.Text,
                         chktempStopBits.SelectedIndex);

                PortNames.Add(tmpSerialPort.PortName, chk.Parent.Text);
            }
            else
            {
                PortClose(tmpSerialPort);
                PortNames.Remove(tmpSerialPort.PortName);
                Operatimes.Remove(tmpSerialPort.PortName);
                Weights.Remove(tmpSerialPort.PortName);
                receivedDataBuffers.Remove(tmpSerialPort.PortName);
            }

            if (tmpSerialPort.IsOpen)
            {
                chk.Text = "Close";

                chktempComPort.Enabled = false;
                chktempBaudRate.Enabled = false;
                chktempParity.Enabled = false;
                chktempByteSize.Enabled = false;
                chktempStopBits.Enabled = false;
            }
            else
            {
                chk.Text = "Open";

                chktempComPort.Enabled = true;
                chktempBaudRate.Enabled = true;
                chktempParity.Enabled = true;
                chktempByteSize.Enabled = true;
                chktempStopBits.Enabled = true;
            }
        }


        private void PortOpen(System.IO.Ports.SerialPort serialPort,
                                                  string strPortName,
                                                  string strBaudRate,
                                                  string strParity,
                                                  string strDataBits,
                                                  int    intStopBit)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }

                serialPort.PortName = strPortName;
                serialPort.BaudRate = int.Parse(strBaudRate);
                serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), strParity);
                serialPort.DataBits = int.Parse(strDataBits);

                StopBits[] stop_bits = { StopBits.One, StopBits.OnePointFive, StopBits.Two };
                serialPort.StopBits = stop_bits[intStopBit];
                
                serialPort.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PortClose(System.IO.Ports.SerialPort serialPort)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
            }
            catch
            {

            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            SerialPort sp = (SerialPort)sender;

            byte[] buffer = null;
            buffer = new byte[sp.BytesToRead];
            sp.Read(buffer, 0, buffer.Length);

            // 转换为字符串（根据实际编码调整）
            string receivedData = Encoding.Default.GetString(buffer);

            //this.BeginInvoke((EventHandler)(delegate
            //{
            //    Console.WriteLine(System.Text.RegularExpressions.Regex.Escape(receivedData));
            //}));

            lock (receivedDataBuffers)
            {
                if (receivedDataBuffers.Keys.Contains(sp.PortName))
                {
                    receivedDataBuffers[sp.PortName].Append(receivedData);
                }
                else
                {
                    receivedDataBuffers.Add(sp.PortName, new StringBuilder(receivedData));
                }
            }

            ProcessReceivedData(sp);
        }

        private void ProcessReceivedData(SerialPort sp)
        {
            // 假设数据包以"\n"结尾
            string bufferContent = receivedDataBuffers[sp.PortName].ToString();

            if (!string.IsNullOrEmpty(ConfigHelper.InitialIdentifier))
            {
                int endIndex = bufferContent.IndexOf(ConfigHelper.FinalIdentifier);

                while (endIndex >= 0)
                {
                    // 提取完整数据包（包含换行符）
                    string completePacket = bufferContent.Substring(0, endIndex + 1);

                    completePacket = completePacket.Substring(completePacket.IndexOf(ConfigHelper.InitialIdentifier) + 1).Trim();

                    // 处理完整数据包
                    this.BeginInvoke((EventHandler)(delegate
                    {
                        EScaleModel es = GetEScaleModelFromSerialPort(sp, completePacket);

                        if (string.IsNullOrEmpty(es.Name)) return;

                        //if (es.OperationTime == null)
                        //{
                        //    Weights[sp.PortName].weight = Weights[sp.PortName].weight + es.weight;

                        //    return;
                        //}

                        lock (Weights)
                        {
                            if (Weights.Keys.Contains(sp.PortName))
                            {
                                Weights[sp.PortName] = es;
                            }
                            else
                            {
                                Weights.Add(sp.PortName, es);
                                Operatimes.Add(sp.PortName, null);
                            }
                        }

                    }));

                    lock (receivedDataBuffers)
                    {
                        // 从缓冲区移除已处理数据
                        receivedDataBuffers[sp.PortName].Remove(0, endIndex + 1);
                    }

                    // 更新缓冲区内容
                    bufferContent = receivedDataBuffers[sp.PortName].ToString();
                    endIndex = bufferContent.IndexOf('\n');
                }
            }
            else
            {
                this.BeginInvoke((EventHandler)(delegate
                {


                    EScaleModel es = GetEScaleModelFromSerialPort(sp, bufferContent,10);

                    if (string.IsNullOrEmpty(es.Name)) return;

                    if (es.OperationTime == null)
                    //if(waitflags.Keys.Contains(sp.PortName) && waitflags[sp.PortName] == true)
                    {
                        Weights[sp.PortName].weight = Weights[sp.PortName].weight + es.weight;
                        
                        return;
                    }

                    lock (Weights)
                    {
                        if (Weights.Keys.Contains(sp.PortName))
                        {
                            Weights[sp.PortName] = es;
                        }
                        else
                        {
                            Weights.Add(sp.PortName, es);
                            Operatimes.Add(sp.PortName, null);
                        }
                    }

                    //Thread.Sleep(1);

                }));

                lock (receivedDataBuffers)
                {
                    // 从缓冲区移除已处理数据
                    receivedDataBuffers[sp.PortName].Clear();
                }

            }
        }

        private EScaleModel GetEScaleModelFromSerialPort(SerialPort sp, string completePacket, int tn = 0)
        {
            EScaleModel result = new EScaleModel();

            if(!waitflags.Keys.Contains(sp.PortName))
            {
                waitflags.Add(sp.PortName, 0);
            }

            try
            {
                if (completePacket != null && completePacket.Length > 0)
                {
                    if (waitflags[sp.PortName] == 0)
                    {
                        result.OperationTime = DateTime.Now;
                    }
                    else { 
                        
                    }
                    waitflags[sp.PortName] = tn;//10
                    result.Name = PortNames[sp.PortName];
                    result.PortName = sp.PortName;
                    result.weight = completePacket;
                        
                }

                //sp.DiscardInBuffer();
            }
            catch (FormatException fex)
            {
                return new EScaleModel();
            }
            catch
            {
                throw;
            }
            return result;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            lock (Weights)
            {
                foreach (string d in Weights.Keys)
                {
                    if (Operatimes.Keys.Contains(d) && (Operatimes[d] == null || DateTime.Now > Operatimes[d].Value.AddSeconds(ConfigHelper.DataRetrievalTimeInterval)))
                    {
                        OutRichText(Weights[d]);
                        BL.SaveData(Weights[d]);
                        Operatimes[d] = DateTime.Now;
                    }
                }
            }

            timer1.Enabled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }

        private void OutRichText(EScaleModel es,bool flag=false)
        {
            if (richTextBox1.Lines.Length > 200)
            {
                // 选中第一行
                richTextBox1.Select(0, richTextBox1.GetFirstCharIndexFromLine(1));
                // 删除选中内容
                richTextBox1.SelectedText = string.Empty;
            }
            if (!flag)
            {
                richTextBox1.AppendText($"{es.Name} {es.PortName} {es.weight} {es.OperationTime} {Environment.NewLine}");
            }
            else {
                richTextBox1.AppendText($"{System.Text.RegularExpressions.Regex.Escape(es.weight)}  {Environment.NewLine}");
               
            }
            richTextBox1.SelectionStart = richTextBox1.TextLength; // 将光标移到末尾
            richTextBox1.ScrollToCaret();
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            FormExport t= new FormExport();
            t.ShowDialog();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            string[] tmp =  waitflags.Keys.ToArray();


            foreach (string key in tmp)
            {
                if (waitflags[key] > 0)
                {
                    waitflags[key]--;
                }
            }
            
            
        }

    }
}
