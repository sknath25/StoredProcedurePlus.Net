﻿using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class DoubleTypeConfiguration<S> : PrimitiveTypeConfiguration<S, double> where S : class
    {
        public DoubleTypeConfiguration(Expression<Func<S, double>> memberSelector):base(memberSelector, SqlDbType.Decimal)
        {
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

            base.Validate(value);
            return value;
        }

        public DoubleTypeConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public DoubleTypeConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }


        internal byte? ScaleSize = null;
        internal byte? PrecisionSize = null;
        public DoubleTypeConfiguration<S> HasSize(byte scale, byte precision)
        {
            ScaleSize = scale;
            PrecisionSize = precision;
            return this;
        }


        double? AllowedMaxValue = null;
        public DoubleTypeConfiguration<S> Max(double value)
        {
            AllowedMaxValue = value;
            return this;
        }

        double? AllowedMinValue = null;
        public DoubleTypeConfiguration<S> Min(double value)
        {
            AllowedMinValue = value;
            return this;
        }

        double[] AllowedValuesOnly = null;
        public DoubleTypeConfiguration<S> AllowedOnly(double[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        double[] AllowedValuesExcept = null;
        public DoubleTypeConfiguration<S> AllowedExcept(double[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
