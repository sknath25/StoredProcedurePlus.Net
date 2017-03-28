using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.Core
{
    public class EntityAccessor<S>
    {       
        public static EntityAccessor<S, T> Create<T>(Expression<Func<S, T>> memberSelector)
        {
            return new GetterSetter<T>(memberSelector);
        }

        class GetterSetter<T> : EntityAccessor<S, T>
        {
            public GetterSetter(Expression<Func<S, T>> memberSelector) : base(memberSelector)
            {
                
            }
        }
    }

    public class EntityAccessor<S, T> : EntityAccessor<S>
    {
        public string PropertyName { get; private set; }

        readonly Func<S, T> Getter;

        readonly Action<S, T> Setter;

        public bool IsReadable { get; private set; }
        public bool IsWritable { get; private set; }
        public T this[S instance]
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
        protected EntityAccessor(Expression<Func<S, T>> memberSelector) //SUMAN: This shouldn't be accessed by outsider.
        {
            if (memberSelector.Body is MemberExpression)
            {
                MemberExpression me = (MemberExpression)memberSelector.Body;

                var prop = me.Member as PropertyInfo;

                PropertyName = prop.Name;
                IsReadable = prop.CanRead;
                IsWritable = prop.CanWrite;
                AssignGetDelegate(IsReadable, ref Getter, prop.GetGetMethod());
                AssignSetDelegate(IsWritable, ref Setter, prop.GetSetMethod());
            }
        }
        void AssignGetDelegate(bool assignable, ref Func<S, T> assignee, MethodInfo assignor)
        {
            if (assignable)
            {
                var target = Expression.Parameter(typeof(S));
                var body = Expression.Call(target, assignor);

                assignee = Expression.Lambda<Func<S, T>>(body, target)
                    .Compile();
            }
        }
        void AssignSetDelegate(bool assignable, ref Action<S, T> assignee, MethodInfo assignor)
        {
            if (assignable)
            {
                var target = Expression.Parameter(typeof(S));
                var value = Expression.Parameter(typeof(T));
                var body = Expression.Call(target, assignor,
                    Expression.Convert(value, typeof(T)));

                assignee = Expression.Lambda<Action<S, T>>(body, target, value)
                    .Compile();
            }
        }
    }
}
