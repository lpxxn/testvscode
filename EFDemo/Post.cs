
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDemo
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid? BlogId { get; set; }
        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; }
    }
}
