using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using EFDemo.Migrations;

namespace EFDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Version.ToString());
            try
            {//
                using (var db = new BloggingContext("cs"))
                {
                    
                    // Create and save a new Blog 
                    Console.Write("Enter a name for a new Blog: ");
                    var name = Console.ReadLine();
                    Console.WriteLine( $"-=------Connect State : {db.Database.Connection.State}");

                    var blog = new Blog { Name = name };

                    var myBlog = db.GetSet<Blog>();
                    myBlog.Add(blog);
                    Console.WriteLine($"-=------Connect State : {db.Database.Connection.State}");
                    //db.Blogs.Add(blog);
                    db.SaveChanges();
                    Console.WriteLine($"-=------Connect State : {db.Database.Connection.State}");
                    // Display all Blogs from the database 
                    var query = from b in db.Blogs
                                orderby b.Name
                                select b;
                    Console.WriteLine($"C-=------onnect State : {db.Database.Connection.State}");
                    Console.WriteLine("All blogs in the database:");
                    foreach (var item in query)
                    {
                        Console.WriteLine($"Name {item.Name}, Url: {item.Url}");
                    }

                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }

    public class Test
    {
        [Key]
        public string Id { get; set; }
    }
}
