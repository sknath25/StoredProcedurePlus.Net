using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StoredProcedurePlus.Net.ConnectionManagers
{
    public class SqlConnectionManager : IConnectionManager
    {
        private IDbConnection Connection = null;

        private string ConnectionStringName = null;

        private string ConnectionString = null;

        public SqlConnectionManager()
        {
            ConnectionStringName = "DbString";
        }

        public SqlConnectionManager(string connectionStringName)
        {
            ConnectionStringName = connectionStringName;
        }

        public IDbConnection GetConnection()
        {
            if (Connection == null)
            {
                if (ConnectionString == null && ConnectionStringName != null)
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
                }

                if (ConnectionString == null) return null;

                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                return Connection;
            }
            else
            {
                return Connection;
            }
        }

        public void SetConnectionString(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public void SetConnectionStringName(string name)
        {
            this.ConnectionStringName = name;
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
