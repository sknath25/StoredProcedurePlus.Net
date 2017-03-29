using System.Data;

namespace StoredProcedurePlus.Net.ConnectionManagers
{
    public interface IConnectionManager
    {
        void SetConnectionStringName(string name);

        void SetConnectionString(string connectionString);

        IDbConnection GetConnection();

        void TrashConnection(IDbConnection connection);
    }
}
