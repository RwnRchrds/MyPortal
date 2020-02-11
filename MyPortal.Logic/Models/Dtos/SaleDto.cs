using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class SaleDto
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid ProductId { get; set; }

        public Guid AcademicYearId { get; set; }

        public DateTime Date { get; set; }

        public decimal AmountPaid { get; set; }

        public bool Processed { get; set; }

        public bool Refunded { get; set; }

        public bool Deleted { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual AcademicYearDto AcademicYear { get; set; }

        public virtual ProductDto Product { get; set; }
    }
}
