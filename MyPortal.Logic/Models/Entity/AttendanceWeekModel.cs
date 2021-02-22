using System;

namespace MyPortal.Logic.Models.Entity
{
    public class AttendanceWeekModel
    {
        public Guid Id { get; set; }

        public Guid WeekPatternId { get; set; }

        public Guid AcademicTermId { get; set; }

        public DateTime Beginning { get; set; }

        public bool IsNonTimetable { get; set; }

        public virtual AttendanceWeekPatternModel WeekPattern { get; set; }
        public virtual AcademicTermModel AcademicTerm { get; set; }
    }
}
