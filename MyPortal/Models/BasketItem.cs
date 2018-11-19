using System.ComponentModel.DataAnnotations;

namespace MyPortal.Models
{
    public class BasketItem
    {
        [Display(Name = "ID")] public int Id { get; set; }

        [Display(Name = "Student")] public int StudentId { get; set; }

        [Display(Name = "Product")] public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Student Student { get; set; }
    }
}