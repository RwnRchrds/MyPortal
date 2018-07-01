using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    public class Log
    {
        [Display(Name = "ID")] public int Id { get; set; }

        [Required]
        [Display(Name = "Log Type")]
        public int Type { get; set; }

        [Required]
        [Display(Name = "Author")]
        [StringLength(3)]
        public string Author { get; set; }

        [Display(Name = "Student")] public int Student { get; set; }

        [Required]
        [StringLength(4000)]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual Student Student1 { get; set; }

        public virtual LogType LogType { get; set; }
    }
}