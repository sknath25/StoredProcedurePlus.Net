using StoredProcedurePlus.Net.EntityConfigurationManagers;
using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes;
using StoredProcedurePlus.Net.EntityManagers;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public class EntityConfiguration<S> : NonPrimitiveEntityConfiguration where S : class
    {
        #region Private

        private class OrdinalProxy : EntityOrdinalConfiguration
        {
            internal OrdinalProxy(List<PropertyConfiguration> parameters, IDataEntityAdapter record) : base(parameters, record) { }
        }

        private class SqlParameterEntityAdapterProxy : SqlParameterEntityAdapter
        {
            internal SqlParameterEntityAdapterProxy(List<PropertyConfiguration> configurations) :base(configurations)
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
                }
            }
        }

        internal EntityConfiguration(){}

        internal IDataEntityAdapter GetAsSqlParameters()
        {
            SqlParameterEntityAdapter manager = new SqlParameterEntityAdapterProxy(Configurations);
            return manager;
        }

        internal void Prepare(IDataEntityAdapter record)
        {
            OrdinalProvider = new OrdinalProxy(Configurations, record);
        }

        EntityOrdinalConfiguration OrdinalProvider = null;
        internal void Fill(IDataEntityAdapter fromEntity, S toInstance)
        {
            if (OrdinalProvider == null) Error.PrepareDidnotCalled();

            for(int i = 0; i < Configurations.Count; i++) 
            {
                PropertyConfiguration configuration = Configurations[i];

                int Ordinal = OrdinalProvider[configuration.PropertyName];

                if (configuration.DataType == typeof(int))
                {
                    IntegerTypeConfiguration<S> Configuration = configuration as IntegerTypeConfiguration<S>;
                    Configuration[toInstance] = fromEntity.GetInt(Ordinal);
                }
                else if (configuration.DataType == typeof(string))
                {
                    StringTypeConfiguration<S> Configuration = configuration as StringTypeConfiguration<S>;
                    Configuration[toInstance] = fromEntity.GetString(Ordinal);
                }
                else if (configuration.DataType == typeof(double))
                {
                    DoubleTypeConfiguration<S> Configuration = configuration as DoubleTypeConfiguration<S>;
                    Configuration[toInstance] = fromEntity.GetDouble(Ordinal);
                }
                else if (configuration.DataType == typeof(decimal))
                {
                    DecimalTypeConfiguration<S> Configuration = configuration as DecimalTypeConfiguration<S>;
                    Configuration[toInstance] = fromEntity.GetDecimal(Ordinal);
                }
            }
        }

        internal void Fill(S fromInstance, IDataEntityAdapter toEntity)
        {
            if (OrdinalProvider == null) Error.PrepareDidnotCalled();

            for (int i = 0; i < Configurations.Count; i++)
            {
                PropertyConfiguration configuration = Configurations[i];

                int Ordinal = OrdinalProvider[configuration.PropertyName];

                if (configuration.DataType == typeof(int))
                {
                    IntegerTypeConfiguration<S> Configuration = configuration as IntegerTypeConfiguration<S>;
                    toEntity.SetInt(Ordinal, Configuration[fromInstance]);
                }
                else if (configuration.DataType == typeof(string))
                {
                    StringTypeConfiguration<S> Configuration = configuration as StringTypeConfiguration<S>;
                    toEntity.SetString(Ordinal, Configuration[fromInstance]);
                }
                else if (configuration.DataType == typeof(double))
                {
                    DoubleTypeConfiguration<S> Configuration = configuration as DoubleTypeConfiguration<S>;
                    toEntity.SetDouble(Ordinal, Configuration[fromInstance]);
                }
                else if (configuration.DataType == typeof(decimal))
                {
                    DecimalTypeConfiguration<S> Configuration = configuration as DecimalTypeConfiguration<S>;
                    toEntity.SetDecimal(Ordinal, Configuration[fromInstance]);
                }
            }
        }

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

        #endregion
    }
}