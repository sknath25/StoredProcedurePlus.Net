using System;
using System.Data;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.Core
{
    public abstract class PropertyConfiguration : IEquatable<PropertyConfiguration>
    {
        public PropertyConfiguration(DbType t)
        {
            _DbType = t;
        }

        public PropertyConfiguration(bool CanEnumerate)
        {
            _DbType =  DbType.Object;
            _IsEnumerable = CanEnumerate;
        }

        protected internal string PropertyName { get; protected set; }

        protected internal bool IsOut { get; protected set; }

        protected internal string ParameterName { get; protected set; }

        protected internal Type DataType { get; protected set; }

        public bool Equals(PropertyConfiguration other)
        {
            if (other.PropertyName == this.PropertyName) return true; else return false;
        }

        bool _IsEnumerable { get; set; }

        internal bool IsEnumerable
        {
            get
            {
                return _IsEnumerable;
            }
        }

        DbType _DbType { get; set; }

        internal DbType GetDbType
        {
            get
            {
                return _DbType;
            }
        }

        protected internal uint Size1 { get; protected set; }

        protected internal uint Size2 { get; protected set; }
    }
}