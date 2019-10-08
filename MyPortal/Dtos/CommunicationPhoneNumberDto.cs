using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Dtos
{
    
    public class CommunicationPhoneNumberDto
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int PersonId { get; set; }

        
        public string Number { get; set; }  

        public virtual CommunicationPhoneNumberTypeDto Type { get; set; }
        public virtual PersonDto Person { get; set; }
    }
}