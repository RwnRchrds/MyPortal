namespace MyPortal.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool? Visible { get; set; }
        public bool? OnceOnly { get; set; }
    }
}