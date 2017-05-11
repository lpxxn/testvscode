using System;
using System.Data.Entity;

namespace EFDemo
{
    public class BloggingContext : DbContext
    {
        static BloggingContext()
        {
            Database.SetInitializer(new StandardInternalize());
        }

        public BloggingContext(string value) : base(value)
        {
            Init();
        }

        private void Init()
        {
            Database.Log = Console.WriteLine;
        }

        public IDbSet<T> GetSet<T>() where T : class
        {
            return this.Set<T>();
        }
        public BloggingContext()
        {
            Init();
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Test> Test { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x => x.DisplayName).HasColumnName("display_name");
        }
    }

    public class StandardInternalize : CreateDatabaseIfNotExists<BloggingContext>
    {
        public StandardInternalize()
        {
            
        }

        protected override void Seed(BloggingContext context)
        {
            base.Seed(context);
        }
    }
}
