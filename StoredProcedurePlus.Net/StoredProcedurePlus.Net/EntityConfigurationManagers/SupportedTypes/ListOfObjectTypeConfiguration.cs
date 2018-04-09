using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.EntityManagers;
using StoredProcedurePlus.Net.ErrorManagers;
using StoredProcedurePlus.Net.StoredProcedureManagers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{

    internal class ListObjectTypeConfiguration<S> : PropertyConfiguration where S : class
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

        internal ParameterInputEntityConfiguration<T> AsTable<T>(Expression<Func<S, List<T>>> memberSelector) where T : class
        {
            if (memberSelector.Body is MemberExpression me)
            {
                var prop = me.Member as PropertyInfo;
                DataType = prop.PropertyType;
                PropertyName = prop.Name;
                ParameterName = prop.Name;
                TableTypeName = typeof(T).Name;
                
                if (prop.CanRead)
                {
                    mi = prop.GetGetMethod();
                }
                else
                {
                    throw new ArgumentException(string.Format("{0} Property get method not found.", PropertyName));
                }
            }

            ParameterInputEntityConfiguration<T> pc = new ParameterInputEntityConfiguration<T>();
            ChildEntityConfiguration = pc;
            return pc;
        }

        internal ParameterInputEntityConfiguration<T> AsTable<T>(Expression<Func<S, List<T>>> memberSelector, string typename) where T : class
        {
            var x = AsTable(memberSelector);
            TableTypeName = typename;
            return x;
        }

        internal ParameterInputEntityConfiguration<T> AsTable<T>(Expression<Func<S, List<T>>> memberSelector, string typename, string parametername) where T : class
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
                object Result = mi.Invoke(instance, null);
                return Result;
            }
        }
    }
}
