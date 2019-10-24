using MyPortal.Attributes;

namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A student in the system.
    /// </summary>
    
    public partial class StudentDto
    { 
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int RegGroupId { get; set; }

        public int YearGroupId { get; set; }

        public int? HouseId { get; set; }

        public string CandidateNumber { get; set; }

        public int AdmissionNumber { get; set; }

        public decimal AccountBalance { get; set; }

        public bool FreeSchoolMeals { get; set; }

        public bool GiftedAndTalented { get; set; }

        public int SenStatusId { get; set; }

        public bool PupilPremium { get; set; }

        public string MisId { get; set; }

        
        public string Upn { get; set; }

        public string Uci { get; set; }

        public bool Deleted { get; set; }

        
        

        
        

        
        

        
        

        
        

        
        

        
        

        
        

        public virtual PastoralRegGroupDto PastoralRegGroupDto { get; set; }

        public virtual PastoralYearGroupDto PastoralYearGroupDto { get; set; }

        public virtual PersonDto PersonDto { get; set; }

        public virtual SenStatusDto SenStatusDto { get; set; }

        public virtual PastoralHouseDto HouseDto { get; set; }    

        
        

        
        

        
        

        
        
    }
}
