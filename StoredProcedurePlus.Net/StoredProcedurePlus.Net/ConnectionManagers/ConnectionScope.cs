using StoredProcedurePlus.Net.ConnectionManagers;
using System;
using System.Data;

namespace StoredProcedurePlus.Net
{
    public sealed class ConnectionScope : IDisposable
    {
        bool IsDisposed = false;

        //Broken, Fetching, Executing are for Future version od ADO.NET and not in use at present.

        ConnectionState NotUsable =
            ConnectionState.Broken      |
            ConnectionState.Connecting  |
            ConnectionState.Fetching    |
            ConnectionState.Executing;

        ConnectionState ReadyToOpen =
            ConnectionState.Closed;

        ConnectionState ReadyToClose =
            ConnectionState.Open        | 
            ConnectionState.Broken;

        ConnectionFactory ConnectionFactory;
        readonly ConnectionScopeType ScopeType;

        public ConnectionScope()
        {
            ScopeType = ConnectionScopeType.CloseAfterEachExecution;
        }

        public ConnectionScope(ConnectionScopeType type)
        {
            ScopeType = type;
        }

        private IDbConnection Connection = null;
        public void Dispose()
        {
            if (Connection != null && !IsDisposed)
            {
                Connection.Dispose();
            }
        }

        internal void SetConnectionProvider(ConnectionFactory factory)
        {
            ConnectionFactory = factory;
        }

        internal void End()
        {
            if (Connection == null) return;

            if (Connection.State == ReadyToClose && ScopeType == ConnectionScopeType.CloseAfterEachExecution)
            {
                Connection.Close();
            }
            else if(ScopeType == ConnectionScopeType.DisposeAfterEachExecution)
            {
                Connection.Dispose();
                Connection = null;
                IsDisposed = true;
            }
        }

        internal IDbConnection Create()
        {
            if (Connection == null)
            {
                Connection = ConnectionFactory.GetNewConnection();

                if (Connection != null)
                {
                    IsDisposed = false;
                    Connection.Open();
                }
            }
            else
            {
                if (Connection.State == NotUsable)
                {
                    Connection.Dispose();

                    Connection = ConnectionFactory.GetNewConnection();
                }

                if (Connection.State == ReadyToOpen)
                {
                    Connection.Open();
                }
            }

            return Connection;
        }
    }

    public enum ConnectionScopeType
    {
        DisposeAfterEachExecution,
        CloseAfterEachExecution,
        KeepOpen,
    }
}
