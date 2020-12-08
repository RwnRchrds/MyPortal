using System;
using System.Collections.Generic;
using System.Linq;
using MyPortal.Database.Constants;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Reporting;

namespace MyPortal.Logic.Models.Requests.Attendance
{
    public class AttendanceSummary
    {
        public int Present { get; private set; }
        public int Late { get; private set; }
        public int AuthorisedAbsence { get; private set; }
        public int ApprovedEdActivity { get; private set; }
        public int UnauthorisedAbsence { get; private set; }
        public int NotRequired { get; private set; }

        public int TotalMarks => Convert.ToInt32(MathHelper.Sum(Present, Late, AuthorisedAbsence, ApprovedEdActivity,
            UnauthorisedAbsence, NotRequired));

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
        /// Gets total marks (or percentage of marks) where the student was present, late or taking part in an approved educational activity.
        /// </summary>
        public double GetPresentAndAea(bool asPercentage = false)
        {
            if (asPercentage)
            {
                return MathHelper.Percent(Present, TotalMarks, 1) +
                       MathHelper.Percent(ApprovedEdActivity, TotalMarks, 1) + MathHelper.Percent(Late, TotalMarks, 1);
            }

            return Present + ApprovedEdActivity + Late;
        }

        public ChartData<CategoricChartDataPoint> GetChartData(bool asPercentage = false)
        {
            var data = new List<CategoricChartDataPoint>();

            data.Add(new CategoricChartDataPoint("Present", asPercentage ? MathHelper.Percent(Present, TotalMarks, 1) : Present));
            data.Add(new CategoricChartDataPoint("Authorised Absence", asPercentage ? MathHelper.Percent(AuthorisedAbsence, TotalMarks, 1) : AuthorisedAbsence));
            data.Add(new CategoricChartDataPoint("Unauthorised Absence", UnauthorisedAbsence));
            data.Add(new CategoricChartDataPoint("Approved Educational Activity", ApprovedEdActivity));
            data.Add(new CategoricChartDataPoint("Attendance Not Required", NotRequired));
            data.Add(new CategoricChartDataPoint("Late", Late));

            return new ChartData<CategoricChartDataPoint>(data);
        }
    }
}