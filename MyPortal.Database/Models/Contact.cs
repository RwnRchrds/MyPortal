using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("Contact")]
    public class Contact
    {
        public Contact()
        {
            StudentContacts = new HashSet<StudentContact>();
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid PersonId { get; set; }

        [DataMember]
        public bool ParentalBallot { get; set; }

        [DataMember]
        [StringLength(256)]
        public string PlaceOfWork { get; set; }

        [DataMember]
        [StringLength(256)]
        public string JobTitle { get; set; }

        [DataMember]
        [StringLength(128)]
        public string NiNumber { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentContact> StudentContacts { get; set; }
    }
}