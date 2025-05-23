using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EScaleDataCapture
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            SQLitePCL.Batteries_V2.Init();

           // SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            
            //SQLitePCL.Batteries.Init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // 捕获 UI 线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            // 捕获非 UI 线程异常（见下文）
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.Run(new Form1());

            //Application.Run(new FormExport());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            // 记录日志
            

            // 显示友好错误信息
            MessageBox.Show($"发生错误: {e.Exception.Message}\n请联系管理员。\n\n错误详情已记录。",
                           "应用程序错误",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
        }

        // 非 UI 线程异常处理（见下文）
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // 实现见下文
        }
    }
}
