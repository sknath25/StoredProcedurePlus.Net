using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class BoolTypeNullableConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, bool?> where TContainerType : class
    {
        public BoolTypeNullableConfiguration(Expression<Func<TContainerType, bool?>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.Bit;
            }
        }

        protected override bool? Validate(bool? value)
        {
            if (IsRequired && !value.HasValue) Error.RequiredPropertyValidationError(PropertyName);

            if (value.HasValue)
            {
                if (AllowedValuesOnly != value)
                {
                    Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesOnly);
                }
            }

            return base.Validate(value);
        }

        public BoolTypeNullableConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public BoolTypeNullableConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal bool IsRequired { get; private set; }
        public BoolTypeNullableConfiguration<TContainerType> Required()
        {
            this.IsRequired = true;
            return this;
        }

        bool AllowedValuesOnly = true;
        public BoolTypeNullableConfiguration<TContainerType> AllowedOnly(bool values)
        {
            AllowedValuesOnly = values;
            return this;
        }
    }
}
