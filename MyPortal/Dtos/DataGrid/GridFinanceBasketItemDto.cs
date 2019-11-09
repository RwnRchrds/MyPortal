using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridFinanceBasketItemDto : IGridDto
    {
        public int Id { get; set; }

        public string StudentName { get; set; }

        public string ProductDescription { get; set; }
    }
}