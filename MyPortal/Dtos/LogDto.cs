using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Dtos
{
    public class LogDto
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public int AuthorId { get; set; }

        public int StudentId { get; set; }

        [Required] [StringLength(4000)] public string Message { get; set; }

        [Column(TypeName = "date")] public DateTime Date { get; set; }

        public LogTypeDto LogType { get; set; }

        public StaffDto Staff { get; set; }

        public StudentDto Student { get; set; }
    }
}