using StoredProcedurePlus.Net.EntityConfigurationManagers;
using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes;
using StoredProcedurePlus.Net.EntityManagers;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public class EntityConfiguration<S> : NonPrimitiveEntityConfiguration where S : class, new()
    {
        #region Private

        private class OrdinalProxy : EntityOrdinalConfiguration
        {
            internal OrdinalProxy(List<PropertyConfiguration> parameters, IDataEntityAdapter record) : base(parameters, record) { }
        }

        private class DbParameterEntityAdapterProxy : DbParameterEntityAdapter
        {
            internal DbParameterEntityAdapterProxy(List<PropertyConfiguration> configurations) :base(configurations)
            {

            }
        }

        private class DbDataEntityAdapterProxy : DbDataEntityAdapter
        {
            internal DbDataEntityAdapterProxy(IDataReader record) : base(record)
            {

            }
        }

        private readonly List<PropertyConfiguration> Configurations = new List<PropertyConfiguration>();

        private LambdaExpression BuildExpression(Type sourceType, PropertyInfo propertyInfo)
        {
            var parameter = Expression.Parameter(sourceType, propertyInfo.Name);
            var property = Expression.Property(parameter, propertyInfo);
            var funcType = typeof(Func<,>).MakeGenericType(sourceType, propertyInfo.PropertyType);
            var lambda = Expression.Lambda(funcType, property, parameter);
            return lambda;
        }

        private void AddMapping(PropertyConfiguration configuration)
        {
            Configurations.Remove(configuration);//IT will only check agaist property name. 
            Configurations.Add(configuration);
        }

        #endregion

        protected override void InitializePropertyConfigurations()
        {
            SourceType = typeof(S);

            PropertyInfo[] Properties = SourceType.GetProperties();

            for (int i = 0; i < Properties.Length; i++)
            {
                if (!Configurations.Exists(v=>v.PropertyName == Properties[i].Name))
                {
                    if (Properties[i].PropertyType == typeof(string))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, string>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(int))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, int>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(decimal))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, decimal>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(double))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, double>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(DateTime))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, double>>)l);
                    }
                }
            }
        }

        #region Internal

        internal override object CreateNewDefaultInstance()
        {
            return new S();
        }

        internal IDataEntityAdapter GetAsDbParameters()
        {
            return new DbParameterEntityAdapterProxy(Configurations);
        }

        internal EntityConfiguration(){}

        override internal void Prepare(IDataEntityAdapter record)
        {
            OrdinalProvider = new OrdinalProxy(Configurations, record);
        }

        override internal DbDataEntityAdapter GetNewDataAdapter(IDataReader record)
        {
            return new DbDataEntityAdapterProxy(record);
        }

        EntityOrdinalConfiguration OrdinalProvider = null;
        override internal void Set(IDataEntityAdapter fromEntity, object toInstance)
        {
            if (OrdinalProvider == null) Error.PrepareDidnotCalled();

            S Instance = (S)toInstance;

            for(int i = 0; i < Configurations.Count; i++) 
            {
                PropertyConfiguration configuration = Configurations[i];

                int Ordinal = OrdinalProvider[configuration.PropertyName];

                if (configuration.DataType == typeof(int))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableIntProperty(configuration.PropertyName);
                    }
                    else
                    {
                        IntegerTypeConfiguration<S> Configuration = configuration as IntegerTypeConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetInt(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(string))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        StringTypeConfiguration<S> Configuration = configuration as StringTypeConfiguration<S>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        StringTypeConfiguration<S> Configuration = configuration as StringTypeConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetString(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(double))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableDoubleProperty(configuration.PropertyName);
                    }
                    else
                    {
                        DoubleTypeConfiguration<S> Configuration = configuration as DoubleTypeConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetDouble(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(decimal))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableDecimalProperty(configuration.PropertyName);
                    }
                    else
                    {
                        DecimalTypeConfiguration<S> Configuration = configuration as DecimalTypeConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetDecimal(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(DateTime))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableDateTimeProperty(configuration.PropertyName);
                    }
                    else
                    {
                        DateTimeTypeConfiguration<S> Configuration = configuration as DateTimeTypeConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetDate(Ordinal);
                    }
                }
            }
        }

        override internal void Get(object fromInstance, IDataEntityAdapter toEntity)
        {
            if (OrdinalProvider == null) Error.PrepareDidnotCalled();

            S Instance = (S)fromInstance;

            for (int i = 0; i < Configurations.Count; i++)
            {
                PropertyConfiguration configuration = Configurations[i];

                int Ordinal = OrdinalProvider[configuration.PropertyName];

                if (configuration.DataType == typeof(int))
                {
                    IntegerTypeConfiguration<S> Configuration = configuration as IntegerTypeConfiguration<S>;
                    toEntity.SetInt(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(string))
                {
                    StringTypeConfiguration<S> Configuration = configuration as StringTypeConfiguration<S>;
                    toEntity.SetString(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(double))
                {
                    DoubleTypeConfiguration<S> Configuration = configuration as DoubleTypeConfiguration<S>;
                    toEntity.SetDouble(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(decimal))
                {
                    DecimalTypeConfiguration<S> Configuration = configuration as DecimalTypeConfiguration<S>;
                    toEntity.SetDecimal(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(DateTime))
                {
                    DateTimeTypeConfiguration<S> Configuration = configuration as DateTimeTypeConfiguration<S>;
                    toEntity.SetDateTime(Ordinal, Configuration[Instance]);
                }
            }
        }

        #endregion

        #region Public

        public IntegerTypeConfiguration<S> Maps(Expression<Func<S, int>> memberSelector)
        {
            IntegerTypeConfiguration<S> Configuration = new IntegerTypeConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }

        public StringTypeConfiguration<S> Maps(Expression<Func<S, string>> memberSelector)
        {
            StringTypeConfiguration<S> Configuration = new StringTypeConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }

        public DecimalTypeConfiguration<S> Maps(Expression<Func<S, decimal>> memberSelector)
        {
            DecimalTypeConfiguration<S> Configuration = new DecimalTypeConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }

        public DoubleTypeConfiguration<S> Maps(Expression<Func<S, double>> memberSelector)
        {
            DoubleTypeConfiguration<S> Configuration = new DoubleTypeConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }

        public DateTimeTypeConfiguration<S> Maps(Expression<Func<S, DateTime>> memberSelector)
        {
            DateTimeTypeConfiguration<S> Configuration = new DateTimeTypeConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }

        
        #endregion
    }
}