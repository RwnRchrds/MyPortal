using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Products")]
    public partial class Product : BaseTypes.Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BasketItems = new HashSet<BasketItem>();
            BillItems = new HashSet<BillItem>();
        }

        [Column(Order = 1)]
        public Guid ProductTypeId { get; set; }

        [Column(Order = 2)]
        public Guid VatRateId { get; set; }

        [Column(Order = 3)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 4)]
        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 5, TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Column(Order = 6)]
        public bool ShowOnStore { get; set; }

        [Range(0, Int32.MaxValue)]
        [Column(Order = 7)]
        public int OrderLimit { get; set; }

        [Column(Order = 8)]
        public bool Deleted { get; set; }

        public virtual ProductType Type { get; set; }

        public virtual VatRate VatRate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasketItem> BasketItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillItem> BillItems { get; set; }

        public virtual ICollection<ProductDiscount> ProductDiscounts { get; set; }
    }
}
