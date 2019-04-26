namespace MyPortal.Dtos
{
    public class FinanceBasketItemDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProductId { get; set; }

        public CoreStudentDto CoreStudent { get; set; }

        public FinanceProductDto FinanceProduct { get; set; }
    }
}