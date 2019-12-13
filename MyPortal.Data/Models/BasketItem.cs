using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// Represents a product in a student's basket.
    /// </summary>
    [Table("BasketItem", Schema = "finance")]
    public partial class BasketItem
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProductId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Product Product { get; set; }
    }
}
