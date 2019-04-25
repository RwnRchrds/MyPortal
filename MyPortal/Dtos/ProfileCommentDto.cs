namespace MyPortal.Dtos
{
    public class ProfileCommentDto
    {
        public int Id { get; set; }

        public int CommentBankId { get; set; }
        
        public string Value { get; set; }

        public ProfileCommentBankDto ProfileCommentBank { get; set; }
    }
}