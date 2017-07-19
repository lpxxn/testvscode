﻿using System;
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

                var stuDress = new StudentAddress() { Address1 = "aa", Address2 = "a" };
                var student = new Student() { StudentId = 1, StudentAddress = stuDress, StudentPhoto = new StudentPhoto()};
                context.Students.Add(student);
                context.SaveChanges();

                var student2 = new Student() { StudentId = 2, StudentPhoto = new StudentPhoto() { FileName = "filename1" }, StudentAddress = new StudentAddress()};
                context.Students.Add(student2);
                context.SaveChanges();

                var s1 = context.StudentsPhotos.First(x => x.StudentId == 1);
                s1.FileName = "haha";
                context.SaveChanges();

                var s2 = context.StudentsAddresses.First(x => x.StudentId == 2);
                s2.Address2 = "modify222";
                context.Entry(s2).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                //var s4 = context.Students.Where(x => x.StudentId == 4).ToList();
                var s3 = context.Students.FirstOrDefault(x => x.StudentId == 4);
                Console.WriteLine($"{s3.Name}, {s3.StudentAddress.Address2}, {s3.StudentPhoto.FileName}");

            }

            Console.WriteLine("Database Created!!!");
            Console.ReadKey();
        }
    }
}
