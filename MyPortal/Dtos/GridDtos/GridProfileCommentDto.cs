using MyPortal.Interfaces;

namespace MyPortal.Dtos.GridDtos
{
    public class GridProfileCommentDto : IGridDto
    {
        public int Id { get; set; }
        public string CommentBankName { get; set; }
        public string Value { get; set; }
    }
}