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
    public class StudentAddress
    {
        [Key]
        public int StudentId { get; set; }

        public string Address1 { get; set; }

        public  string Address2 { get; set; }
        [ForeignKey("StudentId")]
        public Student PhotoOf { get; set; }
    }
}
