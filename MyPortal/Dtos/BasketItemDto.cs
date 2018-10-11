namespace MyPortal.Dtos
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ProductId { get; set; }

        public ProductDto Product { get; set; }
        public StudentDto Student { get; set; }
    }
}