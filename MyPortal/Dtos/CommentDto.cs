using MyPortal.Models;

namespace MyPortal.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int CommentBankId { get; set; }
        public string Value { get; set; }

        public CommentBankDto CommentBank { get; set; }
    }
}