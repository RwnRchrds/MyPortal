using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    [Table("People_Contacts")]
    public class Contact
    {
        public Contact()
        {
            StudentContacts = new HashSet<StudentContact>();
        }

        public int Id { get; set; }

        public int PersonId { get; set; }

        public bool ParentalBallot { get; set; }

        public string PlaceOfWork { get; set; }

        public string JobTitle { get; set; }

        [StringLength(128)]
        public string NiNumber { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentContact> StudentContacts { get; set; }
    }
}