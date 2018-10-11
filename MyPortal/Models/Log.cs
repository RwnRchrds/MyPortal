namespace MyPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Log Type")]
        public int TypeId { get; set; }

        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        [Display(Name = "Student")]
        public int StudentId { get; set; }

        [Required]
        public string Message { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public virtual LogType LogType { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual Student Student { get; set; }
    }
}
