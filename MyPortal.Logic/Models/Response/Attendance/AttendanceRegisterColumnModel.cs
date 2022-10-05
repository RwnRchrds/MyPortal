using System;

namespace MyPortal.Logic.Models.Response.Attendance;

public class AttendanceRegisterColumnModel
{
    public Guid AttendancePeriodId { get; set; }
    public Guid AttendanceWeekId { get; set; }
    public string ColumnName { get; set; }
    public int ColumnOrder { get; set; }
}