using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class DecimalTypeNullableConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, decimal?> where TContainerType : class
    {
        public DecimalTypeNullableConfiguration(Expression<Func<TContainerType, decimal?>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.Decimal;
            }
        }

        protected override decimal? Validate(decimal? value)
        {
            if (IsRequired && !value.HasValue) Error.RequiredPropertyValidationError(PropertyName);

            if (value.HasValue)
            {
                if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value.Value, AllowedMaxValue.Value);
                if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value.Value, AllowedMinValue.Value);

                if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
                {
                    if (!Array.Exists<decimal>(AllowedValuesOnly, v => v.Equals(value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesOnly);
                }

                if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
                {
                    if (Array.Exists<decimal>(AllowedValuesExcept, v => v.Equals(value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesExcept);
                }
            }

            return base.Validate(value);
        }

        internal bool IsRequired { get; private set; }
        public DecimalTypeNullableConfiguration<TContainerType> Required()
        {
            this.IsRequired = true;
            return this;
        }

        public DecimalTypeNullableConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public DecimalTypeNullableConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal byte? ScaleSize = null;
        internal byte? PrecisionSize = null;
        public DecimalTypeNullableConfiguration<TContainerType> HasSize(byte scale, byte precision)
        {
            ScaleSize = scale;
            PrecisionSize = precision;
            return this;
        }


        decimal? AllowedMaxValue = null;
        public DecimalTypeNullableConfiguration<TContainerType> Max(decimal value)
        {
            AllowedMaxValue = value;
            return this;
        }

        decimal? AllowedMinValue = null;
        public DecimalTypeNullableConfiguration<TContainerType> Min(decimal value)
        {
            AllowedMinValue = value;
            return this;
        }

        decimal[] AllowedValuesOnly = null;
        public DecimalTypeNullableConfiguration<TContainerType> AllowedOnly(decimal[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        decimal[] AllowedValuesExcept = null;
        public DecimalTypeNullableConfiguration<TContainerType> AllowedExcept(decimal[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
