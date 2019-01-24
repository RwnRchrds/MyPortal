using System.ComponentModel.DataAnnotations;

namespace MyPortal.Models
{
    public class Comment
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        
        [Display(Name = "Comment Bank")]
        public int CommentBankId { get; set; }
        
        [Required] public string Value { get; set; }

        public virtual CommentBank CommentBank { get; set; }
    }
}