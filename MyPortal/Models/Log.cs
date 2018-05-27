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

        public virtual Staff Staff { get; set; }

        public virtual Student Student1 { get; set; }

        public virtual LogType LogType { get; set; }
    }
}
