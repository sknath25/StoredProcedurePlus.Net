using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public class EntityConfiguration<S>
    {
        private readonly Dictionary<string, PropertyConfiguration> Configurations = new Dictionary<string, PropertyConfiguration>();

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
                parameter.Value = value;
            }
            else if (configuration.DataType == typeof(string))
            {
                StringTypeConfiguration<S> Configuration = configuration as StringTypeConfiguration<S>;
                string value = Configuration[instance];
                parameter.Value = value;
                if (Configuration.AllowedMaxLength.HasValue)
                {
                    parameter.Size = Configuration.AllowedMaxLength.Value;
                }
            }

            return parameter;
        }

        public IntegerTypeConfiguration<S> Maps(Expression<Func<S, int>> memberSelector)
        {
            IntegerTypeConfiguration<S> Configuration = new IntegerTypeConfiguration<S>(memberSelector);
            Configurations.Add(Configuration.PropertyName, Configuration);
            return Configuration;
        }

        public StringTypeConfiguration<S> Maps(Expression<Func<S, string>> memberSelector)
        {
            StringTypeConfiguration<S> Configuration = new StringTypeConfiguration<S>(memberSelector);
            Configurations.Add(Configuration.PropertyName, Configuration);
            return Configuration;
        }
    }
}