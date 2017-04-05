using StoredProcedurePlus.Net.ConnectionManagers;
using StoredProcedurePlus.Net.EntityManagers;
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
    public abstract class StoredProcedureManager<S> where S : class
    {
        static object Locker = new object();

        readonly static ProcedureConfiguration<S> Configuration = new ProcedureConfiguration<S>();

        static ObjectActivator CreateCtor(Type type)
        {
            if (type == null)
            {
                throw new NullReferenceException("type");
            }
            ConstructorInfo emptyConstructor = type.GetConstructor(Type.EmptyTypes);
            var dynamicMethod = new DynamicMethod("CreateInstance", type, Type.EmptyTypes, true);
            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
            ilGenerator.Emit(OpCodes.Nop);
            ilGenerator.Emit(OpCodes.Newobj, emptyConstructor);
            ilGenerator.Emit(OpCodes.Ret);
            return (ObjectActivator)dynamicMethod.CreateDelegate(typeof(ObjectActivator));
        }

        delegate object ObjectActivator();

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

        List<List<DbDataRecord>> ResultSet = new List<List<DbDataRecord>>();

        [SuppressMessage("Microsoft.Security", "CA2100", Justification = "The command text is not user given")]
        public int Execute(S input, ConnectionScope scope)
        {
            this.Initialize();

            IDbCommand Command = new SqlCommand(Configuration.ProcedureName);

            Command.CommandType = CommandType.StoredProcedure;

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

                        CommandBehavior Behavior = CommandBehavior.SequentialAccess;

                        Configuration.Input.Prepare(adapter);

                        Configuration.Input.Fill(input, adapter);

                        if (Configuration.OutputSets.Any())
                        {
                            int ResultSetIndex = 0;

                            using (IDataReader DataReader = Command.ExecuteReader(Behavior))
                            {
                                do
                                {
                                    ResultSet.Add(new List<DbDataRecord>());

                                    while (DataReader.Read())
                                    {
                                        
                                    }

                                    ResultSetIndex++;

                                } while (DataReader.NextResult());
                            }
                        }
                        else
                        {
                            Result = Command.ExecuteNonQuery();
                        }

                        Configuration.Input.Fill(adapter, input);
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

                        CommandBehavior Behavior = CommandBehavior.SequentialAccess;

                        Configuration.Input.Prepare(adapter);

                        Configuration.Input.Fill(input, adapter);

                        if (Configuration.OutputSets.Any())
                        {
                            int ResultSetIndex = 0;

                            using (IDataReader DataReader = Command.ExecuteReader(Behavior))
                            {
                                do
                                {
                                    ResultSet.Add(new List<DbDataRecord>());

                                    while (DataReader.Read())
                                    {

                                    }

                                    ResultSetIndex++;

                                } while (DataReader.NextResult());
                            }
                        }
                        else
                        {
                            Result = Command.ExecuteNonQuery();
                        }

                        Configuration.Input.Fill(adapter, input);
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

        public ICollection<T> GetResult<T>(int index) where T : class
        {
            List<T> Result = new List<T>();

            var x = Configuration.OutputSets[index] as EntityConfiguration<T>;

            if (x == null)
            {
                Error.WrongReturnTypeExpected(typeof(T), Configuration.OutputSets[index].SourceType);
            }

            for (int s = 0; s < ResultSet[index].Count; s++)
            {
                T instance = (T)CreateCtor(typeof(T)).Invoke();
                //x.Fill(ResultSet[index][s], instance);
                Result.Add(instance);
            }

            return Result;
        }

        public ICollection<T> GetResult<T>() where T : class
        {
            return this.GetResult<T>(0);
        }
    }
}
