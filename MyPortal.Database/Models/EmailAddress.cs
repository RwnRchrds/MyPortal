using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("EmailAddress")]
    public class EmailAddress : IEntity
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid TypeId { get; set; }

        [Column(Order = 2)]
        public Guid PersonId { get; set; }

        [Column(Order = 3)]
        [Required]
        [EmailAddress]
        [StringLength(128)]
        public string Address { get; set; }

        [Column(Order = 4)]
        public bool Main { get; set; }

        [Column(Order = 5)]
        public bool Primary { get; set; }

        [Column(Order = 6)]
        public string Notes { get; set; }

        public virtual Person Person { get; set; }
        public virtual EmailAddressType Type { get; set; }
    }
}