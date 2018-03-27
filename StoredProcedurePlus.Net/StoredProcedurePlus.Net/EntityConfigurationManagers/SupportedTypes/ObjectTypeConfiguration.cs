using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.EntityManagers;
using StoredProcedurePlus.Net.ErrorManagers;
using StoredProcedurePlus.Net.StoredProcedureManagers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    internal interface IObjectTypeConfiguration
    {
        IDataEntityAdapter PropertiesConfigurations { get; }
    }

    public class ObjectTypeConfiguration<S, T> : PrimitiveTypeConfiguration<S, IList<T>>, IObjectTypeConfiguration where S : class where T : class
    {
        public ObjectTypeConfiguration(Expression<Func<S, IList<T>>> memberSelector) : base(memberSelector)
        {
            Properties = new ParameterInputEntityConfiguration<T>();
        }

        internal override DbType GetDbType
        {
            get
            {
                return DbType.Object;
            }
        }

        protected override IList<T> Validate(IList<T> value)
        {
            if (IsRequired && value == null) Error.RequiredPropertyValidationError(PropertyName);

            if (value != null)
            {

            }

            //base.Validate(value);
            return value;
        }

        public ObjectTypeConfiguration<S, T> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        public ParameterInputEntityConfiguration<T> Properties {get; private set;}

        internal bool IsRequired { get; private set; }

        IDataEntityAdapter IObjectTypeConfiguration.PropertiesConfigurations => Properties.GetAsDbParameters();

        public ObjectTypeConfiguration<S,T> Required()
        {
            this.IsRequired = true;
            return this;
        }

        uint? AllowedMaxLength = null;
        public ObjectTypeConfiguration<S,T> MaxLength(uint value)
        {
            AllowedMaxLength = value;
            base.Size1 = value;
            return this;
        }

        uint? AllowedMinLength = null;
        public ObjectTypeConfiguration<S,T> MinLength(uint value)
        {
            AllowedMinLength = value;
            return this;
        }

    }
}
