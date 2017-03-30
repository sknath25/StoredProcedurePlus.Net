using System;
using System.Data;

namespace StoredProcedurePlus.Net.ConnectionManagers
{
    public interface IConnectionManager:IDisposable
    {
        void SetConnectionStringName(string name);

        void SetConnectionString(string connectionString);

        IDbConnection GetNewConnection();
    }
}
