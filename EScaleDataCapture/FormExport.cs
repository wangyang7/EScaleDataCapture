using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EScaleDataCapture
{
    public partial class FormExport : Form
    {
        public FormExport()
        {
            InitializeComponent();

            sDate.Value = DateTime.Now.Date.AddDays(-14);
            eDate.Value = DateTime.Now.Date;

            InitializeDataGridView();
           
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
           
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            // 合并日期和时间
            DateTime sDateTime = sDate.Value.Add(sTime.Value.TimeOfDay);
            DateTime eDateTime = eDate.Value.Add(eTime.Value.TimeOfDay);
            
            int count = BL.GetCount(sDateTime, eDateTime);

            if (count > 1000)
            {
                MessageBox.Show($"Display only the last 1,000 out of {count.ToString("N0", CultureInfo.InvariantCulture)} pieces of data.");
            }

            dataGridView1.DataSource = BL.GetDatas(sDateTime,eDateTime);
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "Escaledate." + (rbCSV.Checked == true ? "csv":"xlsx");

            if (saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                
                if(File.Exists(saveFileDialog1.FileName))
                {
                    File.Delete(saveFileDialog1.FileName);
                    //MessageBox.Show("The target file already exists and cannot be generated. Please change the path or file name.");
                    //return;
                }

                DateTime sDateTime = sDate.Value.Add(sTime.Value.TimeOfDay);
                DateTime eDateTime = eDate.Value.Add(eTime.Value.TimeOfDay);

                var data = BL.GetDatas(sDateTime, eDateTime);


                if (rbCSV.Checked)
                {
                    Exporter.ExportToCsv(data,saveFileDialog1.FileName);
                }
                else
                {
                    Exporter.ExportToExcel(data, saveFileDialog1.FileName);
                }
            }
        }

        private void InitializeDataGridView()
        {
            //dataGridView1.VirtualMode = true;
            dataGridView1.AutoGenerateColumns = true;

        }

        private object GetDataFromDatabase(int RowIndex, int ColumnIndex)
        { 
            object result = null;



            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Warning: All data will be deleted if confirmed.", "Warning",MessageBoxButtons.OKCancel)==DialogResult.Cancel)
            {
                return;
            }

            if (MessageBox.Show("Warning: Please confirm whether the data has been backed up..", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            BL.DelDatas();


        }
    }
}
