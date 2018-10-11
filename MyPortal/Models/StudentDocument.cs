namespace MyPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StudentDocument
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Student")]
        public int StudentId { get; set; }

        [Display(Name = "Document")]
        public int DocumentId { get; set; }

        public virtual Document Document { get; set; }

        public virtual Student Student { get; set; }
    }
}
