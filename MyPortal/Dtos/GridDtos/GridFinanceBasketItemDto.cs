using MyPortal.Interfaces;

namespace MyPortal.Dtos.GridDtos
{
    public class GridFinanceBasketItemDto : IGridDto
    {
        public int Id { get; set; }

        public string StudentName { get; set; }

        public string ProductDescription { get; set; }
    }
}