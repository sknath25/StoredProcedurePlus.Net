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

        //protected void SetChild(PropertyConfiguration c)
        //{
        //    Child = c;
        //    HasChild = true;
        //}

        //protected void UnsetChild()
        //{
        //    Child = null;
        //    HasChild = false;
        //}

        #endregion

        #region Internals

        //internal PropertyConfiguration(DbType t)
        //{
        //    GetDbType = t;
        //}

        internal PropertyConfiguration(SqlDbType t, bool Enumerable = false)
        {
            //HasChild = false;
            GetDbType = t;
            IsEnumerable = Enumerable;
        }

        //internal abstract IEnumerator GetEnumerator(IList collection);

        //internal PropertyConfiguration Child { get; private set; }

        //internal bool HasChild { get; private set; }

        internal bool IsEnumerable { get; }

        internal SqlDbType GetDbType { get; }
        #endregion

        public bool Equals(PropertyConfiguration other)
        {
            if (other.PropertyName == this.PropertyName) return true; else return false;
        }


    }
}