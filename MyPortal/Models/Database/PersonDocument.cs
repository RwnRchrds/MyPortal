using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    /// <summary>
    /// A document assigned to a person.
    /// </summary>
    [Table("Docs_PersonDocuments")]
    public class PersonDocument
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int DocumentId { get; set; }

        public virtual Document Document { get; set; }

        public virtual Person Person { get; set; }
    }
}