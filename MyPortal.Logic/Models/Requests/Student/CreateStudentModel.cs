using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Requests.Admin;
using MyPortal.Logic.Models.Requests.Person;

namespace MyPortal.Logic.Models.Requests.Student
{
    public class CreateStudentModel : CreatePersonModel
    {
        public Guid RegGroupId { get; set; }

        public Guid YearGroupId { get; set; }

        public Guid? HouseId { get; set; }

        public int AdmissionNumber { get; set; }

        public DateTime? DateStarting { get; set; }

        public DateTime? DateLeaving { get; set; }

        public bool FreeSchoolMeals { get; set; }

        public Guid? SenStatusId { get; set; }

        public bool PupilPremium { get; set; }

        [StringLength(13)]
        public string Upn { get; set; }
    }
}
