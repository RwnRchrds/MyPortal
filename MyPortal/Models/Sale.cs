namespace MyPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Student")]
        public int Student { get; set; }

        [Display(Name = "Product")]
        public int Product { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        public virtual Product Product1 { get; set; }

        public virtual Student Student1 { get; set; }
    }
}
