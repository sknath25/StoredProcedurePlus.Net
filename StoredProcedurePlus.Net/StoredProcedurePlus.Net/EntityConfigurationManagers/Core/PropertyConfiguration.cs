using System;
using System.Collections;
using System.Data;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.Core
{
    public abstract class PropertyConfiguration : IEquatable<PropertyConfiguration>
    {
        #region Private 
        #endregion

        #region Protected 
        protected internal bool IsOut { get; protected set; }

        protected internal string PropertyName { get; protected set; }

        protected internal string ParameterName { get; protected set; }

        protected internal Type DataType { get; protected set; }

        protected internal uint Size1 { get; protected set; }

        protected internal uint Size2 { get; protected set; }

        #endregion

        #region Internals

        internal PropertyConfiguration( bool Enumerable = false)
        {
            IsEnumerable = Enumerable;
        }

        internal bool IsEnumerable { get; }

        internal abstract SqlDbType GetDbType { get; }
        #endregion

        public bool Equals(PropertyConfiguration other)
        {
            if (other.PropertyName == this.PropertyName)
                return true;
            else
                return false;
        }
    }
}