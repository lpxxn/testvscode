using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManualResetEvent1
{
    class Program
    {
        // false 
        private static ManualResetEvent _mre = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            var threads = new Thread[3];
            
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(ThreadRun);
                threads[i].Start();
            }
            //_mre.Set();

            //Console.ReadLine();
            var random = new Random();
            while (true)
            {
                Thread.Sleep(3000);
                var i = random.Next(100);
                if (i == 2)
                {
                    break;                    
                }
                if (i % 2 == 0)
                {
                    
                    _mre.Set();
                    Console.WriteLine("allow");
                }
                else
                {
                    _mre.Reset();
                    Console.WriteLine("block block .......");
                    Thread.Sleep(3000);

                }
            }
            Console.ReadLine();
        }

        static void ThreadRun()
        {
            int threadId = 0;
            while (true)
            {
               
                threadId = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine($"current threadId = {threadId}");
                _mre.WaitOne(3000);
                Console.WriteLine("running");
            }
        }
    }
}
