using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Data.Models
{
    [Table("EmailAddressType")]
    public class EmailAddressType
    {
        public EmailAddressType()
        {
            EmailAddresses = new HashSet<EmailAddress>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }
    }
}
