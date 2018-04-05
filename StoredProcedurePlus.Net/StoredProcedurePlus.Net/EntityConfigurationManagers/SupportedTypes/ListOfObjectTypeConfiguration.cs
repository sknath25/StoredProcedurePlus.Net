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

    public class ObjectTypeConfiguration<S> : PropertyConfiguration where S : class
    {
        public ObjectTypeConfiguration() : base(SqlDbType.Structured, true)
        {
        }


        NonPrimitiveEntityConfiguration npc = null;
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
                if (prop.CanRead)
                {
                    mi = prop.GetGetMethod();
                }
            }

            ParameterInputEntityConfiguration<T> pc = new ParameterInputEntityConfiguration<T>();
            npc = pc;
            return pc;
        }

        public ObjectTypeConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        public IDataEntityAdapter GetAsDbParameters()
        {
            return npc.GetAsDbParameters();
        }

        public object this[object instance]
        {
            get
            {
                //npc.GetNewDataAdapter(null);

                object Result = mi.Invoke(instance, null);
                return Result;
            }
        }
    }
}
