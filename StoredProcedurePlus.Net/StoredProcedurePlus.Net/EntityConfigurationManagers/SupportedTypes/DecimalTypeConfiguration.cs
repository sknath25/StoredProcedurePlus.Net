using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class DecimalTypeConfiguration<S> : PrimitiveTypeConfiguration<S, decimal> where S : class
    {
        public DecimalTypeConfiguration(Expression<Func<S, decimal>> memberSelector):base(memberSelector)
        {
        }
        internal override SqlDbType GetSqlDbType()
        {
            return SqlDbType.Decimal;
        }

        protected override decimal ValidateToDb(decimal value)
        {
            if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value, AllowedMaxValue.Value);
            if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value, AllowedMinValue.Value);
            base.ValidateToDb(value);
            return value;
        }

        public DecimalTypeConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public DecimalTypeConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }


        decimal? AllowedMaxValue = null;
        decimal? AllowedMinValue = null;

        public DecimalTypeConfiguration<S> Max(decimal value)
        {
            AllowedMaxValue = value;
            return this;
        }
        public DecimalTypeConfiguration<S> Min(decimal value)
        {
            AllowedMinValue = value;
            return this;
        }

        public DecimalTypeConfiguration<S> AllowedOnly(decimal[] values)
        {
            return this;
        }

        public DecimalTypeConfiguration<S> AllowedExcept(decimal[] values)
        {
            return this;
        }
    }
}
