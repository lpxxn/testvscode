using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(TaskTest1());            

            // async
            var a =AsyncTest1("abcde");
            //Console.WriteLine(a.Result);
            Console.WriteLine("test Task");

            AsyncTest2("aa");
            // sync
            TaskResultTest1();
            Console.Read();
        }

        static string TaskTest1()
        {
            var task = Task.Run(() =>
            {
                Thread.Sleep(200);
                Console.WriteLine("Hi Task test 1");
            });
            //task.Wait();
            return "Hello World";
        }

        static void TaskResultTest1()
        {
           
            Task<string> task = Task<string>.Run(() =>
            {
                Thread.Sleep(200);
                Console.WriteLine("inner the task");
                return "hi result ： " + Thread.CurrentThread.ManagedThreadId.ToString();
            });
            Console.WriteLine(task.Result);
            Console.WriteLine("task result over");
        }

        static async Task<int> AsyncTest1(string str)
        {
            Console.WriteLine("begin AsyncTest1");
            var a = await Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine($"AsyncTest1  {str} len : {str.Length}");
                Console.WriteLine("AsyncTest1 running");
               
                return str.Length;
            });
            Console.WriteLine("end AsyncTest1");
            return a;
        }

        static async void AsyncTest2(string str)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(200);
               
                Console.WriteLine("AsyncTest2 running");

            });
        }
    }
}
