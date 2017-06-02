using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionDemo1
{
    class Program
    {
        //Func<IEnumerable<int>, int, bool> dividesectionmethod = (x, y) =>
        //{
        //    int nos1 = 0;
        //    int nos2 = 0;
        //    foreach (int i in x)
        //    {
        //        if (i <= y)
        //            nos1++;
        //        else
        //            nos2++;
        //    }
        //    return nos1 > nos2;
        //};


        static void Main(string[] args)
        {

            ReInstateField();
            ReinstatementExpression();
            ComposeFuncLambda();

            ComposeFuncLambda2();

            ComposeFuncLambda3();

            Console.ReadLine();
        }

        private static void ReInstateField()
        {
            NewExpression newAnimal = Expression.New(typeof(Animal));
            
            MemberInfo species = typeof(Animal).GetMember("Species")[0];
            MemberInfo age = typeof(Animal).GetMember("Age")[0];

            MemberBinding speciesBinding = Expression.Bind(species, Expression.Constant("aabc"));
            MemberBinding ageBinding = Expression.Bind(age, Expression.Constant(123));

            MemberInitExpression initExpression = Expression.MemberInit(newAnimal, speciesBinding, ageBinding);

            ParameterExpression animalObj = Expression.Variable(typeof(Animal));
            BinaryExpression setAnimal = Expression.Assign(animalObj, initExpression);


            MemberExpression nameExp = Expression.Field(animalObj, "Name");
            BinaryExpression binaryFile = Expression.Assign(nameExp, Expression.Constant("abc"));

            BlockExpression blockExpression1 = Expression.Block(new[] { animalObj }, setAnimal, binaryFile, animalObj);
            var strExp = initExpression.ToString();

            ParameterExpression xP = Expression.Parameter(typeof(Animal), "x");
            var exp1 = Expression.Lambda<Func<Animal>>(blockExpression1);
            var reval = exp1.Compile()();
        }

        private static void ReinstatementExpression()
        {
            
            NewExpression newAnimal = Expression.New(typeof(Animal));

            MemberExpression nameExp = Expression.Field(newAnimal, "Name");

            MemberInfo species = typeof(Animal).GetMember("Species")[0];
            MemberInfo age = typeof(Animal).GetMember("Age")[0];


            MemberBinding speciesBinding = Expression.Bind(species, Expression.Constant("aaa"));
            MemberBinding ageBinding = Expression.Bind(age, Expression.Constant(10));




            MemberInitExpression initExpression = Expression.MemberInit(newAnimal, speciesBinding, ageBinding);

            Console.WriteLine(initExpression.ToString());

            var exp = Expression.Lambda<Func<Animal>>(initExpression);
            var a = exp.Compile()();

            ParameterExpression xParam = Expression.Parameter(typeof(Animal), "x");
            var exp2 = Expression.Lambda<Func<Animal, Animal>>(initExpression, xParam);
            var nt2 = exp2.NodeType;
            var nt22 = exp2.Body.NodeType;

            Console.WriteLine(exp2.ToString());


            BlockExpression blockExp = Expression.Block(initExpression);

            var exp3 = Expression.Lambda<Func<Animal, Animal>>(blockExp, xParam);
            Console.WriteLine(exp3);
            var nt3 = exp3.NodeType;
            var nt33 = exp3.Body.NodeType;
            var n3 = exp3.Compile()(null);

            var nodeType = exp.Body.NodeType;
            Console.WriteLine(nodeType);


            var aimal = new Animal();
            var n1 = aimal.GetName(x => x.Age);

            object obj = new object();
            
            var path = GetPropertyName<Animal, int>(x => aimal.Age);

            var path2 = GetPropertyName<Animal, string>(x => aimal.Info.Desc);

        }

        private static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> path)
        {
            Expression body = path.Body;
            var type = body.NodeType;
            MemberExpression memberExpression = body as MemberExpression;

            if (memberExpression != null)
            {
                string value = memberExpression.Member.Name;
                return value;
            }


            return "";
        }

        private static void ComposeFuncLambda3()
        {
            ParameterExpression enumerableParam = Expression.Parameter(typeof(IEnumerable<int>), "enumerable");
            ParameterExpression intParameter = Expression.Parameter(typeof(int), "x");

            ParameterExpression nos1 = Expression.Variable(typeof(int), "nos1");
            ParameterExpression nos2 = Expression.Variable(typeof(int), "nos2");
            BinaryExpression assignNos1 = Expression.Assign(nos1, Expression.Constant(0));
            BinaryExpression assignNos2 = Expression.Assign(nos2, Expression.Constant(0));

            ParameterExpression enumerator = Expression.Variable(typeof(IEnumerator<int>), "enumerator");
            BinaryExpression assignEnumerator = Expression.Assign(enumerator,
                Expression.Call(enumerableParam, typeof(IEnumerable<int>).GetMethod("GetEnumerator")));

            MethodCallExpression moveNext = Expression.Call(enumerator, typeof(IEnumerator).GetMethod("MoveNext"));



            ParameterExpression currentParamter = Expression.Variable(typeof(int), "current");

            BinaryExpression assignCurrent = Expression.Assign(currentParamter,
                Expression.Property(enumerator, "Current"));

            LabelTarget target = Expression.Label("reval");

            BlockExpression block =
                Expression.Block(new ParameterExpression[] { nos1, nos2, enumerator, currentParamter },
                    assignNos1,
                    assignNos2,
                    assignEnumerator,
                    Expression.Loop(Expression.Block(
                        Expression.IfThenElse(Expression.Equal(moveNext, Expression.Constant(true)),
                            Expression.Block(assignCurrent, Expression.IfThenElse(Expression.LessThan(currentParamter, intParameter),
                                Expression.Assign(nos1, Expression.Increment(nos1)),
                                Expression.Assign(nos2, Expression.Increment(nos2))))
                            , Expression.Break(target))
                        ), target),
                    Expression.GreaterThan(nos1, nos2)
                );

            Expression<Func<IEnumerable<int>, int, bool>> expression =
                Expression.Lambda<Func<IEnumerable<int>, int, bool>>(block, enumerableParam, intParameter);
            var func = expression.Compile();
            var r = func(new[] { 1, 2, 3, 4 }, 10);
        }

        private static void ComposeFuncLambda2()
        {
            #region 

            ParameterExpression enumerableExpression = Expression.Parameter(typeof(IEnumerable<int>), "x");
            ParameterExpression intexpression = Expression.Parameter(typeof(int), "y");

            ParameterExpression localVarNos1 = Expression.Variable(typeof(int), "nose1");
            ParameterExpression localVarNos2 = Expression.Variable(typeof(int), "nos2");
            ConstantExpression zeroConstantExpression = Expression.Constant(0);
            BinaryExpression bexplocalnos1 = Expression.Assign(localVarNos1, zeroConstantExpression);
            BinaryExpression bexplocalnos2 = Expression.Assign(localVarNos2, zeroConstantExpression);

            // As Expression does not support Foreach we need to get Enumerator before doing loop
            ParameterExpression enumerator = Expression.Variable(typeof(IEnumerator<int>), "enumerator");
            BinaryExpression assignEnumerator = Expression.Assign(enumerator,
                Expression.Call(enumerableExpression, typeof(IEnumerable<int>).GetMethod("GetEnumerator")));

            var currentElement = Expression.Parameter(typeof(int), "i");
            var callCurrent = Expression.Assign(currentElement, Expression.Property(enumerator, "Current"));

            BinaryExpression firstLessEqualSecond = Expression.LessThanOrEqual(currentElement, intexpression);

            MethodCallExpression moveNext = Expression.Call(enumerator, typeof(IEnumerator).GetMethod("MoveNext"));

            LabelTarget looTarget2 = Expression.Label(typeof(bool));
            ParameterExpression resValParam = Expression.Parameter(typeof(bool), "resVal");

            #endregion


            BlockExpression block = Expression.Block(
                new ParameterExpression[] { localVarNos1, localVarNos2, enumerator, currentElement, resValParam },
                bexplocalnos1,
                bexplocalnos2,
                assignEnumerator,
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.NotEqual(moveNext, Expression.Constant(false)), // if
                        Expression.Block(
                            callCurrent,
                            Expression.IfThenElse(
                                firstLessEqualSecond,
                                Expression.Assign(
                                    localVarNos1,
                                    Expression.Increment(localVarNos1)),
                                Expression.Assign(
                                    localVarNos2,
                                    Expression.Increment(localVarNos2)))),
                        // else TODO 一样的
                        Expression.Block(
                            Expression.Assign(resValParam, Expression.LessThan(localVarNos1, localVarNos2)),
                            Expression.Break(looTarget2, resValParam))),

                    looTarget2));

            //// else　TODO 一样的
            //Expression.Block(
            //    Expression.Assign(resValParam, Expression.LessThan(localVarNos1, localVarNos2)),
            //    Expression.Break(looplabel))),

            //        looplabel), resValParam);

            Expression<Func<IEnumerable<int>, int, bool>> lambda =
                Expression.Lambda<Func<IEnumerable<int>, int, bool>>(block, enumerableExpression,
                    intexpression);

            Func<IEnumerable<int>, int, bool> dividesctionMethod = lambda.Compile();


            var resVal = dividesctionMethod(new[] { 1, 2, 3, 4, 5, 6 }, 1);
            Console.WriteLine(resVal);
        }

        private static void ComposeFuncLambda()
        {
            ParameterExpression enumerableExpression = Expression.Parameter(typeof(IEnumerable<int>), "x");
            ParameterExpression intexpression = Expression.Parameter(typeof(int), "y");

            ParameterExpression localVarNos1 = Expression.Variable(typeof(int), "nose1");
            ParameterExpression localVarNos2 = Expression.Variable(typeof(int), "nos2");
            ConstantExpression zeroConstantExpression = Expression.Constant(0);
            BinaryExpression bexplocalnos1 = Expression.Assign(localVarNos1, zeroConstantExpression);
            BinaryExpression bexplocalnos2 = Expression.Assign(localVarNos2, zeroConstantExpression);

            // As Expression does not support Foreach we need to get Enumerator before doing loop
            ParameterExpression enumerator = Expression.Variable(typeof(IEnumerator<int>), "enumerator");
            BinaryExpression assignEnumerator = Expression.Assign(enumerator,
                Expression.Call(enumerableExpression, typeof(IEnumerable<int>).GetMethod("GetEnumerator")));

            var currentElement = Expression.Parameter(typeof(int), "i");
            var callCurrent = Expression.Assign(currentElement, Expression.Property(enumerator, "Current"));

            BinaryExpression firstLessEqualSecond = Expression.LessThanOrEqual(currentElement, intexpression);

            MethodCallExpression moveNext = Expression.Call(enumerator, typeof(IEnumerator).GetMethod("MoveNext"));

            LabelTarget looplabel = Expression.Label("looplabel1");



            BlockExpression block = Expression.Block(
                new ParameterExpression[] { localVarNos1, localVarNos2, enumerator, currentElement },
                bexplocalnos1,
                bexplocalnos2,
                assignEnumerator,
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.NotEqual(moveNext, Expression.Constant(false)), // if
                        Expression.Block(
                            callCurrent,
                            Expression.IfThenElse(
                                firstLessEqualSecond,
                                Expression.Assign(
                                    localVarNos1,
                                    Expression.Increment(localVarNos1)),
                                Expression.Assign(
                                    localVarNos2,
                                    Expression.Increment(localVarNos2)))),

                        Expression.Break(looplabel)),                              // else

                    looplabel),
                Expression.LessThan(localVarNos1, localVarNos2));

            Expression<Func<IEnumerable<int>, int, bool>> lambda =
                Expression.Lambda<Func<IEnumerable<int>, int, bool>>(block, enumerableExpression,
                    intexpression);

            Func<IEnumerable<int>, int, bool> dividesctionMethod = lambda.Compile();


            var resVal = dividesctionMethod(new[] { 1, 2, 3, 4, 5, 6 }, 20);
            Console.WriteLine(resVal);
        }

        private static void ParseExpression1()
        {
            Expression<Func<int, bool>> myExpressionDelegate = x => x < 5;

            ParameterExpression externalParam = myExpressionDelegate.Parameters[0];
            BinaryExpression bbody = myExpressionDelegate.Body as BinaryExpression;

            ParameterExpression parambodyleft = bbody.Left as ParameterExpression;
            ConstantExpression constRight = bbody.Right as ConstantExpression;
            ExpressionType type = bbody.NodeType;

            // create new 

            BlockExpression body = Expression.Block(Expression.LessThan(parambodyleft, constRight));
            Expression<Func<int, bool>> returnexpression = Expression.Lambda<Func<int, bool>>(body, externalParam);
            Func<int, bool> returnFunc = returnexpression.Compile();
            bool returnValue = returnFunc(12);
        }
    }

    public class Info
    {
        public string Desc { get; set; }
    }
    public class Animal
    {
        private string Name;
        public string Species { get; set; }
        public int Age { get; set; }
        public Info Info { get; set; }
    }

    public static class Ex
    {
        public static string GetName<T, TProperty>(this T self, Expression<Func<T, TProperty>> path)
        {
            Expression body = path.Body;
            var type = body.NodeType;
            MemberExpression memberExpression = body as MemberExpression;

            string value = memberExpression.Member.Name;
            return value;
        }
    }


}

