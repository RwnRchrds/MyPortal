using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    [Table("Communication_AddressPersons")]
    public class CommunicationAddressPerson
    {
        public int Id { get; set; }

        public int AddressId { get; set; }

        public int PersonId { get; set; }

        public virtual CommunicationAddress Address { get; set; }
        public virtual Person Person { get; set; }
    }
}