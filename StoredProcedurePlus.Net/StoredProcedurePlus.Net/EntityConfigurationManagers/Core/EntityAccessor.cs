using System;
using System.Linq.Expressions;
using System.Reflection;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.Core
{
    internal class EntityAccessor<TContainerType> where TContainerType : class
    {
        internal static EntityAccessor<TContainerType, TPropertyType> Create<TPropertyType>(Expression<Func<TContainerType, TPropertyType>> memberSelector)
        {
            return new GetterSetter<TPropertyType>(memberSelector);
        }

        class GetterSetter<TPropertyType> : EntityAccessor<TContainerType, TPropertyType>
        {
            public GetterSetter(Expression<Func<TContainerType, TPropertyType>> memberSelector) : base(memberSelector)
            {
                
            }
        }
    }

    internal class EntityAccessor<TContainerType, TPropertyType> : EntityAccessor<TContainerType> where TContainerType : class
    {
        readonly Func<TContainerType, TPropertyType> Getter;
        readonly Action<TContainerType, TPropertyType> Setter;
        internal Type DataType { get; private set; }
        internal string PropertyName { get; private set; }
        public bool IsReadable { get; private set; }
        public bool IsWritable { get; private set; }

        internal TPropertyType this[TContainerType instance]
        {
            get
            {
                if (!IsReadable)
                    throw new ArgumentException("Property get method not found.");

                return Getter(instance);
            }
            set
            {
                if (!IsWritable)
                    throw new ArgumentException("Property set method not found.");

                Setter(instance, value);
            }
        }

        protected EntityAccessor(Expression<Func<TContainerType, TPropertyType>> memberSelector) //SUMAN: This shouldn't be accessed by outsider.
        {
            if (memberSelector.Body is MemberExpression me)
            {
                var prop = me.Member as PropertyInfo;

                DataType = prop.PropertyType;
                PropertyName = prop.Name;
                IsReadable = prop.CanRead;
                IsWritable = prop.CanWrite;
                AssignGetDelegate(IsReadable, ref Getter, prop.GetGetMethod());
                AssignSetDelegate(IsWritable, ref Setter, prop.GetSetMethod());
            }
        }

        static void AssignGetDelegate(bool assignable, ref Func<TContainerType, TPropertyType> assignee, MethodInfo assignor)
        {
            if (assignable)
            {
                var target = Expression.Parameter(typeof(TContainerType));
                var body = Expression.Call(target, assignor);

                assignee = Expression.Lambda<Func<TContainerType, TPropertyType>>(body, target)
                    .Compile();
            }
        }

        static void AssignSetDelegate(bool assignable, ref Action<TContainerType, TPropertyType> assignee, MethodInfo assignor)
        {
            if (assignable)
            {               
                var target = Expression.Parameter(typeof(TContainerType));
                var value = Expression.Parameter(typeof(TPropertyType));
                var body = Expression.Call(target, assignor,
                    Expression.Convert(value, typeof(TPropertyType)));

                assignee = Expression.Lambda<Action<TContainerType, TPropertyType>>(body, target, value)
                    .Compile();
            }
        }
    }
}
