using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Models.Requests.Admin;
using MyPortal.Logic.Models.Requests.Person;

namespace MyPortal.Logic.Models.Requests.Student
{
    public class StudentRequestModel : PersonRequestModel
    {
        public Guid? HouseId { get; set; }
        
        [NotEmpty]
        public Guid YearGroupId { get; set; }
        
        [NotEmpty]
        public Guid RegGroupId { get; set; }

        public DateTime? DateStarting { get; set; }
        
        public DateTime? DateLeaving { get; set; }

        [Upn]
        public string Upn { get; set; }

        public Guid? SenStatusId { get; set; }
        
        public Guid? SenTypeId { get; set; }
        
        public Guid? EnrolmentStatusId { get; set; }
        
        public Guid? BoarderStatusId { get; set; }
        
        public bool PupilPremium { get; set; }
    }
}
