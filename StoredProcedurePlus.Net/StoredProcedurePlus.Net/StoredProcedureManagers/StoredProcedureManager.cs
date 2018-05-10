using StoredProcedurePlus.Net.EntityManagers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public class MockEventArgs:EventArgs
    {
        public MockEventArgs(IDataEntityAdapter input):base()
        {
            Input = input;
        }

        public IDataEntityAdapter Input { get; private set; }
        public int Result { get; set; }
    }

    public delegate void EventHandler<MockEventArgs>(object sender, MockEventArgs e);

    /// <summary>
    /// The New Stored procedure class decorated with two generic types. 1st one for the Stored procedure class and 2nd one for the parameter type.
    /// </summary>
    /// <typeparam name="TSelf">The derive class type</typeparam>
    /// <typeparam name="TParameterContainerType">The paraneter class type</typeparam>
    public abstract class StoredProcedureManager<TSelf, TParameterContainerType> where TParameterContainerType : class, new() where TSelf : StoredProcedureManager<TSelf, TParameterContainerType>
    {
        static object Locker = new object();

        readonly static ProcedureConfiguration<TParameterContainerType> Configuration = new ProcedureConfiguration<TParameterContainerType>();

        List<List<object>> ResultSet = null;

        void Initialize()
        {
            ResultSet = new List<List<object>>();

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

        #region Restricted 
        protected StoredProcedureManager()
        {

        }

        protected abstract void Setup(ProcedureConfiguration<TParameterContainerType> configuration);

        #endregion

        #region Public Events 
        public event EventHandler<MockEventArgs> OnMockExecutionEventHandler = null;
        #endregion

        #region Public Methods

        [SuppressMessage("Microsoft.Security", "CA2100", Justification = "The command text is not user given")]
        public int Execute(TParameterContainerType input, ConnectionScope scope)
        {
            this.Initialize();

            int Result = -1;

            using (IDbCommand Command = new SqlCommand())
            {
                Command.CommandType = CommandType.StoredProcedure;

                if (Configuration.ProcedureName != null)
                {
                    Command.CommandText = Configuration.ProcedureName;
                }
                else
                {
                    Command.CommandText = this.GetType().Name;
                }

                CommandBehavior Behavior = CommandBehavior.Default;

                if (scope == null)
                {
                    using (scope = new ConnectionScope())
                    {
                        scope.SetConnectionProvider(Configuration.Connection);

                        DbParameterEntityAdapter adapter = (DbParameterEntityAdapter)Configuration.Input.GetAsDbParameters();

                        for (int i = 0; i < adapter.FieldCount; i++)
                        {
                            Command.Parameters.Add(adapter[i]);
                        }

                        Configuration.Input.Prepare(adapter);

                        if (adapter.FieldCount > 0)
                        {
                            Configuration.Input.Get(input, adapter);
                        }

                        for(int p_counter = 0; p_counter < Command.Parameters.Count; p_counter++)
                        {
                            SqlParameter p_parameter = (SqlParameter)Command.Parameters[p_counter];
                            if (p_parameter.SqlDbType == SqlDbType.Structured && 
                                (p_parameter.Value == DBNull.Value || (p_parameter.Value as DataTable).Rows.Count <= 0))
                            {
                                Command.Parameters.RemoveAt(p_counter);
                            }
                        }

                        if (Configuration.OutputSets.Any())
                        {
                            int ResultSetIndex = 0;

                            if (Configuration.Mock)
                            {
                                if (OnMockExecutionEventHandler != null)
                                {
                                    MockEventArgs Args = new MockEventArgs(adapter);
                                    OnMockExecutionEventHandler?.Invoke(this, Args);
                                    Result = Args.Result;
                                }
                            }
                            else
                            {
                                Command.Connection = scope.Create();

                                if (Command.Connection != null)
                                {
                                    using (IDataReader DataReader = Command.ExecuteReader(Behavior))
                                    {
                                        do
                                        {
                                            ResultSet.Add(new List<object>());

                                            if (DataReader.Read())
                                            {
                                                NonPrimitiveEntityConfiguration c = Configuration.OutputSets[ResultSetIndex];

                                                DbDataEntityAdapter EntityAdapter = c.GetNewDataAdapter(DataReader);

                                                c.Prepare(EntityAdapter);

                                                object Entity = ((IHasDefaultConstructor)Configuration.OutputSets[ResultSetIndex]).CreateNewDefaultInstance();

                                                c.Set(EntityAdapter, Entity);

                                                ResultSet[ResultSetIndex].Add(Entity);

                                                while (DataReader.Read())
                                                {
                                                    Entity = ((IHasDefaultConstructor)Configuration.OutputSets[ResultSetIndex]).CreateNewDefaultInstance();

                                                    c.Set(EntityAdapter, Entity);

                                                    ResultSet[ResultSetIndex].Add(Entity);
                                                }
                                            }

                                            ResultSetIndex++;

                                        } while (DataReader.NextResult());
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Configuration.Mock)
                            {
                                if (OnMockExecutionEventHandler != null)
                                {
                                    MockEventArgs Args = new MockEventArgs(adapter);
                                    OnMockExecutionEventHandler?.Invoke(this, Args);
                                    Result = Args.Result;
                                }
                            }
                            else
                            {
                                Command.Connection = scope.Create();

                                if (Command.Connection != null)
                                {
                                    Result = Command.ExecuteNonQuery();
                                }
                            }
                        }

                        Configuration.Input.Set(adapter, input);
                    }
                }
                else
                {
                    try
                    {
                        scope.SetConnectionProvider(Configuration.Connection);

                        DbParameterEntityAdapter adapter = (DbParameterEntityAdapter)Configuration.Input.GetAsDbParameters();

                        for (int i = 0; i < adapter.FieldCount; i++)
                        {
                            Command.Parameters.Add(adapter[i]);
                        }

                        Configuration.Input.Prepare(adapter);

                        if (adapter.FieldCount > 0)
                        {
                            Configuration.Input.Get(input, adapter);
                        }

                        for (int p_counter = 0; p_counter < Command.Parameters.Count; p_counter++)
                        {
                            SqlParameter p_parameter = (SqlParameter)Command.Parameters[p_counter];
                            if (p_parameter.SqlDbType == SqlDbType.Structured && p_parameter.Value == DBNull.Value)
                            {
                                Command.Parameters.RemoveAt(p_counter);
                            }
                        }

                        if (Configuration.OutputSets.Any())
                        {
                            int ResultSetIndex = 0;

                            if (Configuration.Mock)
                            {
                                if (OnMockExecutionEventHandler != null)
                                {
                                    MockEventArgs Args = new MockEventArgs(adapter);
                                    OnMockExecutionEventHandler?.Invoke(this, Args);
                                    Result = Args.Result;
                                }
                            }
                            else
                            {
                                Command.Connection = scope.Create();

                                if (Command.Connection != null)
                                {
                                    using (IDataReader DataReader = Command.ExecuteReader(Behavior))
                                    {
                                        do
                                        {
                                            ResultSet.Add(new List<object>());

                                            if (DataReader.Read())
                                            {
                                                NonPrimitiveEntityConfiguration c = Configuration.OutputSets[ResultSetIndex];

                                                DbDataEntityAdapter EntityAdapter = c.GetNewDataAdapter(DataReader);

                                                c.Prepare(EntityAdapter);

                                                object Entity = ((IHasDefaultConstructor)Configuration.OutputSets[ResultSetIndex]).CreateNewDefaultInstance();

                                                c.Set(EntityAdapter, Entity);

                                                ResultSet[ResultSetIndex].Add(Entity);

                                                while (DataReader.Read())
                                                {
                                                    Entity = ((IHasDefaultConstructor)Configuration.OutputSets[ResultSetIndex]).CreateNewDefaultInstance();

                                                    c.Set(EntityAdapter, Entity);

                                                    ResultSet[ResultSetIndex].Add(Entity);
                                                }
                                            }

                                            ResultSetIndex++;

                                        } while (DataReader.NextResult());
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Configuration.Mock)
                            {
                                if (OnMockExecutionEventHandler != null)
                                {
                                    MockEventArgs Args = new MockEventArgs(adapter);
                                    OnMockExecutionEventHandler?.Invoke(this, Args);
                                    Result = Args.Result;
                                }
                            }
                            else
                            {
                                Command.Connection = scope.Create();

                                if (Command.Connection != null)
                                {
                                    Result = Command.ExecuteNonQuery();
                                }
                            }
                        }

                        Configuration.Input.Set(adapter, input);
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
            }
            return Result;
        }

        public int Execute(TParameterContainerType input)
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

        #endregion
    }

    /// <summary>
    /// This is deprecated.
    /// REason: Unique types required as parameter for each Stored procedure class. 
    /// </summary>
    /// <typeparam name="S">The paraneter class type</typeparam>
    [Obsolete("Cannot share common parameter type or class with multiple stored procedure classes. Use StoredProcedureManager<D,S> type as base instead where D and S are Derived stored procedure and parameter class types.", false)]
    public abstract class StoredProcedureManager<S> where S : class, new() 
    {      
        static object Locker = new object();

        readonly static ProcedureConfiguration<S> Configuration = new ProcedureConfiguration<S>();

        List<List<object>> ResultSet = null;

        void Initialize()
        {
            ResultSet = new List<List<object>>();

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

        #region Restricted 
        protected StoredProcedureManager()
        {

        }

        protected abstract void Setup(ProcedureConfiguration<S> configuration);

        #endregion

        #region Public Events 
        public event EventHandler<MockEventArgs> OnMockExecution = null;
        #endregion

        #region Public Methods

        [SuppressMessage("Microsoft.Security", "CA2100", Justification = "The command text is not user given")]
        public int Execute(S input, ConnectionScope scope)
        {
            this.Initialize();

            int Result = -1;

            using (IDbCommand Command = new SqlCommand())
            {
                Command.CommandType = CommandType.StoredProcedure;

                if (Configuration.ProcedureName != null)
                {
                    Command.CommandText = Configuration.ProcedureName;
                }
                else
                {
                    Command.CommandText = this.GetType().Name;
                }

                //CommandBehavior Behavior = CommandBehavior.Default;

                if (scope == null)
                {
                    using (scope = new ConnectionScope())
                    {
                        scope.SetConnectionProvider(Configuration.Connection);

                        DbParameterEntityAdapter adapter = (DbParameterEntityAdapter)Configuration.Input.GetAsDbParameters();

                        for (int i = 0; i < adapter.FieldCount; i++)
                        {
                            Command.Parameters.Add(adapter[i]);
                        }

                        Configuration.Input.Prepare(adapter);

                        if (adapter.FieldCount > 0)
                        {
                            Configuration.Input.Get(input, adapter);
                        }

                        for (int p_counter = 0; p_counter < Command.Parameters.Count; p_counter++)
                        {
                            SqlParameter p_parameter = (SqlParameter)Command.Parameters[p_counter];
                            if (p_parameter.SqlDbType == SqlDbType.Structured && p_parameter.Value == DBNull.Value)
                            {
                                Command.Parameters.RemoveAt(p_counter);
                            }
                        }

                        if (Configuration.OutputSets.Any())
                        {
                            int ResultSetIndex = 0;

                            if (Configuration.Mock)
                            {
                                if (OnMockExecution != null)
                                {
                                    MockEventArgs Args = new MockEventArgs(adapter);
                                    OnMockExecution?.Invoke(this, Args);
                                    Result = Args.Result;
                                }
                            }
                            else
                            {
                                Command.Connection = scope.Create();

                                if (Command.Connection != null)
                                {
                                    using (IDataReader DataReader = Command.ExecuteReader())
                                    {
                                        do
                                        {
                                            ResultSet.Add(new List<object>());

                                            if (DataReader.Read())
                                            {
                                                NonPrimitiveEntityConfiguration c = Configuration.OutputSets[ResultSetIndex];

                                                DbDataEntityAdapter EntityAdapter = c.GetNewDataAdapter(DataReader);

                                                c.Prepare(EntityAdapter);

                                                object Entity = ((IHasDefaultConstructor)Configuration.OutputSets[ResultSetIndex]).CreateNewDefaultInstance();

                                                c.Set(EntityAdapter, Entity);

                                                ResultSet[ResultSetIndex].Add(Entity);

                                                while (DataReader.Read())
                                                {
                                                    Entity = ((IHasDefaultConstructor)Configuration.OutputSets[ResultSetIndex]).CreateNewDefaultInstance();

                                                    c.Set(EntityAdapter, Entity);

                                                    ResultSet[ResultSetIndex].Add(Entity);
                                                }
                                            }

                                            ResultSetIndex++;

                                        } while (DataReader.NextResult());
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Configuration.Mock)
                            {
                                if (OnMockExecution != null)
                                {
                                    MockEventArgs Args = new MockEventArgs(adapter);
                                    OnMockExecution?.Invoke(this, Args);
                                    Result = Args.Result;
                                }
                            }
                            else
                            {
                                Command.Connection = scope.Create();

                                if (Command.Connection != null)
                                {
                                    Result = Command.ExecuteNonQuery();
                                }
                            }
                        }

                        Configuration.Input.Set(adapter, input);
                    }
                }
                else
                {
                    try
                    {
                        scope.SetConnectionProvider(Configuration.Connection);

                        DbParameterEntityAdapter adapter = (DbParameterEntityAdapter)Configuration.Input.GetAsDbParameters();

                        for (int i = 0; i < adapter.FieldCount; i++)
                        {
                            Command.Parameters.Add(adapter[i]);
                        }

                        Configuration.Input.Prepare(adapter);

                        if (adapter.FieldCount > 0)
                        {
                            Configuration.Input.Get(input, adapter);
                        }

                        for (int p_counter = 0; p_counter < Command.Parameters.Count; p_counter++)
                        {
                            SqlParameter p_parameter = (SqlParameter)Command.Parameters[p_counter];
                            if (p_parameter.SqlDbType == SqlDbType.Structured && p_parameter.Value == DBNull.Value)
                            {
                                Command.Parameters.RemoveAt(p_counter);
                            }
                        }

                        if (Configuration.OutputSets.Any())
                        {
                            int ResultSetIndex = 0;

                            if (Configuration.Mock)
                            {
                                if (OnMockExecution != null)
                                {
                                    MockEventArgs Args = new MockEventArgs(adapter);
                                    OnMockExecution?.Invoke(this, Args);
                                    Result = Args.Result;
                                }
                            }
                            else
                            {
                                Command.Connection = scope.Create();

                                if (Command.Connection != null)
                                {
                                    using (IDataReader DataReader = Command.ExecuteReader())
                                    {
                                        do
                                        {
                                            ResultSet.Add(new List<object>());

                                            if (DataReader.Read())
                                            {
                                                NonPrimitiveEntityConfiguration c = Configuration.OutputSets[ResultSetIndex];

                                                DbDataEntityAdapter EntityAdapter = c.GetNewDataAdapter(DataReader);

                                                c.Prepare(EntityAdapter);

                                                object Entity = ((IHasDefaultConstructor)Configuration.OutputSets[ResultSetIndex]).CreateNewDefaultInstance();

                                                c.Set(EntityAdapter, Entity);

                                                ResultSet[ResultSetIndex].Add(Entity);

                                                while (DataReader.Read())
                                                {
                                                    Entity = ((IHasDefaultConstructor)Configuration.OutputSets[ResultSetIndex]).CreateNewDefaultInstance();

                                                    c.Set(EntityAdapter, Entity);

                                                    ResultSet[ResultSetIndex].Add(Entity);
                                                }
                                            }

                                            ResultSetIndex++;

                                        } while (DataReader.NextResult());
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Configuration.Mock)
                            {
                                if (OnMockExecution != null)
                                {
                                    MockEventArgs Args = new MockEventArgs(adapter);
                                    OnMockExecution?.Invoke(this, Args);
                                    Result = Args.Result;
                                }
                            }
                            else
                            {
                                Command.Connection = scope.Create();

                                if (Command.Connection != null)
                                {
                                    Result = Command.ExecuteNonQuery();
                                }
                            }
                        }

                        Configuration.Input.Set(adapter, input);
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

        #endregion
    }
}
