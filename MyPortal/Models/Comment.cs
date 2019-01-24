using System.ComponentModel.DataAnnotations;

namespace MyPortal.Models
{
    public class Comment
    {
        public int Id { get; set; }
        
        public int CommentBankId { get; set; }
        
        [Required] public string Value { get; set; }

        public virtual CommentBank CommentBank { get; set; }
    }
}