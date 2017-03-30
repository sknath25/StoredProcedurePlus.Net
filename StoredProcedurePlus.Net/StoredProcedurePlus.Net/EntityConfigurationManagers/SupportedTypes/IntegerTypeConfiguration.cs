﻿using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class IntegerTypeConfiguration<S> : PrimitiveTypeConfiguration<S, int> where S : class
    {
        public IntegerTypeConfiguration(Expression<Func<S, int>> memberSelector):base(memberSelector)
        {
        }
        internal override SqlDbType GetSqlDbType()
        {
            return SqlDbType.Int;
        }

        protected override int ValidateToDb(int value)
        {
            if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value, AllowedMaxValue.Value);
            if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value, AllowedMinValue.Value);
            base.ValidateToDb(value);
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
        int? AllowedMinValue = null;

        public IntegerTypeConfiguration<S> Max(int value)
        {
            AllowedMaxValue = value;
            return this;
        }
        public IntegerTypeConfiguration<S> Min(int value)
        {
            AllowedMinValue = value;
            return this;
        }

        public IntegerTypeConfiguration<S> AllowedOnly(int[] values)
        {
            return this;
        }

        public IntegerTypeConfiguration<S> AllowedExcept(int[] values)
        {
            return this;
        }
    }
}
