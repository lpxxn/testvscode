using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;

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

    public enum TestEnum2 : long
    {
        ABCDE = 1,
        GJFID = 2
    }
    public class Program
    {
        static string GetIP4Address()
        {
            string IP4Address = String.Empty;

            
            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }
            return IP4Address;
        }
        public static Tuple<string, List<EnumClass>> GetObjFormEnumS<T>(List<Expression<Func<T, T>>> avoidValues = null, string languageCode = "zh-cn")
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T muset Enum Type");


            var enumName = typeof(T).Name;
            var names = Enum.GetNames(typeof(T));
            var avoidNames = new List<string>();

            avoidValues?.ForEach(x =>
            {
                LambdaExpression le = x as LambdaExpression;
                MemberExpression me = le.Body as MemberExpression;
                var eleName = "";
                if (me != null)
                {
                    eleName = me.Member.Name;
                }
                else if (le.Body is ConstantExpression)
                {
                    eleName = ((ConstantExpression)le.Body).Value.ToString();
                }
                avoidNames.Add(eleName);

            });
           
            var values = Enum.GetNames(typeof(T)).Where(x=> avoidValues == null || !avoidNames.Contains(x));

           
            var datas = values.Select(valueName =>
            {
                var intValue = (int)Enum.Parse(typeof(T), valueName);
                return new EnumClass() { Name = valueName, Value = intValue };
            }).ToList();

            return new Tuple<string, List<EnumClass>>(enumName, datas);
        }
        public static void Main(string[] args)
        {
            var enumType = typeof(TestEnum);
            var enumType2 = typeof(TestEnum2);
            var enumValues = Enum.GetValues(typeof(TestEnum2));
            var eleType = enumValues.GetValue(0) is long;
            var underLyingType1 = Enum.GetUnderlyingType(typeof(TestEnum));
            var underLineType =  Enum.GetUnderlyingType(typeof(TestEnum2));
            
            var localHost = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
            var ipv4 = GetIP4Address();
            //            var testResult = GetObjFormEnumS<TestEnum>(new List<Expression<Func<TestEnum, TestEnum>>>{ x => TestEnum.ABCDE });
            //            var testResult2 = GetObjFormEnumS<TestEnum>();
            string[] companies = { "Consolidated Messenger", "Alpine Ski House", "Southridge Video", "City Power & Light",
"Coho Winery", "Wide World Importers", "Graphic Design Institute", "Adventure Works",
"Humongous Insurance", "Woodgrove Bank", "Margie's Travel", "Northwind Traders",
"Blue Yonder Airlines", "Trey Research", "The Phone Company",
"Wingtip Toys", "Lucerne Publishing", "Fourth Coffee" };

            IQueryable<string> queryableData = companies.AsQueryable();

            ParameterExpression pe = Expression.Parameter(typeof(string), "company");

            // where(company => (company.ToLower() == "coho winery" || company.Length > 16))
            // create an expression tree represents the expression 'company.ToLower() == "coho winery"'.
            Expression left = Expression.Call(pe, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
            Console.WriteLine("Hello World!");


            ConstantExpression constExp = Expression.Constant("abc", typeof(string));

            MethodCallExpression _methodExp =
                Expression.Call(typeof(Console).GetMethod("WriteLine", new Type[] {typeof(string)}),
                    new Expression[] {constExp});
            
            Expression<Action> lambdaExp = Expression.Lambda<Action>(_methodExp);

            var arg1 = Expression.Parameter(typeof(int), "a");
            var arg2 = Expression.Parameter(typeof(int), "b");
            LambdaExpression normalExp =
                Expression.Lambda(Expression.Add(arg1, arg2), arg1
                    , arg2);

            Delegate compDel = normalExp.Compile();
            
            var comp = normalExp.Compile().DynamicInvoke(1, 2);
           

            Expression<Func<int, int, int>> normalExp2 = Expression.Lambda<Func<int, int, int>>(Expression.Add(arg1, arg2), arg1, arg2);
            
            var compDel2 = normalExp2.Compile();
            var result2 = compDel2(1, 2);
            var comp2 = normalExp2.Compile().DynamicInvoke(5, 2);

            Expression<Func<int, int, int>> normalExp3 = (x, y) => x + y;

            Func<int, int, int> compDel3 = normalExp3.Compile();
            var result3 = compDel3(1, 2);

            var comp3 = compDel3.DynamicInvoke(4, 4);


            LambdaExpression normalExp4 = Expression.Lambda(
                Expression.Block(Expression.Call(typeof(Console).GetMethod("WriteLine", new Type[] {typeof(string)}),
                    Expression.Constant("11111"))));

            Delegate compDel4 = normalExp4.Compile();
            var comp4 = compDel4.DynamicInvoke();


            Expression<Action> normal5 = () => Console.WriteLine("aaaa");

            Action action5 = normal5.Compile();
            action5();

            var testCode = ReadClassTemplate();
            File.WriteAllText(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\test.cs", testCode);

            MyColors enumColor = MyColors.Blue | MyColors.Yellow| MyColors.Green | MyColors.ligth;
            var intEnumColor = (int) enumColor;
            var retrieveEnum = (MyColors) intEnumColor;
            Console.WriteLine(retrieveEnum);
            Console.WriteLine(enumColor);
            if ((enumColor & MyColors.Yellow) == MyColors.Yellow)
            {
                Console.WriteLine($"have element {MyColors.Yellow}");
                // Yellow has been set...
            }
            enumColor &= ~MyColors.Yellow;
            if (enumColor.HasFlag(MyColors.Yellow))
            {
                Console.WriteLine($"have element {MyColors.Yellow}");
            }
            enumColor &= ~MyColors.ligth;
            if (enumColor.HasFlag(MyColors.ligth))
            {
                Console.WriteLine($"have element {MyColors.ligth}");
            }
        }

        [Flags]
        public enum MyColors
        {
            ligth = 0,
            Yellow = 1,
            Green = 2,
            Red = 4,
            Blue = 8
        }
        private static string ReadClassTemplate()
        {
            var ClassCode = "@@Usings@@\n";
            ClassCode += "namespace @@Namespace@@\n";
            ClassCode += "{\n\t";
            ClassCode += "public class @@ClassName@@\n\t";
            ClassCode += "{\n\t\t";
            ClassCode += "@@Fields@@\n\n\t\t";
            ClassCode += "public @@className@@()\n\t\t";
            ClassCode += "{\n\t\t";
            ClassCode += "\n\t\t";
            ClassCode += "}\n\t";

            ClassCode += "}\n";
            ClassCode += "}";

            return ClassCode;
        }

    }


   public class Program1
    {

        static void Main(string[] args2)
        {
            Console.WriteLine("This is the second main method");
            Console.ReadLine();
        }
    }
}
