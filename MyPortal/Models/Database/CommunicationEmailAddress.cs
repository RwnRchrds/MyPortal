using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    public enum EmailAddressType
    {
        Home,
        Work,
        Other
    }

    [Table("Communication_EmailAddresses")]
    public class CommunicationEmailAddress
    {
        public int Id { get; set; }

        public EmailAddressType Type { get; set; }

        public int PersonId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(128)]
        public string Address { get; set; }

        public bool Main { get; set; }
        public bool Primary { get; set; }
        public string Notes { get; set; }

        public virtual Person Person { get; set; }
    }
}