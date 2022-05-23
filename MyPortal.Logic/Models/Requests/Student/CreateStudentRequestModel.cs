using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Requests.Admin;
using MyPortal.Logic.Models.Requests.Person;

namespace MyPortal.Logic.Models.Requests.Student
{
    public class CreateStudentRequestModel : CreatePersonRequestModel
    {
        public Guid? HouseId { get; set; }
        
        public Guid YearGroupId { get; set; }
        
        public Guid RegGroupId { get; set; }

        public DateTime? DateStarting { get; set; }

        public Guid? SenStatusId { get; set; }
        
        public Guid? SenTypeId { get; set; }
        
        public Guid? EnrolmentStatusId { get; set; }
        
        public Guid? BoarderStatusId { get; set; }
        
        public bool PupilPremium { get; set; }
    }
}
