namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Finance_Sales")]
    public partial class FinanceSale
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProductId { get; set; }

        public int AcademicYearId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public decimal AmountPaid { get; set; }

        public bool Processed { get; set; }

        public virtual PeopleStudent CoreStudent { get; set; }

        public virtual CurriculumAcademicYear CurriculumAcademicYear { get; set; }

        public virtual FinanceProduct FinanceProduct { get; set; }
    }
}
