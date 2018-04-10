using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class DoubleTypeConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, double> where TContainerType : class
    {
        public DoubleTypeConfiguration(Expression<Func<TContainerType, double>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.Float;
            }
        }

        protected override double Validate(double value)
        {
            if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value, AllowedMaxValue.Value);
            if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value, AllowedMinValue.Value);

            if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
            {
                if (!Array.Exists<double>(AllowedValuesOnly, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesOnly);
            }

            if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
            {
                if (Array.Exists<double>(AllowedValuesExcept, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesExcept);
            }

            return base.Validate(value);
        }

        public DoubleTypeConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public DoubleTypeConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }


        internal byte? ScaleSize = null;
        internal byte? PrecisionSize = null;
        public DoubleTypeConfiguration<TContainerType> HasSize(byte scale, byte precision)
        {
            ScaleSize = scale;
            PrecisionSize = precision;
            return this;
        }


        double? AllowedMaxValue = null;
        public DoubleTypeConfiguration<TContainerType> Max(double value)
        {
            AllowedMaxValue = value;
            return this;
        }

        double? AllowedMinValue = null;
        public DoubleTypeConfiguration<TContainerType> Min(double value)
        {
            AllowedMinValue = value;
            return this;
        }

        double[] AllowedValuesOnly = null;
        public DoubleTypeConfiguration<TContainerType> AllowedOnly(double[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        double[] AllowedValuesExcept = null;
        public DoubleTypeConfiguration<TContainerType> AllowedExcept(double[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
