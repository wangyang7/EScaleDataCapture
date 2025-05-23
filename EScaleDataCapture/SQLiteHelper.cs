using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;

namespace EScaleDataCapture
{
    public static class SQLiteHelper
    {
        private static string _connectionString = "Data Source=db.db;";

        /// <summary>
        /// 设置连接字符串
        /// </summary>
        public static void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
            
        }

        /// <summary>
        /// 执行非查询SQL
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// </summary>
        public static int Execute(string sql, object param = null, IDbTransaction transaction = null)
        {
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                return conn.Execute(sql, param, transaction);
            }
        }

        /// <summary>
        /// 查询单个实体
        /// </summary>
        public static T QueryFirst<T>(string sql, object param = null)
        {
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<T>(sql, param);
            }
        }

        /// <summary>
        /// 查询多个实体
        /// </summary>
        public static IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<T>(sql, param);
            }
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        public static void ExecuteTransaction(Action<IDbTransaction> action)
        {
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        action(transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
