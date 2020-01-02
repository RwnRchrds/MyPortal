using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Data.Interfaces;

namespace MyPortal.Data.Models
{
    [Table("Contact", Schema = "person")]
    public class Contact : IPerson
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