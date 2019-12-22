using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Data.Models
{
    [Table("EmailAddressType", Schema = "communication")]
    public class EmailAddressType
    {
        public EmailAddressType()
        {
            EmailAddresses = new HashSet<EmailAddress>();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }
    }
}
