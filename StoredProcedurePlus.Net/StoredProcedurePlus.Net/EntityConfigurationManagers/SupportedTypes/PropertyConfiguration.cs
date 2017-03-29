using System.Data;
using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using System;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public class PropertyConfiguration
    {
        protected internal string PropertyName { get; protected set; }

        protected internal bool IsOut { get; protected set; }

        protected internal string ParameterName { get; protected set; }     

        protected internal Type DataType { get; protected set; }

        internal virtual SqlDbType GetSqlDbType()
        {
            if (DataType == typeof(int))
            {
                return SqlDbType.Int;
            }
            else
            {
                return SqlDbType.VarChar; // Default
            }
        }
    }
}