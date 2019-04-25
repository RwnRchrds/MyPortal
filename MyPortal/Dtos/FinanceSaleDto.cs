using System;

namespace MyPortal.Dtos
{
    public class FinanceSaleDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProductId { get; set; }

        public DateTime Date { get; set; }

        public decimal AmountPaid { get; set; }

        public bool Processed { get; set; }

        public CoreStudentDto CoreStudent { get; set; }

        public FinanceProductDto FinanceProduct { get; set; }
    }
}