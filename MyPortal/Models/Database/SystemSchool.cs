using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    [Table("System_Schools")]
    public class SystemSchool
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public int? LocalAuthorityId { get; set; }

        public int EstablishmentNumber { get; set; }

        [Required]
        [StringLength(128)]
        public string Urn { get; set; }

        [Required]
        [StringLength(128)]
        public string Uprn { get; set; }

        public int PhaseId { get; set; }

        public int TypeId { get; set; }

        public int GovernanceTypeId { get; set; }

        public int IntakeTypeId { get; set; }

        public int? HeadTeacherId { get; set; }

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

        public virtual SchoolPhase Phase { get; set; }
        public virtual SchoolType Type { get; set; }
        public virtual SchoolGovernanceType GovernanceType { get; set; }
        public virtual SchoolIntakeType IntakeType { get; set; }
        public virtual Person HeadTeacher { get; set; }
    }
}