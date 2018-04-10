using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class IntegerTypeNullableConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, int?> where TContainerType : class
    {
        public IntegerTypeNullableConfiguration(Expression<Func<TContainerType, int?>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.Int;
            }
        }

        protected override int? Validate(int? value)
        {
            if (IsRequired && !value.HasValue) Error.RequiredPropertyValidationError(PropertyName);

            if (value.HasValue)
            {
                if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value.Value, AllowedMaxValue.Value);
                if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value.Value, AllowedMinValue.Value);

                if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
                {
                    if (!Array.Exists<int>(AllowedValuesOnly, v => v.Equals(value.Value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesOnly);
                }

                if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
                {
                    if (Array.Exists<int>(AllowedValuesExcept, v => v.Equals(value.Value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesExcept);
                }
            }

            return base.Validate(value);
        }

        public IntegerTypeNullableConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public IntegerTypeNullableConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal bool IsRequired { get; private set; }
        public IntegerTypeNullableConfiguration<TContainerType> Required()
        {
            this.IsRequired = true;
            return this;
        }


        int? AllowedMaxValue = null;        
        public IntegerTypeNullableConfiguration<TContainerType> Max(int value)
        {
            AllowedMaxValue = value;
            return this;
        }

        int? AllowedMinValue = null;
        public IntegerTypeNullableConfiguration<TContainerType> Min(int value)
        {
            AllowedMinValue = value;
            return this;
        }

        int[] AllowedValuesOnly = null;
        public IntegerTypeNullableConfiguration<TContainerType> AllowedOnly(int[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        int[] AllowedValuesExcept = null;        

        public IntegerTypeNullableConfiguration<TContainerType> AllowedExcept(int[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
