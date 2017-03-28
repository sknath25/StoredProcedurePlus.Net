using StoredProcedurePlus.Net.StoredProcedureManagers.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public abstract class StoredProcedureManager<S>
    {
        protected static string ProcedureName { get; set; }

        protected static EntityConfiguration<S> InputConfiguration { get; set; }

        static StoredProcedureManager()
        {
            InputConfiguration = new EntityConfiguration<S>();
        }

        protected StoredProcedureManager()
        {
            if(ProcedureName==null)
            {
                Setup();
            }
        }

        protected abstract void Setup(); 

        public void Execute(S input, IDbConnection connection)
        {
            IDbCommand Command = new SqlCommand();

            SqlParameter[] Parameters = InputConfiguration.GetAllParameters(input);
        }
    }
}
