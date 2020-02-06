using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("EmailAddress")]
    public class EmailAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid TypeId { get; set; }

        public Guid PersonId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(128)]
        public string Address { get; set; }

        public bool Main { get; set; }
        public bool Primary { get; set; }
        public string Notes { get; set; }

        public virtual Person Person { get; set; }
        public virtual EmailAddressType Type { get; set; }
    }
}