using System;
using System.Linq;

namespace EFDemo
{
    public class Utils
    {
        private static readonly Random ReRandom = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[ReRandom.Next(s.Length)]).ToArray());
        }

        public static int RandomInt(int min, int max) => ReRandom.Next(min, max);


    }
}