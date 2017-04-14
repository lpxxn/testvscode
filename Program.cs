using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;

namespace testvscode
{
    public class EnumClass
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string LanDesc { get; set; }
    }
    public enum TestEnum
    {
        ABCDE = 1,
        GJFID = 2
    }
    public class Program
    {
        public static Tuple<string, List<EnumClass>> GetObjFormEnumS<T, TValue>(Expression<Func<T, TValue>> a,string languageCode = "zh-cn")
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T muset Enum Type");

            var enumName = typeof(T).Name;
            var names = Enum.GetNames(typeof(T));
            LambdaExpression le = a as LambdaExpression;
            MemberExpression me = le.Body as MemberExpression;
            string rv = "";
            if (me != null) 
            {
                rv = me.Member.Name;
            }
            Console.WriteLine($"property name {rv}");
            foreach(var name in names) 
            {
                
            }
            var values = Enum.GetValues(typeof(T)).Cast<T>();
            var datas = values.Select(value =>
            {
                var intValue = Convert.ToInt32(value);
                var valueName = Enum.GetName(typeof(T), value);
                return new EnumClass() { Name = valueName, Value = intValue };
            }).ToList();

            return new Tuple<string, List<EnumClass>>(enumName, datas);
        }
        public static void Main(string[] args)
        {
            GetObjFormEnumS<TestEnum, TestEnum>(x=>TestEnum.ABCDE);
            Console.WriteLine("Hello World!");
        }
    }
}
