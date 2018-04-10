using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class FloatTypeNullableConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, float?> where TContainerType : class
    {
        public FloatTypeNullableConfiguration(Expression<Func<TContainerType, float?>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.Float;
            }
        }

        protected override float? Validate(float? value)
        {
            if (IsRequired && !value.HasValue) Error.RequiredPropertyValidationError(PropertyName);

            if (value.HasValue)
            {
                if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value.Value, AllowedMaxValue.Value);
                if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value.Value, AllowedMinValue.Value);

                if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
                {
                    if (!Array.Exists<float>(AllowedValuesOnly, v => v.Equals(value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesOnly);
                }

                if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
                {
                    if (Array.Exists<float>(AllowedValuesExcept, v => v.Equals(value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesExcept);
                }
            }

            return base.Validate(value);
        }

        internal bool IsRequired { get; private set; }
        public FloatTypeNullableConfiguration<TContainerType> Required()
        {
            this.IsRequired = true;
            return this;
        }

        public FloatTypeNullableConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public FloatTypeNullableConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }


        internal byte? ScaleSize = null;
        internal byte? PrecisionSize = null;
        public FloatTypeNullableConfiguration<TContainerType> HasSize(byte scale, byte precision)
        {
            ScaleSize = scale;
            PrecisionSize = precision;
            return this;
        }


        float? AllowedMaxValue = null;
        public FloatTypeNullableConfiguration<TContainerType> Max(float value)
        {
            AllowedMaxValue = value;
            return this;
        }

        float? AllowedMinValue = null;
        public FloatTypeNullableConfiguration<TContainerType> Min(float value)
        {
            AllowedMinValue = value;
            return this;
        }

        float[] AllowedValuesOnly = null;
        public FloatTypeNullableConfiguration<TContainerType> AllowedOnly(float[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        float[] AllowedValuesExcept = null;
        public FloatTypeNullableConfiguration<TContainerType> AllowedExcept(float[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
