using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.ConnectionManagers
{
    public class SqlConnectionManager : IConnectionManager
    {
        private IDbConnection Connection = null;
        private readonly string ConnectionStringName = null;

        public SqlConnectionManager()
        {
            ConnectionStringName = "DbString";
            //Test commit..
        }

        public SqlConnectionManager(string connectionStringName)
        {
            ConnectionStringName = connectionStringName;
        }

        public IDbConnection GetConnection()
        {
            if (Connection == null)
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                return Connection;
            }
            else
            {
                return Connection;
            }
        }

        public void TrashConnection(IDbConnection connection)
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}
