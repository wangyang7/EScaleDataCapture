using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EScaleDataCapture
{
    public class BL
    {
        static BL() 
        {
            SQLiteHelper.SetConnectionString(ConfigHelper.DBConnection);
        }

        public static void SaveData(EScaleModel es)
        {
            // 插入单个产品
            SQLiteHelper.Execute(
                "INSERT INTO EscaleDatas (Name, PortName, weight,OperationTime) VALUES (@Name, @PortName, @Weight,@OperationTime)",
                es);

            
        }

        public static void CreateTable()
        {
            // 插入单个产品
            SQLiteHelper.Execute( @"
            SELECT name FROM sqlite_master 
            WHERE type='table' AND name='EscaleDatas';

            CREATE TABLE IF NOT EXISTS EscaleDatas (
                Name TEXT NOT NULL,        
                PortName TEXT NOT NULL,    
                weight REAL,              
                OperationTime DATETIME     
            )");


        }

        public static int GetCount(DateTime sd,DateTime ed)
        {
            
            // 查询数量
            var count = SQLiteHelper.QueryFirst<int>(
                "SELECT COUNT(*) FROM EscaleDatas WHERE  OperationTime BETWEEN @sd AND @ed ORDER BY OperationTime DESC",
                new { sd=sd,ed=ed });
            

            return count;
        }

        public static IEnumerable<EScaleModel> GetDatas(DateTime sd, DateTime ed)
        {

            // 查询数量
            var result = SQLiteHelper.Query<EScaleModel>(
                "SELECT ROW_NUMBER() OVER (ORDER BY OperationTime DESC) AS RowNum, Name, PortName,  weight, OperationTime FROM EscaleDatas WHERE  OperationTime BETWEEN @sd AND @ed ORDER BY OperationTime DESC LIMIT 1000;",
                new { sd = sd, ed = ed });


            return result;
        }

        public static void DelDatas()
        {

            // 查询数量
            var result = SQLiteHelper.Execute(
                "DELETE FROM EscaleDatas");


            if(result<=0)
            {
                throw new Exception("Delete Fail!");
            }
        }
    }
}
