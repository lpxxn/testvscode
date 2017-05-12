using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EFDemo;
using EFDemoDelete;
using EntityFramework.Extensions;

namespace EfDemoTransaction
{
    class Program
    {
        static void Main(string[] args)
        {
            CascadeDbContext.InternalizeConfig();

            using (var db = new CascadeDbContext())
            {
                db.Get<Blog>().ToList().ForEach(x =>
                {
                    Console.WriteLine($"Id = {x.BlogId}, Name = {x.Name}, Url = {x.Url}");
                    x.Posts.ForEach(post =>
                    {
                        Console.WriteLine(
                            $" ------    PostID = {post.PostId}, BlogId = {post.BlogId}, Title = {post.Title}, Content = {post.Content}");
                    });
                });
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Posts");
                db.Get<Post>().ToList().ForEach(x =>
                {
                    Console.WriteLine(
                        $"PostID = {x.PostId}, BlogId = {x.BlogId}, Title = {x.Title}, Content = {x.Content}");
                });
                
                Console.WriteLine("type blogId that used to Update:");
                var blogGuid = Guid.Parse(Console.ReadLine());
                var model = new Blog() { BlogId = blogGuid };


                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        db.Get<Post>().Where(x => x.BlogId == blogGuid).Update(x => new Post() {BlogId = null});

                        db.Get<Blog>().Where(x => x.BlogId == blogGuid).Delete();
                        db.SaveChanges();

                        transaction.Complete();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("have error");
                        Console.WriteLine(e);
                    }
                }


                Console.WriteLine("type blogId that used to Update:");
                blogGuid = Guid.Parse(Console.ReadLine());
                Console.WriteLine("Move Posts to BlogId");
                var boBlogGuid = Guid.Parse(Console.ReadLine());


                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        db.Get<Post>().Where(x => x.BlogId == blogGuid).Update(x => new Post() { BlogId = boBlogGuid , Title = x.Title + "moved"});

                        db.Get<Blog>().Where(x => x.BlogId == blogGuid).Update(x=> new Blog() {Name = x.Name + "Modified"});
                        db.SaveChanges();

                        transaction.Complete();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("have error");
                        Console.WriteLine(e);
                    }
                }


                Console.WriteLine("end");
                Console.ReadLine();
            }
        }
    }
}
