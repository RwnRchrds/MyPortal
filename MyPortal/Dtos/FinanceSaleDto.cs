namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Represents a sale created when an item is purchased.
    /// </summary>
    
    public partial class FinanceSaleDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProductId { get; set; }

        public int AcademicYearId { get; set; }

        
        public DateTime Date { get; set; }

        
        public decimal AmountPaid { get; set; }

        public bool Processed { get; set; }

        public bool Refunded { get; set; }

        public bool Deleted { get; set; }

        public virtual StudentDto StudentDto { get; set; }

        public virtual CurriculumAcademicYearDto CurriculumAcademicYear { get; set; }

        public virtual FinanceProductDto FinanceProduct { get; set; }
    }
}
