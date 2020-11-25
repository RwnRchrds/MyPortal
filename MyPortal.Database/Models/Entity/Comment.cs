using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Comments")]
    public class Comment : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid CommentBankId { get; set; }

        [Column(Order = 2)]
        [Required]
        public string Value { get; set; }

        public virtual CommentBank CommentBank { get; set; }
    }
}
