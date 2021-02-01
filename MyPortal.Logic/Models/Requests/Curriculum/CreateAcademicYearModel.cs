using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Logic.Models.Requests.Curriculum
{
    public class CreateAcademicYearModel
    {
        public string Name { get; set; }

        public CreateAcademicTermModel[] AcademicTerms { get; set; }
        public CreateAttendancePlanModel AttendancePlan { get; set; }
    }
}
