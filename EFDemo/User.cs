using System.ComponentModel.DataAnnotations;

namespace EFDemo
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Test { get; set; }
    }
}
