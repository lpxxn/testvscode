using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = typeof(Foo).GetConstructor(new Type[] { }).Invoke(new object[] { });

            typeof(Foo).GetField("bar", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(obj, 567);
            Console.WriteLine(((Foo)obj).GetBar());


            SimpleMv<Foo> testList = new SimpleMv<Foo>();
            testList.Entity.TestList.Add(1);
        }
    }

    public class Foo
    {
        private int bar;

        public Foo()
        {

        }

        public List<int> TestList { get; set; }

        public List<string> TestList2 { get; set; } = new List<string>();
        public List<string> TestList3 { get; set; }
        public int GetBar()
        {
            return bar;
        }
    }

    public class SimpleMv<T> where T : class
    {
        public T Entity { get; set; }

        public SimpleMv()
        {
            Entity = typeof(T).GetConstructor(Type.EmptyTypes).Invoke(new object[] { }) as T;
            var list = Entity.GetType().GetProperties().Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(List<>));
            foreach (var info in list)
            {
                var value = info.GetValue(Entity);
                if (!Object.ReferenceEquals(value, null)) continue;

                var listType = typeof(List<>);
                var constructedListType = listType.MakeGenericType(info.PropertyType.GenericTypeArguments);
                var instance = Activator.CreateInstance(constructedListType);
                info.SetValue(Entity, instance);
            }
        }
    }
}
