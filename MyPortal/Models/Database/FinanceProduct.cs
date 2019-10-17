namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A product available for purchase on the system.
    /// </summary>
    [Table("Finance_Products")]
    public partial class FinanceProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FinanceProduct()
        {
            BasketItems = new HashSet<FinanceBasketItem>();
            Sales = new HashSet<FinanceSale>();
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

        public virtual FinanceProductType Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinanceBasketItem> BasketItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinanceSale> Sales { get; set; }
    }
}
