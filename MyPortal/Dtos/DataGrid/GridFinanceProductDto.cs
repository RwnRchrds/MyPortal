using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridFinanceProductDto : IGridDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string TypeDescription { get; set; }
        public decimal Price { get; set; }
        public bool Visible { get; set; }
        public bool OnceOnly { get; set; }
    }
}