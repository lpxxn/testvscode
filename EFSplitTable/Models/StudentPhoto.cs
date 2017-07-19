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
    public class StudentPhoto
    {
        [Key]
        public int StudentId { get; set; }

        public byte[] Photo { get; set; }

        public string FileName { get; set; }

        [ForeignKey("StudentId")]
        public Student AddressOf { get; set; }
    }
}
