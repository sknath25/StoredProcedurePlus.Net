using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public class StringTypeConfiguration<S> : PrimitiveTypeConfiguration<S,string>
    {
        public StringTypeConfiguration(Expression<Func<S, string>> memberSelector):base(memberSelector)
        {

        }        

        protected override string Validate(string value)
        {
            if (IsRequired && string.IsNullOrEmpty(value)) throw new Exception("Nahi re babua");
            base.Validate(value);
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

        public StringTypeConfiguration<S> MinLength(int value)
        {
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
