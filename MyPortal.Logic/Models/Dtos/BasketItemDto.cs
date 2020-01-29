namespace MyPortal.Logic.Models.Dtos
{
    public class BasketItemDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProductId { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual ProductDto Product { get; set; }
    }
}
