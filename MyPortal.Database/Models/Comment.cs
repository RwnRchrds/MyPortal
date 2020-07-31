using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Comments")]
    public class Comment : Entity
    {
        [Column(Order = 1)]
        public Guid CommentBankId { get; set; }

        [Column(Order = 2)]
        [Required]
        public string Value { get; set; }

        public virtual CommentBank CommentBank { get; set; }
    }
}
