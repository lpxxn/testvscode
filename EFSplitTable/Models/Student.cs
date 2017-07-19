using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSplitTable.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        public string Name { get; set; }

        
        public virtual StudentPhoto StudentPhoto { get; set; } = new StudentPhoto();

        public virtual StudentAddress StudentAddress { get; set; } = new StudentAddress();
    }
}
