using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class SchoolModel : BaseModel
    {
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

        public virtual SchoolPhaseModel SchoolPhase { get; set; }
        public virtual SchoolTypeModel Type { get; set; }
        public virtual GovernanceTypeModel GovernanceType { get; set; }
        public virtual IntakeTypeModel IntakeType { get; set; }
        public virtual PersonModel HeadTeacher { get; set; }
        public virtual LocalAuthorityModel LocalAuthority { get; set; }
    }
}