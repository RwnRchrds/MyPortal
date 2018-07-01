using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    public class Sale
    {
        [Display(Name = "ID")] public int Id { get; set; }

        [Display(Name = "Student")] public int Student { get; set; }

        [Display(Name = "Product")] public int Product { get; set; }

        [Display(Name = "Date")]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public virtual Product Product1 { get; set; }

        public virtual Student Student1 { get; set; }
    }
}