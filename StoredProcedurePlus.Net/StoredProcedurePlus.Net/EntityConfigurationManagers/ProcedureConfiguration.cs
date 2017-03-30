using StoredProcedurePlus.Net.ConnectionManagers;
using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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