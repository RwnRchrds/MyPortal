using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// A product available for purchase on the system.
    /// </summary>
    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BasketItems = new HashSet<BasketItem>();
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }

        public int ProductTypeId { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [Range(0.00, double.MaxValue, ErrorMessage = "Price cannot be negative")]
        public decimal Price { get; set; }

        public bool Visible { get; set; }

        public bool OnceOnly { get; set; }

        public bool Deleted { get; set; }

        public virtual ProductType Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasketItem> BasketItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
