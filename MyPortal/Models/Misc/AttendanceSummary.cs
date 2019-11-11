﻿using System;
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
        public bool IsPercentage { get; set; }

        public AttendanceSummary()
        {
            StudentId = 0;
            Present = 0;
            AuthorisedAbsence = 0;
            ApprovedEdActivity = 0;
            UnauthorisedAbsence = 0;
            NotRequired = 0;
            IsPercentage = false;
        }

        public void ConvertToPercentage()
        {
            if (!IsPercentage)
            {
                var totalMarks = Present + AuthorisedAbsence + ApprovedEdActivity + UnauthorisedAbsence + NotRequired;

                var present = (Present / totalMarks) * 100;
                var authorisedAbsence = (AuthorisedAbsence / totalMarks) * 100;
                var approvedEdActivity = (ApprovedEdActivity / totalMarks) * 100;
                var unauthorisedAbsence = (UnauthorisedAbsence / totalMarks) * 100;
                var notRequired = (NotRequired / totalMarks) * 100;

                Present = Math.Round(present, 1);
                AuthorisedAbsence = Math.Round(authorisedAbsence, 1);
                ApprovedEdActivity = Math.Round(approvedEdActivity, 1);
                UnauthorisedAbsence = Math.Round(unauthorisedAbsence, 1);
                NotRequired = Math.Round(notRequired, 1);

                IsPercentage = true;
            }
        }
    }
}