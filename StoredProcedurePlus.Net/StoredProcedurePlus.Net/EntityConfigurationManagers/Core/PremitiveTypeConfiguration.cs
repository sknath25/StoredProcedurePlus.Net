using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.Core
{
    public abstract class PrimitiveTypeConfiguration<TContainerType,TPropertyType>: PropertyConfiguration where TContainerType : class
    {
        readonly EntityAccessor<TContainerType, TPropertyType> Accessor;

        protected PrimitiveTypeConfiguration(Expression<Func<TContainerType, TPropertyType>> memberSelector):base()
        {
            Accessor = EntityAccessor<TContainerType>.Create(memberSelector);
            PropertyName = Accessor.PropertyName;
            ParameterName = PropertyName;
            DataType = Accessor.DataType;
        }

        public TPropertyType this[TContainerType instance]
        {
            get
            {
                TPropertyType Result = Validate(Accessor[instance]);
                return Result;
            }
            set
            {
                Accessor[instance] = value;
            }
        }

        protected virtual TPropertyType Validate(TPropertyType value)
        {
            return value;
        }
    }
}