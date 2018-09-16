using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MyPortal.Models
{
    public class Product
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BasketItems = new HashSet<BasketItem>();
            Sales = new HashSet<Sale>();
        }

        [Display(Name = "ID")] 
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price")] 
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Available on Store")] 
        public bool Visible { get; set; }

        [Required]
        [Display(Name = "One-Time Purchase")] 
        public bool OnceOnly { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasketItem> BasketItems { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}