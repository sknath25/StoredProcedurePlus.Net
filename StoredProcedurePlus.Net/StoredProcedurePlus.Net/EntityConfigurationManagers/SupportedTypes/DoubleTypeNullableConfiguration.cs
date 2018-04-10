using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class DoubleTypeNullableConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, double?> where TContainerType : class
    {
        public DoubleTypeNullableConfiguration(Expression<Func<TContainerType, double?>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.Float;
            }
        }

        protected override double? Validate(double? value)
        {
            if (IsRequired && !value.HasValue) Error.RequiredPropertyValidationError(PropertyName);

            if (value.HasValue)
            {
                if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value.Value, AllowedMaxValue.Value);
                if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value.Value, AllowedMinValue.Value);

                if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
                {
                    if (!Array.Exists<double>(AllowedValuesOnly, v => v.Equals(value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesOnly);
                }

                if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
                {
                    if (Array.Exists<double>(AllowedValuesExcept, v => v.Equals(value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesExcept);
                }
            }

            return base.Validate(value);
        }

        internal bool IsRequired { get; private set; }
        public DoubleTypeNullableConfiguration<TContainerType> Required()
        {
            this.IsRequired = true;
            return this;
        }

        public DoubleTypeNullableConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public DoubleTypeNullableConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal byte? ScaleSize = null;
        internal byte? PrecisionSize = null;
        public DoubleTypeNullableConfiguration<TContainerType> HasSize(byte scale, byte precision)
        {
            ScaleSize = scale;
            PrecisionSize = precision;
            return this;
        }


        double? AllowedMaxValue = null;
        public DoubleTypeNullableConfiguration<TContainerType> Max(double value)
        {
            AllowedMaxValue = value;
            return this;
        }

        double? AllowedMinValue = null;
        public DoubleTypeNullableConfiguration<TContainerType> Min(double value)
        {
            AllowedMinValue = value;
            return this;
        }

        double[] AllowedValuesOnly = null;
        public DoubleTypeNullableConfiguration<TContainerType> AllowedOnly(double[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        double[] AllowedValuesExcept = null;
        public DoubleTypeNullableConfiguration<TContainerType> AllowedExcept(double[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
