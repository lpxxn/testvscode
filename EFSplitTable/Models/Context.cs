using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSplitTable.Models
{
    public class Context : DbContext
    {
        public Context()
            : base()
        {
        }

        public DbSet<Student> Students { get; set; }
        
        public DbSet<StudentAddress> StudentsAddresses { get; set; }
        public DbSet<StudentPhoto> StudentsPhotos { get; set; }


        public DbSet<T> Get<T>() where T :class 
        {
            return this.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
               .HasRequired(e => e.StudentAddress)
               .WithRequiredPrincipal();

            modelBuilder.Entity<Student>()
               .HasRequired(e => e.StudentPhoto)
               .WithRequiredPrincipal();

            base.OnModelCreating(modelBuilder);
        }
    }
}
