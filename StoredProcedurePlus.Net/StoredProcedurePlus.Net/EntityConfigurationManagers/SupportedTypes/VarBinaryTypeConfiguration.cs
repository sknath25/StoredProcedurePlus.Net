using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public class VarBinaryTypeConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType,byte[]> where TContainerType : class
    {
        public VarBinaryTypeConfiguration(Expression<Func<TContainerType, byte[]>> memberSelector):base(memberSelector)
        {

        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.VarBinary;
            }
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

            return base.Validate(value);
        }

        public VarBinaryTypeConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public VarBinaryTypeConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal bool IsRequired { get; private set; }
        public VarBinaryTypeConfiguration<TContainerType> Required()
        {
            this.IsRequired = true;
            return this;
        }

        uint? AllowedMaxLength = null;
        public VarBinaryTypeConfiguration<TContainerType> MaxLength(uint value)
        {
            AllowedMaxLength = value;
            base.Size1 = value;
            return this;
        }

        uint? AllowedMinLength = null;
        public VarBinaryTypeConfiguration<TContainerType> MinLength(uint value)
        {
            AllowedMinLength = value;
            return this;
        }
    }
}
