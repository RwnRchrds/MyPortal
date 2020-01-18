using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// A comment that can be used in the creation of a log note.
    /// </summary>
    [Table("Comment")]
    public partial class Comment
    {
        public int Id { get; set; }

        public int CommentBankId { get; set; }

        [Required]
        public string Value { get; set; }

        public virtual CommentBank CommentBank { get; set; }
    }
}
