using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumDemo1
{
    [Flags]
    public enum TestFlag
    {
        None    = 0,    // 000000
        IsCheck = 1,    // 000001
        CanCon  = 2,    // 000010
        Display = 4,    // 000100
        Enable  = 8,    // 001000
        All     = IsCheck | CanCon | Display |Enable // 001111
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"None     : {(int)TestFlag.None}");
            Console.WriteLine($"IsCheck  : {(int)TestFlag.IsCheck}");
            Console.WriteLine($"CanCon   : {(int)TestFlag.CanCon}");
            Console.WriteLine($"Display  : {(int)TestFlag.Display}");
            Console.WriteLine($"Enable   : {(int)TestFlag.Enable}");
            Console.WriteLine($"ALL      : {(int)TestFlag.All}");

            // or
            var test1 = TestFlag.None;
            Console.WriteLine("Bitwise OR Operate");
            test1 = TestFlag.Display | TestFlag.CanCon;
            test1 |= TestFlag.Enable;
            Console.WriteLine($"current test1: {test1}");

            Console.WriteLine("Bitwise And Operate");
            Console.WriteLine($"whether test1 contain IsCheck : {(test1 & TestFlag.IsCheck) == TestFlag.IsCheck}");
            
            test1 = test1 & TestFlag.CanCon;
            Console.WriteLine($"test1 = test1 & TestFlag.CanCon : {test1}");

            Console.WriteLine($"current test1: {test1}");

            Console.WriteLine("Bitwise XOR Operate");
            Console.WriteLine("0  0 : 0");
            Console.WriteLine("1  0 : 1");
            Console.WriteLine("0  1 : 1");
            Console.WriteLine("1  1 : 0");
            test1 = test1 ^ TestFlag.CanCon;
            Console.WriteLine(test1);
            test1 ^= TestFlag.IsCheck;
            Console.WriteLine(test1);

            test1 ^= TestFlag.Display;
            Console.WriteLine(test1);

            test1 |= TestFlag.CanCon | TestFlag.Display;
            Console.WriteLine($"current test1: {test1}");
            Console.WriteLine("Bitwise NOT Operate");
            Console.WriteLine($"0100 : 1011");
            test1 = test1 & ~TestFlag.Display;
            Console.WriteLine($"test1 & ~TestFlag.Display: {test1}");

          
           
            // mt get a flag
            var test2 = TestFlag.All & ~TestFlag.Display;

            Console.WriteLine($"current test2: {test2}");
            Console.WriteLine($"current test2: {(int)test2}");

            Console.WriteLine((test2 & TestFlag.IsCheck) == TestFlag.IsCheck ? "have IsCheck Flag" : "not have IsCheck");


            Console.WriteLine((test2 & TestFlag.CanCon) != 0 ? "have CanCon Flag" : "not have CanCon");

            Console.WriteLine((test2 & TestFlag.Display) == TestFlag.Display ? "have Display Flag" : "not have Display");

            Console.WriteLine((test2 & TestFlag.Enable) == TestFlag.Enable ? "have Enable Flag" : "not have Enable");




            Console.ReadLine();
        }
    }
}
