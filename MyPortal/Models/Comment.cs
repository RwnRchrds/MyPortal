using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    [Table("Reporting_Comments")]
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