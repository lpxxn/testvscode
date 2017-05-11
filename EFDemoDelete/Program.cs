using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDemo;
using EntityFramework.Extensions;

namespace EFDemoDelete
{
    class Program
    {
        static void Main(string[] args)
        {
            var uuid = Guid.NewGuid();
            Console.WriteLine($"Uuid: {uuid},  Length: {uuid.ToString().Length}");
            CascadeDbContext.InternalizeConfig();

            using (var db = new CascadeDbContext())
            {
                db.Get<Blog>().ToList().ForEach(x =>
                {
                    Console.WriteLine($"Id = {x.BlogId}, Name = {x.Name}, Url = {x.Url}");
                    x.Posts.ForEach(post =>
                    {
                        Console.WriteLine($" ------    PostID = {post.PostId}, BlogId = {post.BlogId}, Title = {post.Title}, Content = {post.Content}");
                    });
                });
                //db.Get<Post>().ToList().ForEach(x =>
                //{
                //    Console.WriteLine($"PostID = {x.PostId}, BlogId = {x.BlogId}, Title = {x.Title}, Content = {x.Content}");
                //});
                Console.WriteLine("type blogId that used to delete:");
                var delId = Guid.Parse(Console.ReadLine());
                var model = new Blog() {BlogId = delId};

                // 1
                //db.Get<Blog>().Attach(model);
                // 2
                //db.Entry(model).State = EntityState.Deleted;
                //db.SaveChanges();

                // 3
                var delResult = db.Get<Blog>().Where(x => x.BlogId == delId).Delete();
                Console.WriteLine($"deleted rows : {delResult}");

                //delResult = db.Get<Post>().Where(x => x.BlogId == delId).Delete();
                //Console.WriteLine($"deleted rows : {delResult}");
            }

            Console.ReadLine();
        }
    }
}
