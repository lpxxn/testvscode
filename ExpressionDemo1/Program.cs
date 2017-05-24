using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            

            ComposeFuncLambda();

            ComposeFuncLambda2();

            Console.ReadLine();
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
}

