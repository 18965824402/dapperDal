using HSCP.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Dal
{
    public class Utilities
    {
        public static SqlConnection GetOpenConnection(WRStrategy Strategy = WRStrategy.Write)
        {
            ConnectionStringSettings Connection = ConfigurationManager.ConnectionStrings["testdb"];
            bool flag = ZConfig.GetConfigBool("SingleStrategy");
            if (flag)
            {
                if (Strategy == WRStrategy.Read)
                {
                    Connection = ConfigurationManager.ConnectionStrings["readtestdb"];
                }
            }
            string ConnectionString = Connection.ConnectionString;
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}