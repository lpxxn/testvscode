using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaitHandleDemo1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static void Main()
        {
            ManualResetEvent[] events = new ManualResetEvent[10];//Create a wait handle
            for (int i = 0; i < events.Length; i++)
            {
                events[i] = new ManualResetEvent(false);
                Runner r = new Runner(events[i], i);
                new Thread(r.Run).Start();
            }

            //STEP 2: Register for the events to wait for
            int index = WaitHandle.WaitAny(events); //wait here for any event and print following line.

            Console.WriteLine("***** The winner is {0} *****",
                               index);

            WaitHandle.WaitAll(events); //Wait for all of the threads to finish, that is, to call their cooresponding `.Set()` method.

            Console.WriteLine("All finished!");
        }
    }

    public class Runner
    {
        private static readonly object RngLock = new object();
        private static readonly Random Rng = new Random();

        private readonly ManualResetEvent _ev;
        private readonly int _id;

        public Runner(ManualResetEvent ev, int id)
        {
            this._ev = ev; // wait handle associated to each object , thread  in this case
            this._id = id;
        }

        public void Run()
        {
            // STEP3 : Do Some Work
            for (int i = 0; i < 10; i++)
            {
//                int sleepTime;
//                lock (RngLock)
//                {
//                    sleepTime = Rng.Next(2000);
//                }
//                Thread.Sleep(sleepTime);
                Thread.Sleep(2000);
                Console.WriteLine($"runner {_id} at  stage {i}");
            }
            Console.WriteLine("\n");
            // STEP 4: Im done!
            _ev.Set();
            Console.Write("Im done !");
        }


    }


    public interface ABase<out T> where T: class
    {
        T Fun1();

    }

    public partial class ASun
    {
        public const string Desc = "hello";
    }

    public partial class  ASun : ABase<ASun>
    {
        public string Name { get; set; }
        public ASun Fun1()
        {
            var a = new ASun() {Name = this.Name};
            return a;
        }
    }
}
