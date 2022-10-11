using System;
using MyPortal.Database.Models.QueryResults.Attendance;

namespace MyPortal.Logic.Models.Response.Attendance.Register;

public class AttendanceRegisterColumnModel
{
    public Guid AttendancePeriodId { get; set; }
    public Guid AttendanceWeekId { get; set; }
    public string Header { get; set; }
    public int Order { get; set; }
    public bool IsReadOnly{ get; set; }
}