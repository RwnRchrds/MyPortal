namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Represents a sale created when a student purchases an item.
    /// </summary>
    public partial class FinanceSaleDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProductId { get; set; }

        public int AcademicYearId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// The amount paid at the time of purchase.
        /// </summary>
        public decimal AmountPaid { get; set; }

        /// <summary>
        /// Indicates whether the sale has been processed by the school's finance department.
        /// </summary>
        public bool Processed { get; set; }

        public virtual StudentDto CoreStudent { get; set; }

        public virtual CurriculumAcademicYearDto CurriculumAcademicYear { get; set; }

        public virtual FinanceProductDto FinanceProduct { get; set; }
    }
}
