using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class IntegerTypeConfiguration<S> : PrimitiveTypeConfiguration<S, int> where S : class
    {
        public IntegerTypeConfiguration(Expression<Func<S, int>> memberSelector):base(memberSelector, SqlDbType.Int)
        {
        }

        protected override int Validate(int value)
        {
            if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value, AllowedMaxValue.Value);
            if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value, AllowedMinValue.Value);

            if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
            {
                if (!Array.Exists<int>(AllowedValuesOnly, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesOnly);
            }

            if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
            {
                if (Array.Exists<int>(AllowedValuesExcept, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesExcept);
            }

            base.Validate(value);
            return value;
        }

        public IntegerTypeConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public IntegerTypeConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }


        int? AllowedMaxValue = null;        
        public IntegerTypeConfiguration<S> Max(int value)
        {
            AllowedMaxValue = value;
            return this;
        }

        int? AllowedMinValue = null;
        public IntegerTypeConfiguration<S> Min(int value)
        {
            AllowedMinValue = value;
            return this;
        }

        int[] AllowedValuesOnly = null;
        public IntegerTypeConfiguration<S> AllowedOnly(int[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        int[] AllowedValuesExcept = null;        

        public IntegerTypeConfiguration<S> AllowedExcept(int[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
