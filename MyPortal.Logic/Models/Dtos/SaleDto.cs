using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Dtos
{
    public class SaleDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProductId { get; set; }

        public int AcademicYearId { get; set; }

        public DateTime Date { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount paid cannot be negative.")]
        public decimal AmountPaid { get; set; }

        public bool Processed { get; set; }

        public bool Refunded { get; set; }

        public bool Deleted { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual AcademicYearDto AcademicYear { get; set; }

        public virtual ProductDto Product { get; set; }
    }
}
