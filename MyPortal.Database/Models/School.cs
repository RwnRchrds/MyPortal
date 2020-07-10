using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("School")]
    public class School :IEntity
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

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

        [Column(Order = 11)]
        [Phone]
        [StringLength(128)]
        public string TelephoneNo { get; set; }

        [Column(Order = 13)]
        [Phone]
        [StringLength(128)]
        public string FaxNo { get; set; }

        [Column(Order = 14)]
        [EmailAddress]
        [StringLength(128)]
        public string EmailAddress { get; set; }

        [Column(Order = 15)]
        [Url]
        [StringLength(128)]
        public string Website { get; set; }

        [Column(Order = 16)]
        public bool Local { get; set; }

        public virtual Phase Phase { get; set; }
        public virtual SchoolType Type { get; set; }
        public virtual GovernanceType GovernanceType { get; set; }
        public virtual IntakeType IntakeType { get; set; }
        public virtual Person HeadTeacher { get; set; }
        public virtual LocalAuthority LocalAuthority { get; set; }
    }
}