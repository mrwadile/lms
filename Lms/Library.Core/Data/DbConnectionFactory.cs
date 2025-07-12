using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Data
{
    public class DbConnectionFactory
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["LMSLiteDb"].ConnectionString;

        public static SqlConnection CreateConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
}
