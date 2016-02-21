using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace WindowsFormsApplication
{
    class A1 : CD.IDomainModel { public A1(A2 obj) { } }
    class A2 { }
    class B1 : CD.IDomainModel { public B1(B2 obj) { } }
    class B2 { }
    class C1 : CD.IDomainModel { public C1(C2 obj) { } }
    class C2 { }
    class D1 : CD.IDomainModel { public D1(D2 obj) { } }
    class D2 { }

    class Class1
    {
        internal static ConcurrentDictionary<Type, Type> Map = new ConcurrentDictionary<Type, Type>();

        static Class1()
        {
            var map = new Dictionary<Type, Type>()
            {
                {typeof(A1), typeof(A2)},
                {typeof(B1), typeof(B2)},
                {typeof(C1), typeof(C2)},
                {typeof(D1), typeof(D2)}
            };

            Map = new ConcurrentDictionary<Type, Type>(map);
        }
    }

    class Class2 : Class1
    {
        private readonly ConcurrentDictionary<Type, object> cache
            = new ConcurrentDictionary<Type, object>();

        public virtual T Get<T>(int id) where T : CD.IDomainModel
        {
            var t2 = PegarEquavalencia(typeof(T));
            var getInternal = (Func<int, T>)cache
                .GetOrAdd(typeof(T), t => DelegateFor_GetInternal<T>(t2));
            var result = getInternal(id);
            return result;
        }

        private static Func<int, T> DelegateFor_GetInternal<T>(Type t2)
        {
            var ctor = typeof(T).GetConstructor(new[] { t2 });

            var param = Expression.Parameter(t2, "obj");

            var creatorType = typeof(Func<,>).MakeGenericType(t2, typeof(T));

            dynamic lambda = typeof(Class2)
                .GetMethod("Lambda", BindingFlags.Static | BindingFlags.NonPublic)
                .MakeGenericMethod(creatorType)
                .Invoke(null, new object[]
                {
                    Expression.New(ctor, param), new[] { param }
                });

            var creatorDelegate = (object)lambda.Compile();

            // func
            var getInternal = Activator.CreateInstance(typeof(GetInternal<,>)
                .MakeGenericType(t2, typeof(T)), creatorDelegate);

            var result = (Func<int, T>)Delegate.CreateDelegate(
                typeof(Func<int, T>),
                getInternal,
                getInternal.GetType().GetMethod("Invoke",
                    BindingFlags.Public | BindingFlags.Instance));

            return result;
        }

        private static Expression<TDelegate> Lambda<TDelegate>(
            Expression body, ParameterExpression[] parameters)
        {
            return Expression.Lambda<TDelegate>(body, parameters);
        }

        public class GetInternal<T2, T>
        {
            private readonly Func<T2, T> creator;

            public GetInternal(Func<T2, T> creator)
            {
                this.creator = creator;
            }

            public T Invoke(int id)
            {
                using (var rep = new R.Rep())
                {
                    var obj = rep.Get<T2>(id);
                    return creator(obj);
                }
            }
        }

        public static Type PegarEquavalencia(Type type)
        {
            Type tipoEquivalente;
            Map.TryGetValue(type, out tipoEquivalente);

            return tipoEquivalente;
        }
    }
}

namespace WindowsFormsApplication.R
{
    class Rep : IDisposable
    {
        public T Get<T>(int id)
        {
            return default(T);
        }

        public void Dispose()
        {
        }
    }
}

namespace CD
{
    public interface IDomainModel
    {
    }
}