using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class IntegerTypeConfiguration<S> : PrimitiveTypeConfiguration<S, int>
    {
        public IntegerTypeConfiguration(Expression<Func<S, int>> memberSelector):base(memberSelector)
        {
        }

        protected override int Validate(int value)
        {
            if (AllowedMaxValue.HasValue && value > AllowedMaxValue) throw new Exception("bahut bara hay");
            if (AllowedMinValue.HasValue && value < AllowedMinValue) throw new Exception("bahut chota hay");
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
