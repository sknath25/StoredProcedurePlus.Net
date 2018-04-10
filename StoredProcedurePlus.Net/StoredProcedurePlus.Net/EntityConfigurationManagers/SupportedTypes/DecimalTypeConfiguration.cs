using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class DecimalTypeConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, decimal> where TContainerType : class
    {
        public DecimalTypeConfiguration(Expression<Func<TContainerType, decimal>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.Decimal;
            }
        }

        protected override decimal Validate(decimal value)
        {
            if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value, AllowedMaxValue.Value);
            if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value, AllowedMinValue.Value);

            if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
            {
                if (!Array.Exists<decimal>(AllowedValuesOnly, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesOnly);
            }

            if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
            {
                if (Array.Exists<decimal>(AllowedValuesExcept, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesExcept);
            }

            return base.Validate(value);
        }

        public DecimalTypeConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public DecimalTypeConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal byte? ScaleSize = null;
        internal byte? PrecisionSize = null;
        public DecimalTypeConfiguration<TContainerType> HasSize(byte scale, byte precision)
        {
            ScaleSize = scale;
            PrecisionSize = precision;
            return this;
        }


        decimal? AllowedMaxValue = null;
        public DecimalTypeConfiguration<TContainerType> Max(decimal value)
        {
            AllowedMaxValue = value;
            return this;
        }

        decimal? AllowedMinValue = null;
        public DecimalTypeConfiguration<TContainerType> Min(decimal value)
        {
            AllowedMinValue = value;
            return this;
        }

        decimal[] AllowedValuesOnly = null;
        public DecimalTypeConfiguration<TContainerType> AllowedOnly(decimal[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        decimal[] AllowedValuesExcept = null;
        public DecimalTypeConfiguration<TContainerType> AllowedExcept(decimal[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
