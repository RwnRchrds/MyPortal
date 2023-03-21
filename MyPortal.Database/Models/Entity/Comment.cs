using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Comments")]
    public class Comment : BaseTypes.Entity
    {
        [Column(Order = 2)]
        public Guid CommentTypeId { get; set; }
        
        [Column(Order = 3)]
        public Guid CommentBankSectionId { get; set; }

        [Column(Order = 4)]
        [Required]
        public string Value { get; set; }

        public virtual CommentType CommentType { get; set; }
        public virtual CommentBankSection Section { get; set; }
    }
}
