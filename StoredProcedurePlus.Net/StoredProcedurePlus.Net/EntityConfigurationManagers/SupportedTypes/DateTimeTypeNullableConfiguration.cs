﻿using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class DateTimeTypeNullableConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, DateTime?> where TContainerType : class
    {
        public DateTimeTypeNullableConfiguration(Expression<Func<TContainerType, DateTime?>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                if (IsDateTime2)
                {
                    return SqlDbType.DateTime2;
                }
                return SqlDbType.DateTime;
            }
        }

        protected override DateTime? Validate(DateTime? value)
        {
            if (IsRequired && !value.HasValue) Error.RequiredPropertyValidationError(PropertyName);

            if (value.HasValue)
            {
                if (AllowedMaxDate.HasValue && value > AllowedMaxDate) Error.MaxDatePropertyValidationError(PropertyName, value.Value, AllowedMaxDate.Value);
                if (AllowedMinDate.HasValue && value < AllowedMinDate) Error.MinDatePropertyValidationError(PropertyName, value.Value, AllowedMinDate.Value);

                if (AllowedDatesOnly != null && AllowedDatesOnly.Length > 0)
                {
                    if (!Array.Exists<DateTime>(AllowedDatesOnly, v => v.Equals(value)))
                        Error.DateNotAllowedError(PropertyName, value.Value, AllowedDatesOnly);
                }

                if (AllowedDatesExcept != null && AllowedDatesExcept.Length > 0)
                {
                    if (Array.Exists<DateTime>(AllowedDatesExcept, v => v.Equals(value)))
                        Error.DateNotAllowedError(PropertyName, value.Value, AllowedDatesExcept);
                }
            }

            return base.Validate(value);
        }

        public DateTimeTypeNullableConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public DateTimeTypeNullableConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }


        bool IsDateTime2 = false;
        public DateTimeTypeNullableConfiguration<TContainerType> AsDateTime2()
        {
            IsDateTime2 = true;
            return this;
        }

        internal bool IsRequired { get; private set; }
        public DateTimeTypeNullableConfiguration<TContainerType> Required()
        {
            this.IsRequired = true;
            return this;
        }

        DateTime? AllowedMaxDate = null;
        public DateTimeTypeNullableConfiguration<TContainerType> Max(DateTime value)
        {
            AllowedMaxDate = value;
            return this;
        }

        DateTime? AllowedMinDate = null;
        public DateTimeTypeNullableConfiguration<TContainerType> Min(DateTime value)
        {
            AllowedMinDate = value;
            return this;
        }

        DateTime[] AllowedDatesOnly = null;
        public DateTimeTypeNullableConfiguration<TContainerType> AllowedOnly(DateTime[] values)
        {
            AllowedDatesOnly = values;
            return this;
        }

        DateTime[] AllowedDatesExcept = null;
        public DateTimeTypeNullableConfiguration<TContainerType> AllowedExcept(DateTime[] values)
        {
            AllowedDatesExcept = values;
            return this;
        }

        string AllowedDateTimeFormat = null;
        public DateTimeTypeNullableConfiguration<TContainerType> Format(string value)
        {
            AllowedDateTimeFormat = value;
            return this;
        }
    }
}
