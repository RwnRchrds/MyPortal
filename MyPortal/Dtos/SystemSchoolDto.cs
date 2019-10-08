using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    
    public class SystemSchoolDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? LocalAuthorityId { get; set; }

        public int EstablishmentNumber { get; set; }

        public string Urn { get; set; }

        public string Uprn { get; set; }

        public int PhaseId { get; set; }

        public int TypeId { get; set; }

        public int GovernanceTypeId { get; set; }

        public int IntakeTypeId { get; set; }

        public int? HeadTeacherId { get; set; }

        
        public string TelephoneNo { get; set; }

        
        public string FaxNo { get; set; }

        
        public string EmailAddress { get; set; }

        
        public string Website { get; set; }

        public bool Local { get; set; }

        public virtual SchoolPhaseDto Phase { get; set; }
        public virtual SchoolTypeDto Type { get; set; }
        public virtual SchoolGovernanceTypeDto GovernanceType { get; set; }
        public virtual SchoolIntakeTypeDto IntakeType { get; set; }
        public virtual PersonDto HeadTeacher { get; set; }
    }
}