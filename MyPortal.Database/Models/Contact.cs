using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Contact")]
    public class Contact : IPersonEntity
    {
        public Contact()
        {
            StudentContacts = new HashSet<StudentContact>();
        }

        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid PersonId { get; set; }

        [Column(Order = 2)]
        public bool ParentalBallot { get; set; }

        [Column(Order = 3)]
        [StringLength(256)]
        public string PlaceOfWork { get; set; }

        [Column(Order = 4)]
        [StringLength(256)]
        public string JobTitle { get; set; }

        [Column(Order = 5)]
        [StringLength(128)]
        public string NiNumber { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentContact> StudentContacts { get; set; }
    }
}