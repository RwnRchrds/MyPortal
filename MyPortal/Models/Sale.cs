using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    public class Sale
    {
        [Display(Name = "ID")] public int Id { get; set; }

        [Display(Name = "Student")] public int StudentId { get; set; }

        [Display(Name = "Product")] public int ProductId { get; set; }

        [Column(TypeName = "date")] public DateTime Date { get; set; }

        [Display(Name = "Amount Paid")]
        public decimal AmountPaid { get; set; }

        public bool Processed { get; set; }

        public virtual Product Product { get; set; }

        public virtual Student Student { get; set; }
    }
}