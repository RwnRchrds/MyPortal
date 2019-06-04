using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Misc
{
    public class AttendanceSummary
    {
        public int StudentId { get; set; }
        public double Present { get; set; }
        public double AuthorisedAbsence { get; set; }
        public double ApprovedEdActivity { get; set; }
        public double UnauthorisedAbsence { get; set; }
        public double NotRequired { get; set; }
        public double Late { get; set; }
        public bool IsPercentage { get; set; }

        public AttendanceSummary()
        {
            StudentId = 0;
            Present = 0;
            AuthorisedAbsence = 0;
            ApprovedEdActivity = 0;
            UnauthorisedAbsence = 0;
            NotRequired = 0;
            Late = 0;
            IsPercentage = false;
        }

        public void ConvertToPercentage()
        {
            if (!IsPercentage)
            {
                var totalMarks = Present + AuthorisedAbsence + ApprovedEdActivity + UnauthorisedAbsence + NotRequired +
                                 Late;

                Present = (Present / totalMarks) * 100;
                AuthorisedAbsence = (AuthorisedAbsence / totalMarks) * 100;
                ApprovedEdActivity = (ApprovedEdActivity / totalMarks) * 100;
                UnauthorisedAbsence = (UnauthorisedAbsence / totalMarks) * 100;
                NotRequired = (NotRequired / totalMarks) * 100;
                Late = (Late / totalMarks) * 100;

                IsPercentage = true;
            }
        }
    }
}