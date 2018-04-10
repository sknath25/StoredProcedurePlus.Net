using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class ShortTypeConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, short> where TContainerType : class
    {
        public ShortTypeConfiguration(Expression<Func<TContainerType, short>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.SmallInt;
            }
        }

        protected override short Validate(short value)
        {
            if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value, AllowedMaxValue.Value);
            if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value, AllowedMinValue.Value);

            if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
            {
                if (!Array.Exists<short>(AllowedValuesOnly, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesOnly);
            }

            if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
            {
                if (Array.Exists<short>(AllowedValuesExcept, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesExcept);
            }

            return base.Validate(value);
        }

        public ShortTypeConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public ShortTypeConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }


        short? AllowedMaxValue = null;        
        public ShortTypeConfiguration<TContainerType> Max(short value)
        {
            AllowedMaxValue = value;
            return this;
        }

        short? AllowedMinValue = null;
        public ShortTypeConfiguration<TContainerType> Min(short value)
        {
            AllowedMinValue = value;
            return this;
        }

        short[] AllowedValuesOnly = null;
        public ShortTypeConfiguration<TContainerType> AllowedOnly(short[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        short[] AllowedValuesExcept = null;        
        public ShortTypeConfiguration<TContainerType> AllowedExcept(short[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
