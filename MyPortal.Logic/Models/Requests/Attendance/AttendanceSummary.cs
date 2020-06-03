using System;
using System.Collections.Generic;
using System.Linq;
using MyPortal.Database.Models;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Requests.Reporting;

namespace MyPortal.Logic.Models.Requests.Attendance
{
    public class AttendanceSummary
    {
        public double Present { get; private set; }
        public double Late { get; private set; }
        public double AuthorisedAbsence { get; private set; }
        public double ApprovedEdActivity { get; private set; }
        public double UnauthorisedAbsence { get; private set; }
        public double NotRequired { get; private set; }
        public bool IsPercentage { get; private set; }


        /// <summary>
        /// Indicates whether there are any attendance marks for the attendance summary to be considered valid.
        /// </summary>
        public bool Valid
        {
            get
            {
                return GetTotalMarks() != 0;
            }
        }

        /// <summary>
        /// A summary of a collection of attendance marks.
        /// </summary>
        /// <param name="codes">Attendance codes to use as a reference.</param>
        /// <param name="marks">Attendance marks to obtain statistics for.</param>
        public AttendanceSummary(List<AttendanceCodeModel> codes, List<AttendanceMarkModel> marks)
        {
            foreach (var mark in marks)
            {
                var code = codes.FirstOrDefault(x => x.Code == mark.Mark);

                if (code == null)
                {
                    throw new Exception($"Code not found for attendance mark '{mark.Mark}'.");
                }

                if (code.Id == AttendanceMeanings.Present)
                {
                    Present++;
                }
                else if (code.Id == AttendanceMeanings.ApprovedEdActivity)
                {
                    ApprovedEdActivity++;
                }
                else if (code.Id == AttendanceMeanings.AuthorisedAbsence)
                {
                    AuthorisedAbsence++;
                }
                else if (code.Id == AttendanceMeanings.UnauthorisedAbsence)
                {
                    UnauthorisedAbsence++;
                }
                else if (code.Id == AttendanceMeanings.AttendanceNotRequired)
                {
                    NotRequired++;
                }
                else if (code.Id == AttendanceMeanings.Late)
                {
                    Late++;
                }
            }
        }

        /// <summary>
        /// Gets the total number of marks represented by the attendance summary.
        /// </summary>
        /// <returns></returns>
        private int GetTotalMarks()
        {
            if (!IsPercentage)
            {
                return (int)(Present + AuthorisedAbsence + ApprovedEdActivity + UnauthorisedAbsence + NotRequired + Late);
            }

            throw new Exception("Cannot get total marks from a percentage.");
        }

        /// <summary>
        /// Gets total marks (or percentage of marks) where the student was present, late or taking part in an approved educational activity.
        /// </summary>
        public double GetPresentAndApproved()
        {
            return Present + ApprovedEdActivity + Late;
        }

        /// <summary>
        /// Converts the attendance summary values to a percentage of total marks.
        /// </summary>
        public void ConvertToPercentage()
        {
            if (!IsPercentage)
            {
                var totalMarks = GetTotalMarks();

                if (totalMarks > 0)
                {
                    Present = Math.Round((Present / totalMarks) * 100, 1);
                    AuthorisedAbsence = Math.Round((AuthorisedAbsence / totalMarks) * 100, 1);
                    ApprovedEdActivity = Math.Round((ApprovedEdActivity / totalMarks) * 100, 1);
                    UnauthorisedAbsence = Math.Round((UnauthorisedAbsence / totalMarks) * 100, 1);
                    NotRequired = Math.Round((NotRequired / totalMarks) * 100, 1);
                    Late = Math.Round((Late / totalMarks) * 100, 1);

                    IsPercentage = true;
                }
            }
        }

        public ChartDataCategoricPoint[] GetChartData()
        {
            var data = new List<ChartDataCategoricPoint>();

            data.Add(new ChartDataCategoricPoint("Present", Present));
            data.Add(new ChartDataCategoricPoint("Authorised Absence", AuthorisedAbsence));
            data.Add(new ChartDataCategoricPoint("Unauthorised Absence", UnauthorisedAbsence));
            data.Add(new ChartDataCategoricPoint("Approved Educational Activity", ApprovedEdActivity));
            data.Add(new ChartDataCategoricPoint("Attendance Not Required", NotRequired));
            data.Add(new ChartDataCategoricPoint("Late", Late));

            return data.ToArray();
        }
    }
}