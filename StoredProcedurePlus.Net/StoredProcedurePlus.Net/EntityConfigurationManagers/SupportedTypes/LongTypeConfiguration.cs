using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class LongTypeConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, long> where TContainerType : class
    {
        public LongTypeConfiguration(Expression<Func<TContainerType, long>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.BigInt;
            }
        }

        protected override long Validate(long value)
        {
            if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value, AllowedMaxValue.Value);
            if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value, AllowedMinValue.Value);

            if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
            {
                if (!Array.Exists<long>(AllowedValuesOnly, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesOnly);
            }

            if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
            {
                if (Array.Exists<long>(AllowedValuesExcept, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesExcept);
            }

            return base.Validate(value);
        }

        public LongTypeConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public LongTypeConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }


        long? AllowedMaxValue = null;        
        public LongTypeConfiguration<TContainerType> Max(long value)
        {
            AllowedMaxValue = value;
            return this;
        }

        long? AllowedMinValue = null;
        public LongTypeConfiguration<TContainerType> Min(long value)
        {
            AllowedMinValue = value;
            return this;
        }

        long[] AllowedValuesOnly = null;
        public LongTypeConfiguration<TContainerType> AllowedOnly(long[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        long[] AllowedValuesExcept = null;        
        public LongTypeConfiguration<TContainerType> AllowedExcept(long[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
