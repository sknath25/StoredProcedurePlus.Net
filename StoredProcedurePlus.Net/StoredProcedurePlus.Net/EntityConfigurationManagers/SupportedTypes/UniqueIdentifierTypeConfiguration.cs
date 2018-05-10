using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class UniqueIdentifierTypeConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, Guid> where TContainerType : class
    {
        public UniqueIdentifierTypeConfiguration(Expression<Func<TContainerType, Guid>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.UniqueIdentifier;
            }
        }

        protected override Guid Validate(Guid value)
        {
            return base.Validate(value);
        }

        public UniqueIdentifierTypeConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public UniqueIdentifierTypeConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }
    }
}
