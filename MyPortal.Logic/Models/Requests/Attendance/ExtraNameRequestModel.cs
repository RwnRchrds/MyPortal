using System;

namespace MyPortal.Logic.Models.Requests.Attendance;

public class ExtraNameRequestModel
{
    public Guid AttendanceWeekId { get; set; }
    public Guid SessionId { get; set; }
    public Guid StudentId { get; set; }
}