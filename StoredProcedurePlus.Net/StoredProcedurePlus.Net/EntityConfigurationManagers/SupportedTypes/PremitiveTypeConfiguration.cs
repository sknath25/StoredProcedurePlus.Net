using System.Data;
using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using System;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public abstract class PrimitiveTypeConfiguration<S,T>
    {
        readonly EntityAccessor<S, T> Accessor;

        protected PrimitiveTypeConfiguration(Expression<Func<S, T>> memberSelector)
        {
            Accessor = EntityAccessor<S>.Create(memberSelector);
            PropertyName = Accessor.PropertyName;
            ParameterName = PropertyName;
        }

        public T this[S instance]
        {
            get
            {
                T Result = Accessor[instance];
                //Do evaluation here
                return Validate(Result);
            }
            set
            {
                //Do evaluation here 
                Accessor[instance] = Validate(value);
            }
        }

        protected internal string PropertyName { get; protected set; }
        
               
        internal bool IsOut { get; private set; }
        public PrimitiveTypeConfiguration<S,T> Out()
        {
            this.IsOut = true;
            return this;
        }

        internal string ParameterName { get; private set; }
        public PrimitiveTypeConfiguration<S,T> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        protected virtual T Validate(T value)
        {
            return value;
        }

        internal abstract SqlDbType GetSqlDbType();

    }
}