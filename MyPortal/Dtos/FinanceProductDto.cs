namespace MyPortal.Dtos
{
    public class FinanceProductDto
    {
        public int Id { get; set; }

        public int ProductTypeId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool Visible { get; set; }

        public bool OnceOnly { get; set; }

        public virtual FinanceProductTypeDto FinanceProductType { get; set; }
    }
}