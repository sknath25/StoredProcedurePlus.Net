using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public class EntityConfiguration<S> where S : class
    {
        private readonly Dictionary<string, PropertyConfiguration> Configurations = new Dictionary<string, PropertyConfiguration>();

        internal EntityConfiguration()
        {
            IncludeUnmappedProperties = true;
        }

        internal void Initialize()
        {
            if (!IncludeUnmappedProperties) return;

            Type SourceType = typeof(S);

            PropertyInfo[] Properties = SourceType.GetProperties();

            for (int i = 0; i < Properties.Length; i++)
            {
                if (!Configurations.ContainsKey(Properties[i].Name))
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

        private LambdaExpression BuildExpression(Type sourceType, PropertyInfo propertyInfo)
        {
            var parameter = Expression.Parameter(sourceType, propertyInfo.Name);
            var property = Expression.Property(parameter, propertyInfo);
            var funcType = typeof(Func<,>).MakeGenericType(sourceType, propertyInfo.PropertyType);
            var lambda = Expression.Lambda(funcType, property, parameter);
            return lambda;
        }

        private void AddMapping(PropertyConfiguration Configuration)
        {
            if (Configurations.ContainsKey(Configuration.PropertyName))
            {
                Configurations.Remove(Configuration.PropertyName);
            }
            Configurations.Add(Configuration.PropertyName, Configuration);
        }

        const string SQLPARAMETERPREFIX = "@";
        private SqlParameter BuildParameter(PropertyConfiguration configuration, S instance)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = string.Concat(SQLPARAMETERPREFIX, configuration.ParameterName);
            parameter.SqlDbType = configuration.GetSqlDbType();
            parameter.Direction = configuration.IsOut ? System.Data.ParameterDirection.Output : System.Data.ParameterDirection.Input;

            if (configuration.DataType == typeof(int))
            {
                IntegerTypeConfiguration<S> Configuration = configuration as IntegerTypeConfiguration<S>;
                int value = Configuration[instance];
                parameter.SqlValue = value;
            }
            else if (configuration.DataType == typeof(string))
            {
                StringTypeConfiguration<S> Configuration = configuration as StringTypeConfiguration<S>;
                string value = Configuration[instance];
                parameter.SqlValue = (object)value ?? DBNull.Value;
                if (Configuration.Size.HasValue)
                {
                    parameter.Size = (int)Configuration.Size.Value;
                }
            }
            else if (configuration.DataType == typeof(decimal))
            {
                DecimalTypeConfiguration<S> Configuration = configuration as DecimalTypeConfiguration<S>;
                decimal value = Configuration[instance];
                parameter.SqlValue = value;
                if (Configuration.ScaleSize.HasValue)
                {
                    parameter.Scale = Configuration.ScaleSize.Value;
                }
                if (Configuration.PrecisionSize.HasValue)
                {
                    parameter.Precision = Configuration.PrecisionSize.Value;
                }
            }
            else if (configuration.DataType == typeof(double))
            {
                DoubleTypeConfiguration<S> Configuration = configuration as DoubleTypeConfiguration<S>;
                double value = Configuration[instance];
                parameter.SqlValue = value;
                if (Configuration.ScaleSize.HasValue)
                {
                    parameter.Scale = Configuration.ScaleSize.Value;
                }
                if (Configuration.PrecisionSize.HasValue)
                {
                    parameter.Precision = Configuration.PrecisionSize.Value;
                }
            }
            return parameter;
        }

        internal SqlParameter GetParameter(S instance, string propertyName)
        {
            PropertyConfiguration Item = Configurations[propertyName];
            return BuildParameter(Item, instance);
        }

        internal SqlParameter[] GetAllParameters(S instance)
        {
            List<SqlParameter> SqlParameters = new List<SqlParameter>();
            foreach (PropertyConfiguration Item in Configurations.Values)
            {
                SqlParameters.Add(BuildParameter(Item, instance));
            }
            return SqlParameters.ToArray();
        }

        internal void SetOuts(SqlParameter[] parameters, S instance)
        {
            SqlParameter FoundParameter;

            foreach (PropertyConfiguration configuration in Configurations.Values)
            {
                FoundParameter = null;

                if (!configuration.IsOut) continue;

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].ParameterName == string.Concat(SQLPARAMETERPREFIX, configuration.ParameterName))
                    {
                        FoundParameter = parameters[i];
                        break;
                    }
                }

                if (FoundParameter == null) continue;

                if (configuration.DataType == typeof(int))
                {
                    IntegerTypeConfiguration<S> Configuration = configuration as IntegerTypeConfiguration<S>;
                    Configuration[instance] = int.Parse(FoundParameter.SqlValue.ToString());
                }
                else if (configuration.DataType == typeof(string))
                {
                    StringTypeConfiguration<S> Configuration = configuration as StringTypeConfiguration<S>;
                    Configuration[instance] = FoundParameter.SqlValue.ToString();
                }
            }
        }

        public bool IncludeUnmappedProperties { get; set; }

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
    }
}