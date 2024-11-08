using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("BasketItems")]
    public class BasketItem : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid StudentId { get; set; }

        [Column(Order = 3)] public Guid ProductId { get; set; }

        [Column(Order = 4)] public int Quantity { get; set; }

        public virtual Student Student { get; set; }

        public virtual Product Product { get; set; }
    }
}