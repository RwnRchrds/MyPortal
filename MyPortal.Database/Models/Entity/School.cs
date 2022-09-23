using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Schools")]
    public class School :BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid AgencyId { get; set; }

        [Column(Order = 2)]
        public Guid? LocalAuthorityId { get; set; }

        [Column(Order = 3)]
        public int EstablishmentNumber { get; set; }

        [Column(Order = 4)]
        [Required]
        [StringLength(128)]
        public string Urn { get; set; }

        [Column(Order = 5)]
        [Required]
        [StringLength(128)]
        public string Uprn { get; set; }

        [Column(Order = 6)]
        public Guid PhaseId { get; set; }
        
        [Column(Order = 7)]
        public Guid TypeId { get; set; }

        [Column(Order = 8)]
        public Guid GovernanceTypeId { get; set; }

        [Column(Order = 9)]
        public Guid IntakeTypeId { get; set; }

        [Column(Order = 10)]
        public Guid? HeadTeacherId { get; set; }

        [Column(Order = 16)]
        public bool Local { get; set; }

        public virtual Agency Agency { get; set; }
        public virtual SchoolPhase SchoolPhase { get; set; }
        public virtual SchoolType Type { get; set; }
        public virtual GovernanceType GovernanceType { get; set; }
        public virtual IntakeType IntakeType { get; set; }
        
        public virtual Person HeadTeacher { get; set; }
        public virtual LocalAuthority LocalAuthority { get; set; }
    }
}