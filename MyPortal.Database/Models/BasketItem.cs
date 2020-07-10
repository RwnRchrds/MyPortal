using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("BasketItem")]
    public partial class BasketItem : IEntity
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 2)]
        public Guid ProductId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Product Product { get; set; }
    }
}
