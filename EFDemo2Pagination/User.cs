using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EFDemo;

namespace EFDemo2Pagination
{
    public class User
    {
        public User()
        {
        }
        [Key]
        public string Id { get; set; }
        [Required]
        public int Age { get; set; }
        public string Name { get; set; }
        //[Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        public DateTime CreatedTime { get; set; }

        public static User NewUser()
        {
            return new User()
            {
                Id = Guid.NewGuid().ToString(),
                Age = Utils.RandomInt(1, 100),
                Name = Utils.RandomString(Utils.RandomInt(3, 10)),
                CreatedTime = DateTime.Now.AddSeconds(Utils.RandomInt(1, 100))
            };
        }
    }
}