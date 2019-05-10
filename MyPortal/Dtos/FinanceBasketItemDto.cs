namespace MyPortal.Dtos
{
    public class FinanceBasketItemDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProductId { get; set; }

        public StudentDto CoreStudent { get; set; }

        public FinanceProductDto FinanceProduct { get; set; }
    }
}