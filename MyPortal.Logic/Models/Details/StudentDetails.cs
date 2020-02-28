using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Models;

namespace MyPortal.Logic.Models.Details
{
    public class StudentDetails
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Guid RegGroupId { get; set; }

        public Guid YearGroupId { get; set; }

        public Guid? HouseId { get; set; }

        [StringLength(128)]
        public string CandidateNumber { get; set; }

        public int AdmissionNumber { get; set; }
        
        public DateTime? DateStarting { get; set; }
        
        public DateTime? DateLeaving { get; set; }
        
        public decimal AccountBalance { get; set; }

        public bool FreeSchoolMeals { get; set; }

        public bool GiftedAndTalented { get; set; }

        public Guid? SenStatusId { get; set; }

        public bool PupilPremium { get; set; }

        [StringLength(13)]
        public string Upn { get; set; }

        public string Uci { get; set; }

        public bool Deleted { get; set; }

        public virtual RegGroupDetails RegGroup { get; set; }

        public virtual YearGroupDetails YearGroup { get; set; }

        public virtual PersonDetails Person { get; set; }

        public virtual SenStatusDetails SenStatus { get; set; }

        public virtual HouseDetails House { get; set; }
    }
}