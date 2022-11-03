using System;

namespace MyPortal.Database.Models.QueryResults.Attendance;

public class PossibleAttendanceMark
{
    public Guid StudentId { get; set; }
    public Guid AttendanceWeekId { get; set; }
    public Guid PeriodId { get; set; }
    public bool Exists { get; set; }
}