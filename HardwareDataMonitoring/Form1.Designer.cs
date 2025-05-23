namespace HardwareDataMonitoring
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnOpen1 = new System.Windows.Forms.CheckBox();
            this.cbStopBits1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbByteSize1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbParity1 = new System.Windows.Forms.ComboBox();
            this.cbBaudRate1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbComPort1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnOpen1
            // 
            this.btnOpen1.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnOpen1.AutoSize = true;
            this.btnOpen1.Location = new System.Drawing.Point(336, 52);
            this.btnOpen1.Margin = new System.Windows.Forms.Padding(10);
            this.btnOpen1.MinimumSize = new System.Drawing.Size(130, 0);
            this.btnOpen1.Name = "btnOpen1";
            this.btnOpen1.Size = new System.Drawing.Size(130, 22);
            this.btnOpen1.TabIndex = 23;
            this.btnOpen1.Tag = "btnOpen";
            this.btnOpen1.Text = "Open";
            this.btnOpen1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOpen1.UseVisualStyleBackColor = true;
            this.btnOpen1.CheckedChanged += new System.EventHandler(this.btnOpen1_CheckedChanged);
            // 
            // cbStopBits1
            // 
            this.cbStopBits1.FormattingEnabled = true;
            this.cbStopBits1.Location = new System.Drawing.Point(236, 54);
            this.cbStopBits1.Name = "cbStopBits1";
            this.cbStopBits1.Size = new System.Drawing.Size(86, 20);
            this.cbStopBits1.TabIndex = 22;
            this.cbStopBits1.Tag = "cbStopBits";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(177, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "StopBits:";
            // 
            // cbByteSize1
            // 
            this.cbByteSize1.FormattingEnabled = true;
            this.cbByteSize1.Location = new System.Drawing.Point(86, 54);
            this.cbByteSize1.Name = "cbByteSize1";
            this.cbByteSize1.Size = new System.Drawing.Size(86, 20);
            this.cbByteSize1.TabIndex = 20;
            this.cbByteSize1.Tag = "cbByteSize";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "ByteSize:";
            // 
            // cbParity1
            // 
            this.cbParity1.FormattingEnabled = true;
            this.cbParity1.Location = new System.Drawing.Point(379, 17);
            this.cbParity1.Name = "cbParity1";
            this.cbParity1.Size = new System.Drawing.Size(86, 20);
            this.cbParity1.TabIndex = 18;
            this.cbParity1.Tag = "cbParity";
            // 
            // cbBaudRate1
            // 
            this.cbBaudRate1.FormattingEnabled = true;
            this.cbBaudRate1.Location = new System.Drawing.Point(236, 17);
            this.cbBaudRate1.Name = "cbBaudRate1";
            this.cbBaudRate1.Size = new System.Drawing.Size(86, 20);
            this.cbBaudRate1.TabIndex = 17;
            this.cbBaudRate1.Tag = "cbBaudRate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(328, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "Parity:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "BaudRate:";
            // 
            // cbComPort1
            // 
            this.cbComPort1.FormattingEnabled = true;
            this.cbComPort1.Location = new System.Drawing.Point(86, 17);
            this.cbComPort1.Name = "cbComPort1";
            this.cbComPort1.Size = new System.Drawing.Size(86, 20);
            this.cbComPort1.TabIndex = 14;
            this.cbComPort1.Tag = "cbComPort";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "Com Port:";
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(29, 93);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(698, 390);
            this.richTextBox1.TabIndex = 24;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 512);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnOpen1);
            this.Controls.Add(this.cbStopBits1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbByteSize1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbParity1);
            this.Controls.Add(this.cbBaudRate1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbComPort1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox btnOpen1;
        private System.Windows.Forms.ComboBox cbStopBits1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbByteSize1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbParity1;
        private System.Windows.Forms.ComboBox cbBaudRate1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbComPort1;
        private System.Windows.Forms.Label label1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

