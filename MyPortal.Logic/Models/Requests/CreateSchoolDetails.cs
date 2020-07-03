using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Requests
{
    public class CreateSchoolDetails
    {
        public string SchoolName { get; set; }
        public Guid? LocalAuthorityId { get; set; }
        public int EstablishmentNumber { get; set; }
        public string Urn { get; set; }
        public string Uprn { get; set; }
        public Guid PhaseId { get; set; }
        public Guid TypeId { get; set; }
        public Guid GovernanceTypeId { get; set; }
        public Guid IntakeTypeId { get; set; }
        public string TelephoneNo { get; set; }
        public string FaxNo { get; set; }
        public string EmailAddress { get; set; }
        public string Website { get; set; }
    }
}
