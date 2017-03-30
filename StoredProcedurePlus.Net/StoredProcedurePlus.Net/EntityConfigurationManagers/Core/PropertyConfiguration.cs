using System;
using System.Data;
using System.Reflection;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.Core
{
    public abstract class PropertyConfiguration
    {
        public PropertyConfiguration()
        {

        }

        internal PropertyConfiguration(PropertyInfo propertyInfo)
        {
            this.PropertyName = propertyInfo.Name;
            this.ParameterName = this.PropertyName;
            this.DataType = propertyInfo.PropertyType;            
        }

        protected internal string PropertyName { get; protected set; }

        protected internal bool IsOut { get; protected set; }

        protected internal string ParameterName { get; protected set; }     

        protected internal Type DataType { get; protected set; }

        internal abstract SqlDbType GetSqlDbType();
    }
}