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
    public abstract class StoredProcedureManager<S> where S : class
    {
        readonly static ProcedureConfiguration<S> Configuration = new ProcedureConfiguration<S>();

        protected StoredProcedureManager()
        {

        }

        private void Initialize()
        {
            if (Configuration.ProcedureName == null)
            {
                Setup(Configuration);

                Configuration.Initialize();

                if (Configuration.ConnectionString == null)
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
            this.Initialize();

            IDbCommand Command = new SqlCommand(Configuration.ProcedureName);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter[] Parameters = Configuration.Input.GetAllParameters(input);

            for(int i=0;i<Parameters.Length;i++)
            {
                Command.Parameters.Add(Parameters[i]);
            }

            int Result = -1;

            using (Command.Connection = Configuration.Connection.GetNewConnection())
            {
                try
                {
                    if (Command.Connection != null)
                    {
                        Result = Command.ExecuteNonQuery();
                        Configuration.Input.SetOuts(Parameters, input);
                    }
                }
                catch
                {
                    throw;
                }
            }

            return Result;
        }
    }
}
