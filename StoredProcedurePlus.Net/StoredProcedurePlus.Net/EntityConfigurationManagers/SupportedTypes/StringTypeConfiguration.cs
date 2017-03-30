using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public class StringTypeConfiguration<S> : PrimitiveTypeConfiguration<S,string> where S : class
    {
        public StringTypeConfiguration(Expression<Func<S, string>> memberSelector):base(memberSelector)
        {

        }
        internal override SqlDbType GetSqlDbType()
        {
            return SqlDbType.VarChar;
        }

        protected override string ValidateToDb(string value)
        {
            if (IsRequired && value==null) Error.RequiredPropertyValidationError(PropertyName);
            if (value != null)
            {
                int Length = value.Length;
                if (AllowedMaxLength.HasValue && Length > AllowedMaxLength) Error.MaxLengthPropertyValidationError(PropertyName, Length, AllowedMaxLength.Value);
                if (AllowedMinLength.HasValue && Length < AllowedMinLength) Error.MinLengthPropertyValidationError(PropertyName, Length, AllowedMinLength.Value);
            }
            base.ValidateToDb(value);
            return value;
        }

        public StringTypeConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public StringTypeConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

 
        internal bool IsRequired { get; private set; }
        public StringTypeConfiguration<S> Required()
        {
            this.IsRequired = true;
            return this;
        }

        internal int? AllowedMaxLength = null;
        public StringTypeConfiguration<S> MaxLength(int value)
        {
            AllowedMaxLength = value;
            return this;
        }

        internal int? AllowedMinLength = null;
        public StringTypeConfiguration<S> MinLength(int value)
        {
            AllowedMinLength = value;
            return this;
        }

        public StringTypeConfiguration<S> AllowedOnly(string[] values)
        {
            return this;
        }

        public StringTypeConfiguration<S> AllowedExcept(string[] values)
        {
            return this;
        }
    }
}
