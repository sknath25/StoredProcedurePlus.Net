using StoredProcedurePlus.Net.ErrorManagers;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StoredProcedurePlus.Net.ConnectionManagers
{
    public sealed class ConnectionFactory
    {
        static object Locker = new object();

        private string ConnectionStringName = null;

        private string ConnectionString = null;

        internal ConnectionFactory()
        {            
        }

        public void SetConnectionString(string connectionString)
        {
                this.ConnectionString = connectionString;
        }

        public void SetConnectionStringName(string name)
        {
            this.ConnectionStringName = name;
        }

        internal IDbConnection GetNewConnection()
        {
            lock (Locker)
            {
                if (ConnectionString == null && ConnectionStringName != null)
                {

                    if(string.IsNullOrEmpty(ConnectionStringName))
                    {
                        Error.NoConnectionNameError();
                    }

                    if (ConfigurationManager.ConnectionStrings.Count <= 0)
                    {
                        Error.ConnectionNameMissingError(ConnectionStringName);
                    }

                    try
                    {
                        ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
                    }
                    catch(ConfigurationErrorsException ex)
                    {
                        Error.ConnectionNameMissingError(ConnectionStringName, ex);
                    }
                }

                if (ConnectionString == null)
                {
                    Error.NoConnectionStringError();
                }

                IDbConnection Connection = new SqlConnection(ConnectionString);

                return Connection;
            }
        }
    }
}
