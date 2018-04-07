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

    public class ListObjectTypeConfiguration<S> : PropertyConfiguration where S : class
    {
        public ListObjectTypeConfiguration() : base(SqlDbType.Structured, true)
        {
        }

        internal NonPrimitiveEntityConfiguration ChildEntityConfiguration { get; private set; }

        MethodInfo mi = null;

        public ParameterInputEntityConfiguration<T> AsTable<T>(Expression<Func<S, List<T>>> memberSelector) where T : class
        {
            if (memberSelector.Body is MemberExpression)
            {
                MemberExpression me = (MemberExpression)memberSelector.Body;
                var prop = me.Member as PropertyInfo;
                DataType = prop.PropertyType;
                PropertyName = prop.Name;
                ParameterName = prop.Name;
                TableTypeName = typeof(T).Name;
                
                if (prop.CanRead)
                {
                    mi = prop.GetGetMethod();
                }
            }

            ParameterInputEntityConfiguration<T> pc = new ParameterInputEntityConfiguration<T>();
            ChildEntityConfiguration = pc;
            return pc;
        }

        public ParameterInputEntityConfiguration<T> AsTable<T>(Expression<Func<S, List<T>>> memberSelector, string typename) where T : class
        {
            var x = AsTable(memberSelector);
            TableTypeName = typename;
            return x;
        }

        public ParameterInputEntityConfiguration<T> AsTable<T>(Expression<Func<S, List<T>>> memberSelector, string typename, string parametername) where T : class
        {
            var x = AsTable(memberSelector);
            TableTypeName = typename;
            ParameterName = parametername;
            return x;
        }

        public ListObjectTypeConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal string TableTypeName = null;        

        public object this[object instance]
        {
            get
            {
                object Result = mi.Invoke(instance, null);
                return Result;
            }
        }
    }
}
