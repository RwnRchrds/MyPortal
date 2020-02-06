using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("EmailAddressType")]
    public class EmailAddressType
    {
        public EmailAddressType()
        {
            EmailAddresses = new HashSet<EmailAddress>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }
    }
}
