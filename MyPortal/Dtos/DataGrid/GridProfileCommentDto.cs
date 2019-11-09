using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridProfileCommentDto : IGridDto
    {
        public int Id { get; set; }
        public string CommentBankName { get; set; }
        public string Value { get; set; }
    }
}