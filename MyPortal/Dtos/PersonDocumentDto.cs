using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Dtos
{
    /// <summary>
    /// A document assigned to a person.
    /// </summary>
    
    public class PersonDocumentDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int DocumentId { get; set; }

        public virtual DocumentDto Document { get; set; }

        public virtual PersonDto Person { get; set; }
    }
}