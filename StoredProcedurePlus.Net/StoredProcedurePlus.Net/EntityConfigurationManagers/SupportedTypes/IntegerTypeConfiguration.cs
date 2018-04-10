using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class IntegerTypeConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, int> where TContainerType : class
    {
        public IntegerTypeConfiguration(Expression<Func<TContainerType, int>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.Int;
            }
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

            return base.Validate(value);
        }

        public IntegerTypeConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public IntegerTypeConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }


        int? AllowedMaxValue = null;        
        public IntegerTypeConfiguration<TContainerType> Max(int value)
        {
            AllowedMaxValue = value;
            return this;
        }

        int? AllowedMinValue = null;
        public IntegerTypeConfiguration<TContainerType> Min(int value)
        {
            AllowedMinValue = value;
            return this;
        }

        int[] AllowedValuesOnly = null;
        public IntegerTypeConfiguration<TContainerType> AllowedOnly(int[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        int[] AllowedValuesExcept = null;        

        public IntegerTypeConfiguration<TContainerType> AllowedExcept(int[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
