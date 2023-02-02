using System;

namespace MyPortal.Logic.Models.Data.Attendance.Register;

public class AttendanceRegisterColumnDataModel
{
    public Guid AttendancePeriodId { get; set; }
    public Guid AttendanceWeekId { get; set; }
    public string Header { get; set; }
    public int Order { get; set; }
    public bool IsReadOnly{ get; set; }
}