using System;
using System.Collections.Generic;
using System.Linq;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.QueryResults.Attendance;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Models.Response.Attendance.Register
{
    public class AttendanceRegisterModel
    {
        public AttendanceRegisterModel()
        {
            Students = new List<AttendanceRegisterStudentModel>();
            ColumnGroups = new List<AttendanceRegisterColumnGroupModel>();
            Codes = new List<AttendanceCodeModel>();
        }

        public string Title { get; set; }

        public ICollection<AttendanceCodeModel> Codes { get; set; }
        public ICollection<AttendanceRegisterColumnGroupModel> ColumnGroups { get; set; }
        public ICollection<AttendanceRegisterStudentModel> Students { get; set; }

        public void PopulateColumnGroups(IEnumerable<PossibleAttendancePeriod> periodCollection, Guid? lockToPeriodId)
        {
            var dates =
                (periodCollection).GroupBy(p =>
                    p.ActualStartTime.Date).OrderBy(d => d.Key).ToArray();

            for (int i = 0; i < dates.Length; i++)
            {
                var date = dates[i];
                var periods = date.ToArray();

                var columnGroup = new AttendanceRegisterColumnGroupModel
                {
                    Header = date.Key.ToString("dd/MM/yyyy"),
                    Order = i
                };

                for (int j = 0; j < periods.Length; j++)
                {
                    var period = periods[j];

                    var column = new AttendanceRegisterColumnModel
                    {
                        Header = period.Name,
                        Order = j,
                        AttendancePeriodId = period.PeriodId,
                        AttendanceWeekId = period.AttendanceWeekId,
                        IsReadOnly = lockToPeriodId.HasValue && lockToPeriodId != period.PeriodId
                    };

                    columnGroup.Columns.Add(column);
                }

                ColumnGroups.Add(columnGroup);
            }
        }

        public void PopulateMarks(IEnumerable<AttendanceMarkDetailModel> markCollection)
        {
            var data = new List<AttendanceRegisterStudentModel>();

            var students = markCollection.GroupBy(c => c.StudentId).ToArray();

            foreach (var student in students)
            {
                var studentData = student.ToArray();

                var dataRow = new AttendanceRegisterStudentModel();

                dataRow.StudentId = student.Key;

                for (int i = 0; i < studentData.Length; i++)
                {
                    var mark = studentData[i];

                    if (i == 0)
                    {
                        dataRow.StudentName = mark.StudentName;
                    }

                    dataRow.Marks.Add(new AttendanceMarkSummaryModel
                    {
                        StudentId = mark.StudentId,
                        WeekId = mark.WeekId,
                        PeriodId = mark.PeriodId,
                        CodeId = mark.CodeId,
                        MinutesLate = mark.MinutesLate,
                        Comments = mark.Comments
                    });
                }

                data.Add(dataRow);
            }

            Students = data.OrderBy(d => d.StudentName).ToArray();
        }

        // Inserts blank marks for marks that should be recorded.
        // We need this because not all students will have a lesson on a given period (e.g. after school lessons)
        // This is therefore used to distinguish students that *should* have marks from those who should not
        public void PopulateMissingMarks(IEnumerable<PossibleAttendanceMark> requiredMarks)
        {
            var students = requiredMarks.GroupBy(m => m.StudentId).ToArray();

            foreach (var student in students)
            {
                var studentData = student.ToArray();

                var dataRow = Students.FirstOrDefault(x => x.StudentId == student.Key);

                if (dataRow != null)
                {
                    var missingMarks = studentData.Where(d => !dataRow.Marks.Any(m =>
                        m.WeekId == d.AttendanceWeekId && m.PeriodId == d.PeriodId)).ToArray();

                    foreach (var missingMark in missingMarks)
                    {
                        dataRow.Marks.Add(new AttendanceMarkSummaryModel
                        {
                            StudentId = student.Key,
                            WeekId = missingMark.AttendanceWeekId,
                            PeriodId = missingMark.PeriodId
                        });
                    }
                }
            }
        }
    }
}
