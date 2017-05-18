using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
   
    public class Program2
    {
        public static void Main(string[] args)
        {
            
            Test();
            Console.WriteLine("hello world");
            Console.ReadLine();
        }

        public static async Task Test()
        {
            var p = new Program2();
            var t1 = p.Method1();
            var t2 = p.Method2();
            Console.WriteLine($"{t1}  {t2}");
            var t3 = await p.Method2();
            Console.WriteLine(t3);
            
            Console.WriteLine("end");
        }

        public async Task<string> Method1()
        {
            return "abc";
        }

        public async Task<string> Method2()
        {
            return await Task.Run(() => { return "abc"; });
        }

       
    }
}
