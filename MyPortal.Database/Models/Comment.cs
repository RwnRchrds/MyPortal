using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("Comment")]
    public class Comment
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid CommentBankId { get; set; }

        [DataMember]
        [Required]
        public string Value { get; set; }

        public virtual CommentBank CommentBank { get; set; }
    }
}
