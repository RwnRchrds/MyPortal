using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("EmailAddresses")]
    public class EmailAddress : Entity
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