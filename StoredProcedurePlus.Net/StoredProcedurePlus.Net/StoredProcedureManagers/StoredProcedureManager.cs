using StoredProcedurePlus.Net.ConnectionManagers;
using StoredProcedurePlus.Net.StoredProcedureManagers;
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
        readonly static ProcedureConfiguration<S> Configuration = new ProcedureConfiguration<S>();

        protected StoredProcedureManager()
        {
            if(Configuration.ProcedureName == null)
            {
                Setup(Configuration);

                if(Configuration.ConnectionString==null)
                {
                    Configuration.Connection.SetConnectionStringName(Configuration.ConnectionStringName);
                }
                else
                {
                    Configuration.Connection.SetConnectionString(Configuration.ConnectionString);
                }
            }
        }

        protected abstract void Setup(ProcedureConfiguration<S> configuration); 

        public int Execute(S input)
        {
            IDbCommand Command = new SqlCommand();

            SqlParameter[] Parameters = Configuration.Input.GetAllParameters(input);
            for(int i=0;i<Parameters.Length;i++)
            {
                Command.Parameters.Add(Parameters[i]);
            }

            // executes 
            IDbConnection Connection = Configuration.Connection.GetConnection();
            Command.Connection = Connection;
            int Result = -1;
            try
            {
                if (Connection != null)
                {
                    Result = Command.ExecuteNonQuery();
                }

                Configuration.Input.SetOuts(Parameters, input);
            }
            catch
            {
                throw;
            }
            finally
            {
                Configuration.Connection.TrashConnection(Connection);
            }

            return Result;
        }
    }
}
