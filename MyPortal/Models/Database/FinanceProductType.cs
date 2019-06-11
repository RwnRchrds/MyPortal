using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    /// <summary>
    /// Type of product in the system.
    /// </summary>
    [Table("Finance_ProductTypes")]
    public partial class FinanceProductType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FinanceProductType()
        {
            Products = new HashSet<FinanceProduct>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        /// <summary>
        /// Indicates whether the product is a meal. Students entitled to FSM will not be charged for meals.
        /// </summary>
        public bool IsMeal { get; set; }

        /// <summary>
        /// Indicates whether the product is a system product. System products cannot be modified or deleted.
        /// </summary>
        public bool System { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinanceProduct> Products { get; set; }
    }
}