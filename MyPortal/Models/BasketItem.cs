using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    [Table("Finance_BasketItems")]
    public class BasketItem
    {
        [Display(Name = "ID")] public int Id { get; set; }

        [Display(Name = "Student")] public int StudentId { get; set; }

        [Display(Name = "Product")] public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Student Student { get; set; }
    }
}