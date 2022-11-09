using System;

namespace MyPortal.Database.Models.QueryResults.Person
{
    public class PersonTypeIndicator
    {
        public Guid? UserId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? StaffId { get; set; }
        public Guid? ApplicantId { get; set; }
        public Guid? AgentId { get; set; }
    }
}
