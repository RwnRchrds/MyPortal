using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    public class LogDto
    {
        public int Id { get; set; }

        public int Type { get; set; }

        [Required]
        [StringLength(3)]
        public string Author { get; set; }

        public int Student { get; set; }

        [Required]
        [StringLength(4000)]
        public string Message { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public LogTypeDto LogType { get; set; }

        public StaffDto Staff { get; set; }

        public StudentDto Student1 { get; set; }
    }
}