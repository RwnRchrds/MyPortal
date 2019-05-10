using System.Runtime.Serialization;

namespace MyPortal.Dtos
{
    [DataContract(Namespace = "")]
    public class StudentDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public int RegGroupId { get; set; }

        [DataMember]
        public int YearGroupId { get; set; }

        [DataMember]
        public string CandidateNumber { get; set; }

        [DataMember]
        public decimal AccountBalance { get; set; }

        [DataMember]
        public string MisId { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public PastoralRegGroupDto PastoralRegGroup { get; set; }

        [DataMember]
        public PastoralYearGroupDto PastoralYearGroup { get; set; }
    }
}