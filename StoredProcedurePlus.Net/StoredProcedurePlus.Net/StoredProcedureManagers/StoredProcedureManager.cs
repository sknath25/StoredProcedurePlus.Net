using StoredProcedurePlus.Net.ConnectionManagers;
using StoredProcedurePlus.Net.EntityManagers;
using StoredProcedurePlus.Net.EntityManagers.Factories;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public abstract class StoredProcedureManager<S> where S : class, new()
    {
        static object Locker = new object();

        readonly static ProcedureConfiguration<S> Configuration = new ProcedureConfiguration<S>();

        private void Initialize()
        {
            lock (Locker)
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
        }

        protected StoredProcedureManager()
        {

        }

        protected abstract void Setup(ProcedureConfiguration<S> configuration);

        List<List<object>> ResultSet = new List<List<object>>();

        [SuppressMessage("Microsoft.Security", "CA2100", Justification = "The command text is not user given")]
        public int Execute(S input, ConnectionScope scope)
        {
            this.Initialize();

            IDbCommand Command = new SqlCommand(Configuration.ProcedureName);

            Command.CommandType = CommandType.StoredProcedure;

            CommandBehavior Behavior = CommandBehavior.Default;

            int Result = -1;

            if (scope == null)
            {
                using (scope = new ConnectionScope())
                {
                    scope.SetConnectionProvider(Configuration.Connection);

                    Command.Connection = scope.Create();

                    if (Command.Connection != null)
                    {
                        SqlParameterEntityAdapter adapter = (SqlParameterEntityAdapter)Configuration.Input.GetAsSqlParameters();

                        for (int i = 0; i < adapter.FieldCount; i++)
                        {
                            Command.Parameters.Add(adapter[i]);
                        }

                        Configuration.Input.Prepare(adapter);

                        Configuration.Input.Get(input, adapter);

                        if (Configuration.OutputSets.Any())
                        {
                            int ResultSetIndex = 0;

                            using (IDataReader DataReader = Command.ExecuteReader(Behavior))
                            {
                                do
                                {
                                    EntityInstanceFactory InstanceFactory = Configuration.OutputSets[ResultSetIndex].GetDefaultInstanceFactory();

                                    ResultSet.Add(new List<object>());

                                    if (DataReader.Read())
                                    {
                                        NonPrimitiveEntityConfiguration c = Configuration.OutputSets[ResultSetIndex];

                                        DbDataEntityAdapter EntityAdapter = c.GetNewDataAdapter(DataReader);

                                        c.Prepare(EntityAdapter);

                                        object Entity = InstanceFactory.CreateNewDefaultInstance();

                                        c.Set(EntityAdapter, Entity);

                                        ResultSet[ResultSetIndex].Add(Entity);

                                        while (DataReader.Read())
                                        {
                                            Entity = InstanceFactory.CreateNewDefaultInstance();

                                            c.Set(EntityAdapter, Entity);

                                            ResultSet[ResultSetIndex].Add(Entity);
                                        }
                                    }

                                    ResultSetIndex++;

                                } while (DataReader.NextResult());
                            }
                        }
                        else
                        {
                            Result = Command.ExecuteNonQuery();
                        }

                        Configuration.Input.Set(adapter, input);
                    }
                }
            }
            else
            {
                try
                {
                    scope.SetConnectionProvider(Configuration.Connection);

                    Command.Connection = scope.Create();

                    if (Command.Connection != null)
                    {
                        SqlParameterEntityAdapter adapter = (SqlParameterEntityAdapter)Configuration.Input.GetAsSqlParameters();

                        for (int i = 0; i < adapter.FieldCount; i++)
                        {
                            Command.Parameters.Add(adapter[i]);
                        }                        

                        Configuration.Input.Prepare(adapter);

                        Configuration.Input.Get(input, adapter);

                        if (Configuration.OutputSets.Any())
                        {
                            int ResultSetIndex = 0;

                            using (IDataReader DataReader = Command.ExecuteReader(Behavior))
                            {
                                do
                                {
                                    EntityInstanceFactory InstanceFactory = Configuration.OutputSets[ResultSetIndex].GetDefaultInstanceFactory();

                                    ResultSet.Add(new List<object>());

                                    if (DataReader.Read())
                                    {
                                        NonPrimitiveEntityConfiguration c = Configuration.OutputSets[ResultSetIndex];

                                        DbDataEntityAdapter EntityAdapter = c.GetNewDataAdapter(DataReader);

                                        c.Prepare(EntityAdapter);

                                        object Entity = InstanceFactory.CreateNewDefaultInstance();

                                        c.Set(EntityAdapter, Entity);

                                        ResultSet[ResultSetIndex].Add(Entity);

                                        while (DataReader.Read())
                                        {
                                            Entity = InstanceFactory.CreateNewDefaultInstance();

                                            c.Set(EntityAdapter, Entity);

                                            ResultSet[ResultSetIndex].Add(Entity);
                                        }
                                    }

                                    ResultSetIndex++;

                                } while (DataReader.NextResult());
                            }
                        }
                        else
                        {
                            Result = Command.ExecuteNonQuery();
                        }

                        Configuration.Input.Set(adapter, input);
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    scope.End();
                }
            }
            return Result;
        }

        public int Execute(S input)
        {
            return this.Execute(input, null);
        }

        public IEnumerable<T> GetResult<T>(int index) where T : class
        {
            return ResultSet[index].Cast<T>();
        }

        public IEnumerable<T> GetResult<T>() where T : class
        {
            return this.GetResult<T>(0);
        }
    }
}
