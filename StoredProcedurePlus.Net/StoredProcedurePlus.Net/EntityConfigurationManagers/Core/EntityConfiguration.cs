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

namespace StoredProcedurePlus.Net.StoredProcedureManagers.Core
{
    public class EntityConfiguration<S>
    {
        private readonly Dictionary<string, Tuple<Type,object>> Configurations = new Dictionary<string, Tuple<Type, object>>();

        public EntityConfiguration()
        {           
        }

        public SqlParameter GetParameter(S instance, string propertyName)
        {
            Tuple<Type, object> Item = Configurations[propertyName];
            return BuildParameter(Item.Item1, Item.Item2, instance);
        }

        public SqlParameter[] GetAllParameters(S instance)
        {
            List<SqlParameter> SqlParameters = new List<SqlParameter>();
            foreach(Tuple<Type, object> Item in Configurations.Values)
            {
                SqlParameters.Add(BuildParameter(Item.Item1, Item.Item2, instance));
            }
            return SqlParameters.ToArray();
        }

        private SqlParameter BuildParameter(Type type, object configuration, S instance)
        {
            SqlParameter parameter = new SqlParameter();

            if (type == typeof(int))
            {
                IntegerTypeConfiguration<S> Configuration = configuration as IntegerTypeConfiguration<S>;
                int value = Configuration[instance];
                parameter.Value = value;
                parameter.ParameterName = string.Format("@{0}", Configuration.ParameterName);
                parameter.SqlDbType = Configuration.GetSqlDbType();                
            }
            else if (type == typeof(string))
            {
                StringTypeConfiguration<S> Configuration = configuration as StringTypeConfiguration<S>;
                string value = Configuration[instance];
                parameter.Value = value;
                parameter.ParameterName = string.Format("@{0}", Configuration.ParameterName);
                parameter.SqlDbType = Configuration.GetSqlDbType();
                if (Configuration.AllowedLength.HasValue)
                {
                    parameter.Size = Configuration.AllowedLength.Value;
                }
            }

            return parameter;
        }


        public IntegerTypeConfiguration<S> Maps(Expression<Func<S, int>> memberSelector)
        {
            IntegerTypeConfiguration<S> Configuration = new IntegerTypeConfiguration<S>(memberSelector);
            Configurations.Add(Configuration.PropertyName,new Tuple<Type, object>(typeof(int), Configuration));
            return Configuration;
        }

        public StringTypeConfiguration<S> Maps(Expression<Func<S, string>> memberSelector)
        {
            StringTypeConfiguration<S> Configuration = new StringTypeConfiguration<S>(memberSelector);
            Configurations.Add(Configuration.PropertyName, new Tuple<Type, object>(typeof(string), Configuration));
            return Configuration;
        }
    }
}