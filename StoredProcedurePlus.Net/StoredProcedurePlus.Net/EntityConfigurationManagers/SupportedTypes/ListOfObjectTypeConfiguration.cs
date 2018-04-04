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
    internal interface IListOfObjectTypeConfiguration
    {
        IDataEntityAdapter PropertiesConfigurations { get; }
    }

    public class ListOfObjectTypeConfiguration<S, T> : PrimitiveTypeConfiguration<S, IList<T>>, IListOfObjectTypeConfiguration where S : class where T : class
    {
        public ListOfObjectTypeConfiguration(Expression<Func<S, IList<T>>> memberSelector) : base(memberSelector, true)
        {
            Properties = new ParameterInputEntityConfiguration<T>();
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

        public ListOfObjectTypeConfiguration<S, T> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        public ParameterInputEntityConfiguration<T> Properties {get; private set;}

        internal bool IsRequired { get; private set; }

        IDataEntityAdapter IListOfObjectTypeConfiguration.PropertiesConfigurations => Properties.GetAsDbParameters();

        public ListOfObjectTypeConfiguration<S,T> Required()
        {
            this.IsRequired = true;
            return this;
        }

        uint? AllowedMaxLength = null;
        public ListOfObjectTypeConfiguration<S,T> MaxLength(uint value)
        {
            AllowedMaxLength = value;
            base.Size1 = value;
            return this;
        }

        uint? AllowedMinLength = null;
        public ListOfObjectTypeConfiguration<S,T> MinLength(uint value)
        {
            AllowedMinLength = value;
            return this;
        }

    }
}
