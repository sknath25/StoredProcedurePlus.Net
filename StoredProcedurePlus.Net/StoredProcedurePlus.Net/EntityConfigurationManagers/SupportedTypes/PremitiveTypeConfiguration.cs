using System.Data;
using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using System;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public abstract class PrimitiveTypeConfiguration<S,T>: PropertyConfiguration
    {
        readonly EntityAccessor<S, T> Accessor;

        protected PrimitiveTypeConfiguration(Expression<Func<S, T>> memberSelector)
        {
            Accessor = EntityAccessor<S>.Create(memberSelector);
            PropertyName = Accessor.PropertyName;
            ParameterName = PropertyName;
            DataType = Accessor.DataType;
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
                      
        protected virtual T Validate(T value)
        {
            return value;
        }
    }
}