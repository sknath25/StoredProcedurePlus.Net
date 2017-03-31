using StoredProcedurePlus.Net.ConnectionManagers;
using System;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public sealed class ProcedureConfiguration<S> where S : class
    {
        public string ConnectionStringName { get; set; }

        public string ConnectionString { get; set; }

        public string ProcedureName { get; set; }
      
        internal readonly ConnectionFactory Connection;

        public EntityConfiguration<S> Input;

        public ProcedureConfiguration()
        {
            Input = new EntityConfiguration<S>();
            Connection = new ConnectionFactory();
        }

        internal void Initialize()
        {
            Input.Initialize();
        }
    }
}