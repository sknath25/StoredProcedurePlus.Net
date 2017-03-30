using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StoredProcedurePlus.Net.ConnectionManagers
{
    public sealed class SqlConnectionManager : IConnectionManager
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

        public void SetConnectionString(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IDbConnection GetNewConnection()
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

        public void SetConnectionStringName(string name)
        {
            this.ConnectionStringName = name;
        }

        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Dispose();
            }
        }
    }
}
