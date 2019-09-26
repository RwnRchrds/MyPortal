using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    [Table("Communication_PhoneNumbers")]
    public class CommunicationPhoneNumber
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int PersonId { get; set; }

        public virtual CommunicationPhoneNumberType Type { get; set; }
        public virtual Person Person { get; set; }
    }
}