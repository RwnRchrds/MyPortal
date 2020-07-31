using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("EmailAddressTypes")]
    public class EmailAddressType : LookupItem
    {
        public EmailAddressType()
        {
            EmailAddresses = new HashSet<EmailAddress>();
        }

        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }
    }
}
