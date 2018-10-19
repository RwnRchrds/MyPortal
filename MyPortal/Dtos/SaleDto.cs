using System;

namespace MyPortal.Dtos
{
    public class SaleDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public decimal AmountPaid { get; set; }
        public bool Processed { get; set; }

        public StudentDto Student { get; set; }
        public ProductDto Product { get; set; }
    }
}