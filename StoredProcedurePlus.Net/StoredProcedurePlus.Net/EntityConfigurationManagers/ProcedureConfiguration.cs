using StoredProcedurePlus.Net.ConnectionManagers;
using System;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public sealed class ProcedureConfiguration<S>: IDisposable where S : class
    {
        public string ConnectionStringName { get; set; }

        public string ConnectionString { get; set; }

        public string ProcedureName { get; set; }
      
        internal readonly IConnectionManager Connection;

        public EntityConfiguration<S> Input;

        public ProcedureConfiguration()
        {
            Input = new EntityConfiguration<S>();
            Connection = new SqlConnectionManager();
        }

        internal void Initialize()
        {
            Input.Initialize();
        }

        public void Dispose()
        {
            if(Connection!=null)
            {
                Connection.Dispose();
            }
        }
    }
}