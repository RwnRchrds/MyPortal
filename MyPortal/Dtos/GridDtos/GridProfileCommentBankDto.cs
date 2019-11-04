using MyPortal.Interfaces;

namespace MyPortal.Dtos.GridDtos
{
    public class GridProfileCommentBankDto : IGridDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}