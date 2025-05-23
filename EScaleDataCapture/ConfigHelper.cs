using EScaleDataCapture.Properties;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace EScaleDataCapture
{
    public class ConfigHelper
    {
        private static Settings cig= new Settings();

        static ConfigHelper()
        {
            config_load();
        }
        
        private static void config_load()
        {
            cig.Reload();

            DataRetrievalTimeInterval = cig.DataRetrievalTimeInterval;
            DBConnection = cig.DBConnection;
            BaudRate = cig.BaudRate;
            ByteSize = cig.ByteSize;
            Parity = cig.Parity;
            StopBit= cig.StopBit;

            InitialIdentifier = cig.InitialIdentifier;
            FinalIdentifier = cig.FinalIdentifier.Replace("\\n", "\n").Replace("\\r", "\r"); ;

            OutTimerInterval = cig.OutTimerInterval;

        }

        private void config_save()
        {
            cig.DataRetrievalTimeInterval = 900;
            cig.DBConnection = "Data Source=database.db;";
            cig.BaudRate = "9600";
            cig.ByteSize = "8";
            cig.Parity = "None";
            cig.StopBit = "1";

            cig.Save();
        }

        public static int DataRetrievalTimeInterval { get; set; }
        public static string DBConnection { get; set; }
        public static string BaudRate { get; set; }
        public static string ByteSize { get; set; }
        public static string Parity { get; set; }
        public static string StopBit { get; set; }
        public static string InitialIdentifier { get; set; }
        public static string FinalIdentifier { get; set; }
        public static int OutTimerInterval { get; private set; }


    }
}
