using System;
using Tutorial;
using static Tutorial.Person.Types;

namespace ProtoDemo1
{

   
    class Program
    {
        static void Main(string[] args)
        {

            Person john = new Person
            {
                Id = 1234,
                Name = "John Doe",
                Email = "jdoe@example.com",
                Phones = { new Person.Types.PhoneNumber { Number = "555-4321", Type = PhoneType.Mobile } }
            };
            Console.WriteLine(john);
            Console.WriteLine(john.Phones[0].Type);
            Console.WriteLine("Hello World!");
        }
    }
}
