using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("ProductTypes")]
    public partial class ProductType : LookupItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        [Column(Order = 4)]
        public bool IsMeal { get; set; }

        
        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<StoreDiscount> ProductTypeDiscounts { get; set; }
    }
}