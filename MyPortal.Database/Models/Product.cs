using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Product")]
    public partial class Product : IEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BasketItems = new HashSet<BasketItem>();
            Sales = new HashSet<Sale>();
        }

        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid ProductTypeId { get; set; }

        [Column(Order = 3)]
        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 4, TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Column(Order = 5)]
        public bool Visible { get; set; }

        [Column(Order = 6)]
        public bool OnceOnly { get; set; }

        [Column(Order = 7)]
        public bool Deleted { get; set; }

        public virtual ProductType Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasketItem> BasketItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
