using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Products")]
    public partial class Product : BaseTypes.Entity, ISoftDeleteEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BasketItems = new HashSet<BasketItem>();
            BillItems = new HashSet<BillItem>();
        }

        [Column(Order = 2)]
        public Guid ProductTypeId { get; set; }

        [Column(Order = 3)]
        public Guid VatRateId { get; set; }

        [Column(Order = 4)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 5)]
        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 6, TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Column(Order = 7)]
        public bool ShowOnStore { get; set; }
        
        [Column(Order = 8)]
        public int OrderLimit { get; set; }

        [Column(Order = 9)]
        public bool Deleted { get; set; }

        public virtual ProductType Type { get; set; }

        public virtual VatRate VatRate { get; set; }

        
        public virtual ICollection<BasketItem> BasketItems { get; set; }

        
        public virtual ICollection<BillItem> BillItems { get; set; }

        public virtual ICollection<StoreDiscount> ProductDiscounts { get; set; }
    }
}
