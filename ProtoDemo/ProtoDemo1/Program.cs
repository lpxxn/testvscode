using System;
using System.Net.Http;
using Google.Protobuf;
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
                Phones = { new Person.Types.PhoneNumber { Number = "555-4321", Type = PhoneType.Home } }
            };
            Console.WriteLine(john);
            Console.WriteLine(john.Phones[0].Type);
            HttpContent content = new ByteArrayContent(john.ToByteArray());
            HttpClient client = new HttpClient();
            
            var resp = client.PostAsync("http://localhost:9000/testproto1", content).Result;
            Console.WriteLine(resp.Content.ToString());
            Console.WriteLine(resp.StatusCode);

            var resData = resp.Content.ReadAsByteArrayAsync().Result;
            
            var p2 = Person.Parser.ParseFrom(resData);
            Console.WriteLine(p2);



            var resp2 = client.PostAsync("http://localhost:9000/testproto2", content).Result;
            Console.WriteLine(resp.Content.ToString());
            Console.WriteLine(resp.StatusCode);

            var resData2 = resp2.Content.ReadAsStringAsync().Result;

            var p3 = Person.Parser.ParseJson(resData2);
            Console.WriteLine(p3);

            Console.WriteLine("Hello World!");
        }
    }
}
