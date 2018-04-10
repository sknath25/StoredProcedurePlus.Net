using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class LongTypeNullableConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, long?> where TContainerType : class
    {
        public LongTypeNullableConfiguration(Expression<Func<TContainerType, long?>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.BigInt;
            }
        }

        protected override long? Validate(long? value)
        {
            if (IsRequired && !value.HasValue) Error.RequiredPropertyValidationError(PropertyName);

            if (value.HasValue)
            {
                if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value.Value, AllowedMaxValue.Value);
                if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value.Value, AllowedMinValue.Value);

                if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
                {
                    if (!Array.Exists<long>(AllowedValuesOnly, v => v.Equals(value.Value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesOnly);
                }

                if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
                {
                    if (Array.Exists<long>(AllowedValuesExcept, v => v.Equals(value.Value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesExcept);
                }
            }

            return base.Validate(value);
        }

        public LongTypeNullableConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public LongTypeNullableConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal bool IsRequired { get; private set; }
        public LongTypeNullableConfiguration<TContainerType> Required()
        {
            this.IsRequired = true;
            return this;
        }


        long? AllowedMaxValue = null;        
        public LongTypeNullableConfiguration<TContainerType> Max(long value)
        {
            AllowedMaxValue = value;
            return this;
        }

        long? AllowedMinValue = null;
        public LongTypeNullableConfiguration<TContainerType> Min(long value)
        {
            AllowedMinValue = value;
            return this;
        }

        long[] AllowedValuesOnly = null;
        public LongTypeNullableConfiguration<TContainerType> AllowedOnly(long[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        long[] AllowedValuesExcept = null;        

        public LongTypeNullableConfiguration<TContainerType> AllowedExcept(long[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
