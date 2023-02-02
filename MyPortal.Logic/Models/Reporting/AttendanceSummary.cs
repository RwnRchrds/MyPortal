using System;
using System.Collections.Generic;
using System.Linq;
using MyPortal.Database.Constants;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Models.Data.Attendance;


namespace MyPortal.Logic.Models.Reporting
{
    public class AttendanceSummary
    {
        public int Present { get; private set; }
        public int AuthorisedAbsence { get; private set; }
        public int ApprovedEdActivity { get; private set; }
        public int UnauthorisedAbsence { get; private set; }
        public int NotRequired { get; private set; }

        public int TotalMarks => Convert.ToInt32(MathHelper.Sum(Present, AuthorisedAbsence, ApprovedEdActivity,
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
                var code = codes.FirstOrDefault(x => x.Id == mark.CodeId);

                if (code == null)
                {
                    throw new Exception($"Code not found for attendance code ID '{mark.CodeId}'.");
                }

                if (code.AttendanceCodeTypeId == AttendanceCodeTypes.Present)
                {
                    Present++;
                }
                else if (code.AttendanceCodeTypeId == AttendanceCodeTypes.ApprovedEdActivity)
                {
                    ApprovedEdActivity++;
                }
                else if (code.AttendanceCodeTypeId == AttendanceCodeTypes.AuthorisedAbsence)
                {
                    AuthorisedAbsence++;
                }
                else if (code.AttendanceCodeTypeId == AttendanceCodeTypes.UnauthorisedAbsence)
                {
                    UnauthorisedAbsence++;
                }
                else if (code.AttendanceCodeTypeId == AttendanceCodeTypes.AttendanceNotRequired)
                {
                    NotRequired++;
                }
            }
        }

        /// <summary>
        /// Gets total marks (or percentage of marks) where the student was present, late or taking part in an approved educational activity.
        /// </summary>
        public double? GetPresentAndAea(bool asPercentage = false)
        {
            if (asPercentage)
            {
                if (TotalMarks == 0)
                {
                    return null;
                }

                return MathHelper.Percent(Present, TotalMarks, 1) +
                       MathHelper.Percent(ApprovedEdActivity, TotalMarks, 1);
            }

            return Present + ApprovedEdActivity;
        }

        /// <summary>
        /// Gets attendance data for presentation in a chart.
        /// </summary>
        /// <param name="seriesTitle">A title for the range of attendance marks represented by this data.</param>
        /// <param name="asPercentage">Specifies whether the values should represent percentages of total marks (defaults to false).</param>
        /// <returns></returns>
        public ChartData<CategoricalChartDataPoint> GetChartData(string seriesTitle, bool asPercentage = false)
        {
            var series = new ChartSeries<CategoricalChartDataPoint>(seriesTitle);

            series.AddPoint(new CategoricalChartDataPoint("Present", asPercentage ? MathHelper.Percent(Present, TotalMarks, 1) : Present));
            series.AddPoint(new CategoricalChartDataPoint("Authorised Absence", asPercentage ? MathHelper.Percent(AuthorisedAbsence, TotalMarks, 1) : AuthorisedAbsence));
            series.AddPoint(new CategoricalChartDataPoint("Unauthorised Absence", UnauthorisedAbsence));
            series.AddPoint(new CategoricalChartDataPoint("Approved Educational Activity", ApprovedEdActivity));
            series.AddPoint(new CategoricalChartDataPoint("Attendance Not Required", NotRequired));

            var chart = new ChartData<CategoricalChartDataPoint>("Attendance Summary", "Type", "Marks");
            
            chart.AddSeries(series);

            return chart;
        }
    }
}