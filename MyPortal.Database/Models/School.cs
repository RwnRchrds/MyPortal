using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("School")]
    public class School
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public Guid? LocalAuthorityId { get; set; }

        public int EstablishmentNumber { get; set; }

        [Required]
        [StringLength(128)]
        public string Urn { get; set; }

        [Required]
        [StringLength(128)]
        public string Uprn { get; set; }

        public Guid PhaseId { get; set; }

        public Guid TypeId { get; set; }

        public Guid GovernanceTypeId { get; set; }

        public Guid IntakeTypeId { get; set; }

        public Guid? HeadTeacherId { get; set; }

        [Phone]
        [StringLength(128)]
        public string TelephoneNo { get; set; }

        [Phone]
        [StringLength(128)]
        public string FaxNo { get; set; }

        [EmailAddress]
        [StringLength(128)]
        public string EmailAddress { get; set; }

        [Url]
        [StringLength(128)]
        public string Website { get; set; }

        public bool Local { get; set; }

        public virtual Phase Phase { get; set; }
        public virtual SchoolType Type { get; set; }
        public virtual GovernanceType GovernanceType { get; set; }
        public virtual IntakeType IntakeType { get; set; }
        public virtual Person HeadTeacher { get; set; }
        public virtual LocalAuthority LocalAuthority { get; set; }
    }
}