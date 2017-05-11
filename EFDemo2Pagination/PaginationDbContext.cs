using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using EFDemo;

namespace EFDemo2Pagination
{
    public class PaginationDbContext : DbContext
    {
        public PaginationDbContext():base("cs")
        {
            if (typeof(DbConfig).BaseType.Name != typeof(DropCreateDatabaseAlways<>).Name)
                Database.Log = Console.WriteLine;
        }
        
        public DbSet<User> User { get; set; }

        public DbSet<T> GetEntity<T>() where T : class
        {
            return this.Set<T>();
        }

        public static void InternalizeConfig() => Database.SetInitializer(new DbConfig());

    }
    // DropCreateDatabaseAlways   CreateDatabaseIfNotExists
    public class DbConfig : CreateDatabaseIfNotExists<PaginationDbContext>
    {
        protected override void Seed(PaginationDbContext context)
        {
            for (var i = 0; i < 10000; i++)
            {
                context.User
                   .Add(User.NewUser());
            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
