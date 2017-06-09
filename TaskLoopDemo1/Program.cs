using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskLoopDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancel = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Console.WriteLine("before wait 3s");
                    var value = cancel.Token.WaitHandle.WaitOne(3000);
                    Console.WriteLine($"after wait return value = {value}");
                    if (value)
                        break;
                }
                Console.WriteLine("break Task");
            }, cancel.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
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
                if (i % 3 == 0)
                {
                    cancel.Cancel();
                    break;
                    
                }              
            }
            Console.ReadLine();
        }
    }
}
