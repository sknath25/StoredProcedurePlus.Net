using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class DateTimeTypeConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, DateTime> where TContainerType : class
    {
        public DateTimeTypeConfiguration(Expression<Func<TContainerType, DateTime>> memberSelector):base(memberSelector)
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

        protected override DateTime Validate(DateTime value)
        {
            if (AllowedMaxDate.HasValue && value > AllowedMaxDate) Error.MaxDatePropertyValidationError(PropertyName, value, AllowedMaxDate.Value);
            if (AllowedMinDate.HasValue && value < AllowedMinDate) Error.MinDatePropertyValidationError(PropertyName, value, AllowedMinDate.Value);

            if(AllowedDatesOnly!=null && AllowedDatesOnly.Length > 0)
            {
                if(!Array.Exists<DateTime>(AllowedDatesOnly, v => v.Equals(value)))
                    Error.DateNotAllowedError(PropertyName, value, AllowedDatesOnly);
            }

            if (AllowedDatesExcept != null && AllowedDatesExcept.Length > 0)
            {
                if (Array.Exists<DateTime>(AllowedDatesExcept, v => v.Equals(value)))
                    Error.DateNotAllowedError(PropertyName, value, AllowedDatesExcept);
            }

            return base.Validate(value);
        }

        public DateTimeTypeConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public DateTimeTypeConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        bool IsDateTime2 = false;
        public DateTimeTypeConfiguration<TContainerType> AsDateTime2()
        {
            IsDateTime2 = true;
            return this;
        }

        DateTime? AllowedMaxDate = null;
        public DateTimeTypeConfiguration<TContainerType> Max(DateTime value)
        {
            AllowedMaxDate = value;
            return this;
        }

        DateTime? AllowedMinDate = null;
        public DateTimeTypeConfiguration<TContainerType> Min(DateTime value)
        {
            AllowedMinDate = value;
            return this;
        }

        DateTime[] AllowedDatesOnly = null;
        public DateTimeTypeConfiguration<TContainerType> AllowedOnly(DateTime[] values)
        {
            AllowedDatesOnly = values;
            return this;
        }

        DateTime[] AllowedDatesExcept = null;
        public DateTimeTypeConfiguration<TContainerType> AllowedExcept(DateTime[] values)
        {
            AllowedDatesExcept = values;
            return this;
        }

        string AllowedDateTimeFormat = null;
        public DateTimeTypeConfiguration<TContainerType> Format(string value)
        {
            AllowedDateTimeFormat = value;
            return this;
        }
    }
}
