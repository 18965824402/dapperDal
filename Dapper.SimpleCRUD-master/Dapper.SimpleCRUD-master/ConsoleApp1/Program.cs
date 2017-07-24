using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 40000; i++)
            {
                Thread thread = new Thread(new System.Threading.ParameterizedThreadStart(ss));

                thread.Start(i);
                Console.WriteLine(i);
            }



            Console.ReadKey();

        }
      
        static void ss(object arg)
        {

            OrderOperationLog OrderOperationLog = new OrderOperationLog();
            OrderOperationLog.BillNo = "1470638701252";
            OrderOperationLog.Message = "订单状态 “执行中”→“开始服务”";
            OrderOperationLog.Title = "确认开始服务";
            OrderOperationLog.IP = "1470638701252";
            OrderOperationLog.Operator = "1470638701252";
            OrderOperationLog.OperatorId = 1;
            OrderOperationLog.CreateTime = DateTime.Now;

            using (var _connection = Utilities.GetOpenConnection())
            {
                _connection.Insert(OrderOperationLog);
            }






        }
    }


    public class Utilities
    {
        private static readonly ConnectionStringSettings Connection = ConfigurationManager.ConnectionStrings["Xiaoyujia"];
        private static readonly string ConnectionString = Connection.ConnectionString;

        public static SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }


    }

    [Table("OrderOperationLog")]
    public partial class OrderOperationLog 
    {
        public int Id
        {
            set;
            get;
        }

        /// <summary>
        /// 操作时间
        /// </summary>

        public DateTime? CreateTime
        {
            set;
            get;
        }


        /// <summary>
        /// 操作者
        /// </summary>
     
        public virtual string Operator { get; set; }

        /// <summary>
        /// 操作者id
        /// </summary>
      
        public virtual int OperatorId { get; set; }

        /// <summary>
        /// ip 地址
        /// </summary>
      
        public virtual string IP { get; set; }


        /// <summary>
        /// 操作标题
        /// </summary>
     
        public string Title
        {
            set;
            get;
        }
        /// <summary>
        /// 操作内容
        /// </summary>
     
        public string Message
        {
            set;
            get;
        }
        /// <summary>
        /// 总订单号
        /// </summary>
       
        public string BillNo
        {
            set;
            get;
        }


    }

}
