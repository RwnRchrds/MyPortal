using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("School")]
    public class School
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [DataMember]
        public Guid? LocalAuthorityId { get; set; }

        [DataMember]
        public int EstablishmentNumber { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Urn { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Uprn { get; set; }

        [DataMember]
        public Guid PhaseId { get; set; }
        
        [DataMember]
        public Guid TypeId { get; set; }

        [DataMember]
        public Guid GovernanceTypeId { get; set; }

        [DataMember]
        public Guid IntakeTypeId { get; set; }

        [DataMember]
        public Guid? HeadTeacherId { get; set; }

        [DataMember]
        [Phone]
        [StringLength(128)]
        public string TelephoneNo { get; set; }

        [DataMember]
        [Phone]
        [StringLength(128)]
        public string FaxNo { get; set; }

        [DataMember]
        [EmailAddress]
        [StringLength(128)]
        public string EmailAddress { get; set; }

        [DataMember]
        [Url]
        [StringLength(128)]
        public string Website { get; set; }

        [DataMember]
        public bool Local { get; set; }

        public virtual Phase Phase { get; set; }
        public virtual SchoolType Type { get; set; }
        public virtual GovernanceType GovernanceType { get; set; }
        public virtual IntakeType IntakeType { get; set; }
        public virtual Person HeadTeacher { get; set; }
        public virtual LocalAuthority LocalAuthority { get; set; }
    }
}