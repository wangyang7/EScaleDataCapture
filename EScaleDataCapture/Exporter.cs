using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EScaleDataCapture
{
    public class Exporter
    {
        /// <summary>
        /// 使用 EPPlus 将 IEnumerable<EScaleModel> 导出为 Excel 文件
        /// </summary>
        public static void ExportToExcel(IEnumerable<EScaleModel> data, string filePath)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("EscaleData");

            // 写入表头
            IRow headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("Name");
            headerRow.CreateCell(1).SetCellValue("PortName");
            headerRow.CreateCell(2).SetCellValue("Weight");
            headerRow.CreateCell(3).SetCellValue("OperationTime");

            // 写入数据
            int rowIdx = 1;
            foreach (var item in data)
            {
                IRow row = sheet.CreateRow(rowIdx++);
                row.CreateCell(0).SetCellValue(item.Name);
                row.CreateCell(1).SetCellValue(item.PortName);
                row.CreateCell(2).SetCellValue(double.Parse(item.weight.ToString()));
                row.CreateCell(3).SetCellValue(((DateTime)item.OperationTime).ToString("G"));
            }

            // 保存文件
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(fs);
            }
        }

        public static void ExportToCsv(IEnumerable<EScaleModel> data, string filePath)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.Default))
            {
                // 写入表头
                writer.WriteLine("Name,PortName,Weight,OperationTime");

                // 写入数据
                foreach (var item in data)
                {
                    writer.WriteLine($"{item.Name},{item.PortName},{item.weight},{item.OperationTime:yyyy-MM-dd HH:mm:ss}");
                }
            }
        }
    }
}
