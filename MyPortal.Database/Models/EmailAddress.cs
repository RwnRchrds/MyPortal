using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("EmailAddress")]
    public class EmailAddress
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid TypeId { get; set; }

        [DataMember]
        public Guid PersonId { get; set; }

        [DataMember]
        [Required]
        [EmailAddress]
        [StringLength(128)]
        public string Address { get; set; }

        [DataMember]
        public bool Main { get; set; }

        [DataMember]
        public bool Primary { get; set; }

        [DataMember]
        public string Notes { get; set; }

        public virtual Person Person { get; set; }
        public virtual EmailAddressType Type { get; set; }
    }
}