﻿using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class BoolTypeConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType, bool> where TContainerType : class
    {
        public BoolTypeConfiguration(Expression<Func<TContainerType, bool>> memberSelector):base(memberSelector)
        {
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.Bit;
            }
        }

        protected override bool Validate(bool value)
        {
            if (_IsAllowedSet && AllowedValuesOnly != value)
            {
                Error.ValueNotAllowedError(PropertyName, value, AllowedValuesOnly);
            }

            return base.Validate(value);
        }

        public BoolTypeConfiguration<TContainerType> Out()
        {
            this.IsOut = true;
            return this;
        }

        public BoolTypeConfiguration<TContainerType> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        bool _IsAllowedSet = false;
        bool AllowedValuesOnly = false;
        public BoolTypeConfiguration<TContainerType> AllowedOnly(bool values)
        {
            _IsAllowedSet = true;
            AllowedValuesOnly = values;
            return this;
        }
    }
}
