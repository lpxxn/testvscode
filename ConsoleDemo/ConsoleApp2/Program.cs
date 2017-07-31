using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 100, x =>
            {
                Console.WriteLine(RandomString(8));
            });
            Console.WriteLine("-------------");
            Parallel.For(0, 100, x =>
            {
                Console.WriteLine(RandomString2(8));
            });
            //            for (int i = 0; i < 100; i++)
            //            {
            ////                // get 1st random string 
            ////                string Rand1 = RandomString(4);
            ////
            ////                // get 2nd random string 
            ////                string Rand2 = RandomString(4);
            ////
            ////                // creat full rand string
            ////                string docNum = Rand1 + Rand2;
            //                //Console.WriteLine(docNum);
            //                //Console.WriteLine("----------");
            //                
            //                
            //            }

            (int quotient, int remainder) = GetDivisionResults(17, 5);
            Console.WriteLine("Quotient is " + quotient);
            Console.WriteLine("Remainder is " + remainder);

        }

        static (int, int) GetDivisionResults(int number, int divisor)
        {
            try
            {
                int quotient = number / divisor;
                int remainder = number % divisor;
                return (quotient, remainder);
            }
            finally
            {
                Console.WriteLine("Hi this is inner of finally  ");
            }

            return (1, 2);

        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#*";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomString2(int length)
        {
            const string valid = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#*";
            string s = "";
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                while (s.Length != length)
                {
                    var oneByte = new byte[1];
                    provider.GetBytes(oneByte);
                    char character = (char)oneByte[0];
                    if (valid.Contains(character))
                    {
                        s += character;
                    }
                }
            }
            return s;
        }
    }
}
