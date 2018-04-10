using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.StoredProcedureManagers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{

    internal class ListObjectTypeConfiguration<TContainerType> : PropertyConfiguration where TContainerType : class
    {
        MethodInfo mi = null;

        internal NonPrimitiveEntityConfiguration ChildEntityConfiguration { get; private set; }

        internal string TableTypeName = null;

        internal ListObjectTypeConfiguration() : base(true)
        {
            ChildEntityConfiguration = null;
        }

        internal override SqlDbType GetDbType
        {
            get
            {
                return SqlDbType.Structured;
            }
        }

        internal TvpParameterInputEntityConfiguration<TPropertyType> AsTable<TPropertyType>(Expression<Func<TContainerType, List<TPropertyType>>> memberSelector) where TPropertyType : class
        {
            if (memberSelector.Body is MemberExpression me)
            {
                var prop = me.Member as PropertyInfo;
                DataType = prop.PropertyType;
                PropertyName = prop.Name;
                ParameterName = prop.Name;
                TableTypeName = typeof(TPropertyType).Name;
                
                if (prop.CanRead)
                {
                    mi = prop.GetGetMethod();
                }
                else
                {
                    throw new ArgumentException(string.Format("{0} Property get method not found.", PropertyName));
                }
            }

            TvpParameterInputEntityConfiguration<TPropertyType> pc = new TvpParameterInputEntityConfiguration<TPropertyType>();
            ChildEntityConfiguration = pc;
            return pc;
        }

        internal TvpParameterInputEntityConfiguration<TPropertyType> AsTable<TPropertyType>(Expression<Func<TContainerType, List<TPropertyType>>> memberSelector, string typename) where TPropertyType : class
        {
            var x = AsTable(memberSelector);
            TableTypeName = typename;
            return x;
        }

        internal TvpParameterInputEntityConfiguration<TPropertyType> AsTable<TPropertyType>(Expression<Func<TContainerType, List<TPropertyType>>> memberSelector, string typename, string parametername) where TPropertyType : class
        {
            var x = AsTable(memberSelector);
            TableTypeName = typename;
            ParameterName = parametername;
            return x;
        }

        internal object this[object instance]
        {
            get
            {
                return mi.Invoke(instance, null);
            }
        }
    }
}
