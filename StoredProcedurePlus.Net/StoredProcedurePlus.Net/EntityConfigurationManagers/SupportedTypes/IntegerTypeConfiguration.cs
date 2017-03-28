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

        internal override SqlDbType GetSqlDbType()
        {
            return SqlDbType.Int;
        }

        protected override int Validate(int value)
        {
            if (AllowedMaxValue.HasValue && value > AllowedMaxValue) throw new Exception("bahut bara hay");
            if (AllowedMinValue.HasValue && value < AllowedMinValue) throw new Exception("bahut chota hay");
            return value;
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
