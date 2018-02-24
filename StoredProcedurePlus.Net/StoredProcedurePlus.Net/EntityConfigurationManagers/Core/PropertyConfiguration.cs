using System;
using System.Data;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.Core
{
    public abstract class PropertyConfiguration: IEquatable<PropertyConfiguration>
    {
        public PropertyConfiguration()
        {

        }

        protected internal string PropertyName { get; protected set; }

        protected internal bool IsOut { get; protected set; }

        protected internal string ParameterName { get; protected set; }     

        protected internal Type DataType { get; protected set; }        

        public bool Equals(PropertyConfiguration other)
        {
            if (other.PropertyName == this.PropertyName) return true; else return false;
        }

        internal abstract DbType GetDbType { get; }

        protected internal uint Size1 { get; protected set; }

        protected internal uint Size2 { get; protected set; }
    }
}