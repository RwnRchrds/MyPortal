using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// Represents a sale created when an item is purchased.
    /// </summary>
    [Table("Sale")]
    public partial class Sale
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProductId { get; set; }

        public int AcademicYearId { get; set; }

        public DateTime Date { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "Amount paid cannot be negative")]
        public decimal AmountPaid { get; set; }

        public bool Processed { get; set; }

        public bool Refunded { get; set; }

        public bool Deleted { get; set; }

        public virtual Student Student { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Product Product { get; set; }
    }
}
