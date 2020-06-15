using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("StudentContact")]
    public class StudentContact
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid RelationshipTypeId { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public Guid ContactId { get; set; }

        [DataMember]
        public bool Correspondence { get; set; }

        [DataMember]
        public bool ParentalResponsibility { get; set; }

        [DataMember]
        public bool PupilReport { get; set; }

        [DataMember]
        public bool CourtOrder { get; set; }

        public virtual RelationshipType RelationshipType { get; set; }
        public virtual Student Student { get; set; }
        public virtual Contact Contact { get; set; }
    }
}