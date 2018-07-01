using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    [Table("BasketItem")]
    public class BasketItem
    {
        [Display(Name = "ID")] public int Id { get; set; }

        [Display(Name = "Student")] public int Student { get; set; }

        [Display(Name = "Product")] public int Product { get; set; }

        public virtual Product Product1 { get; set; }

        public virtual Student Student1 { get; set; }
    }
}