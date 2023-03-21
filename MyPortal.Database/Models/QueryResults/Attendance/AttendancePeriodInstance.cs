using System;

namespace MyPortal.Database.Models.QueryResults.Attendance;

public class AttendancePeriodInstance
{
    public DateTime ActualStartTime { get; set; }
    public DateTime ActualEndTime { get; set; }
    public Guid PeriodId { get; set; }
    public Guid AttendanceWeekId { get; set; }
    public Guid WeekPatternId { get; set; }
    public DayOfWeek Weekday { get; set; }
    public string Name { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool AmReg { get; set; }
    public bool PmReg { get; set; }
}