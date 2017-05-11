using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDemo;

namespace EFDemoDelete
{
    public class CascadeDbContext : DbContext 
    {
        public CascadeDbContext() : base("cs")
        {
            //if (typeof(StandardInitialization).BaseType.Name != typeof(DropCreateDatabaseAlways<>).Name)
                //Database.Log = Console.WriteLine;
        }
        public DbSet<T> Get<T>() where T : class
        {
            return this.Set<T>();
        }

        public static void InternalizeConfig() => Database.SetInitializer(new StandardInitialization());

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }



    // DropCreateDatabaseAlways   CreateDatabaseIfNotExists
    public class StandardInitialization : DropCreateDatabaseAlways<CascadeDbContext>
    {
        protected override void Seed(CascadeDbContext context)
        {

            Console.WriteLine("Seed");
            //context.Get<Blog>().AddRange(new Blog[] { new Blog(){Name = }, })
            for (int i = 0; i < 10; i++)
            {
                var blog = context.Get<Blog>()
                .Add(new Blog() { BlogId = Guid.NewGuid(), Name = Utils.RandomString(Utils.RandomInt(3, 7)), Url = Utils.RandomString(10) });

                for (int postIndex = 0, size = Utils.RandomInt(1, 5); postIndex < size; postIndex++)
                {
                    context.Get<Post>()
                        .Add(new Post()
                        {
                            PostId = Guid.NewGuid(),
                            BlogId = blog.BlogId,
                            Title = Utils.RandomString(Utils.RandomInt(0, 5)),
                            Content = Utils.RandomString(Utils.RandomInt(0, 10)),                           
                        });
                }
            }

            context.SaveChanges();
            

            base.Seed(context);
        }

        public override void InitializeDatabase(CascadeDbContext context)
        {

            base.InitializeDatabase(context);
        }
    }
}
