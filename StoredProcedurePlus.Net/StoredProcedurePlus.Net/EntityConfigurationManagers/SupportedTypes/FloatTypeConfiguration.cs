using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class FloatTypeConfiguration<S> : PrimitiveTypeConfiguration<S, float> where S : class
    {
        public FloatTypeConfiguration(Expression<Func<S, float>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.Float;
            }
        }

        protected override float Validate(float value)
        {
            if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value, AllowedMaxValue.Value);
            if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value, AllowedMinValue.Value);

            if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
            {
                if (!Array.Exists<float>(AllowedValuesOnly, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesOnly);
            }

            if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
            {
                if (Array.Exists<float>(AllowedValuesExcept, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesExcept);
            }

            return base.Validate(value);
        }

        public FloatTypeConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public FloatTypeConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }


        internal byte? ScaleSize = null;
        internal byte? PrecisionSize = null;
        public FloatTypeConfiguration<S> HasSize(byte scale, byte precision)
        {
            ScaleSize = scale;
            PrecisionSize = precision;
            return this;
        }


        float? AllowedMaxValue = null;
        public FloatTypeConfiguration<S> Max(float value)
        {
            AllowedMaxValue = value;
            return this;
        }

        float? AllowedMinValue = null;
        public FloatTypeConfiguration<S> Min(float value)
        {
            AllowedMinValue = value;
            return this;
        }

        float[] AllowedValuesOnly = null;
        public FloatTypeConfiguration<S> AllowedOnly(float[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        float[] AllowedValuesExcept = null;
        public FloatTypeConfiguration<S> AllowedExcept(float[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
