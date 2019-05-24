using System.Runtime.Serialization;

namespace MyPortal.Dtos
{    
    public class StudentDto
    {        
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Gender { get; set; }
        
        public string Email { get; set; }
        
        public int RegGroupId { get; set; }
        
        public int YearGroupId { get; set; }
        
        public string CandidateNumber { get; set; }
        
        public decimal AccountBalance { get; set; }

        public bool FreeSchoolMeals { get; set; }

        public bool PupilPremium { get; set; }

        public bool GiftedAndTalented { get; set; }

        public int SenStatusId { get; set; }
        
        public string MisId { get; set; }
        
        public string UserId { get; set; }
        
        public PastoralRegGroupDto PastoralRegGroup { get; set; }
        
        public PastoralYearGroupDto PastoralYearGroup { get; set; }

        public SenStatusDto SenStatus { get; set; }
    }
}