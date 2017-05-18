using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDemo
{
    public class Blog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid BlogId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Test { get; set; }

        public DateTime CreatedTime
        {
            get
            {
                return _createdTime ?? DateTime.Now;
            }
            set { this._createdTime = value; }
        }
        private DateTime? _createdTime = null;
        public virtual List<Post> Posts { get; set; } = new List<Post>();

        public Blog()
        {
        }
    }
}
