using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Comment")]
    public class Comment : IEntity
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid CommentBankId { get; set; }

        [Column(Order = 2)]
        [Required]
        public string Value { get; set; }

        public virtual CommentBank CommentBank { get; set; }
    }
}
