using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("EmailAddresses")]
    public class EmailAddress : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid TypeId { get; set; }

        [Column(Order = 2)]
        public Guid? PersonId { get; set; }

        [Column(Order = 3)]
        public Guid? AgencyId { get; set; }

        [Column(Order = 4)]
        [Required]
        [EmailAddress]
        [StringLength(128)]
        public string Address { get; set; }

        [Column(Order = 5)]
        public bool Main { get; set; }

        [Column(Order = 7)]
        public string Notes { get; set; }

        public virtual Person Person { get; set; }
        public virtual EmailAddressType Type { get; set; }
    }
}