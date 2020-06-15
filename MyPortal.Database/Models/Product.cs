using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BasketItems = new HashSet<BasketItem>();
            Sales = new HashSet<Sale>();
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid ProductTypeId { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [DataMember]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [DataMember]
        public bool Visible { get; set; }

        [DataMember]
        public bool OnceOnly { get; set; }

        [DataMember]
        public bool Deleted { get; set; }

        public virtual ProductType Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasketItem> BasketItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
