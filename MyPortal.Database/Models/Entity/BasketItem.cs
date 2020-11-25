using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("BasketItems")]
    public partial class BasketItem : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 2)]
        public Guid ProductId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Product Product { get; set; }
    }
}
