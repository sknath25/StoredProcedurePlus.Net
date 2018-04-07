using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public class VarBinaryTypeConfiguration<S> : PrimitiveTypeConfiguration<S,byte[]> where S : class
    {
        public VarBinaryTypeConfiguration(Expression<Func<S, byte[]>> memberSelector):base(memberSelector, SqlDbType.VarBinary)
        {

        }

        protected override byte[] Validate(byte [] value)
        {
            if (IsRequired && value==null) Error.RequiredPropertyValidationError(PropertyName);

            if (value != null)
            {
                int Length = value.Length;

                if (AllowedMaxLength.HasValue && Length > AllowedMaxLength) Error.MaxLengthPropertyValidationError(PropertyName, Length, AllowedMaxLength.Value);
                if (AllowedMinLength.HasValue && Length < AllowedMinLength) Error.MinLengthPropertyValidationError(PropertyName, Length, AllowedMinLength.Value);
            }

            base.Validate(value);
            return value;
        }

        public VarBinaryTypeConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public VarBinaryTypeConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal bool IsRequired { get; private set; }
        public VarBinaryTypeConfiguration<S> Required()
        {
            this.IsRequired = true;
            return this;
        }

        uint? AllowedMaxLength = null;
        public VarBinaryTypeConfiguration<S> MaxLength(uint value)
        {
            AllowedMaxLength = value;
            base.Size1 = value;
            return this;
        }

        uint? AllowedMinLength = null;
        public VarBinaryTypeConfiguration<S> MinLength(uint value)
        {
            AllowedMinLength = value;
            return this;
        }
    }
}
