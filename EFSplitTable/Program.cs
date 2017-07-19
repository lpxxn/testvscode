using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFSplitTable.Models;

namespace EFSplitTable
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<Context>());

            using (var context = new Context())
            {
                context.Database.Initialize(true);

                var stuDress = new StudentAddress(){Address1 = "aa", Address2 = "a"};
                var student = new Student() {StudentId = 1, StudentAddress = stuDress};
                context.Students.Add(student);
                context.SaveChanges();

                var student2 = new Student() {StudentId = 2, StudentPhoto = new StudentPhoto() { FileName = "filename1"}};
                context.Students.Add(student2);
                context.SaveChanges();

                var s1 = context.StudentsPhotos.First(x => x.StudentId == 1);
                s1.FileName = "haha";               
                context.SaveChanges();                
            }

            Console.WriteLine("Database Created!!!");
            Console.ReadKey();
        }
    }
}
