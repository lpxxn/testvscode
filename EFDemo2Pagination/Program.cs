using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;

namespace EFDemo2Pagination
{
    class Program
    {
        static void Main(string[] args)
        {
            PaginationDbContext.InternalizeConfig();

            using (var db = new PaginationDbContext())
            {

                Console.WriteLine("--------------------------");
                var q = db.User.Where(x => x.Age > 10).OrderBy(x => x.CreatedTime);
                db.User.Add(User.NewUser());
                var q1 = q.FutureCount();
                var q2 = q.Skip(0).Take(20).Future();
                int total = q1.Value;
                var users = q2.ToList<User>();
                //var users = q.ToList();
                Console.WriteLine($"total {total}");
                users.ForEach(x=> Console.WriteLine($"Id = {x.Id}, Name = {x.Name}, Age = {x.Age}, CreatedTime = {x.CreatedTime}"));

            }

            Console.ReadLine();
        }
    }





}
