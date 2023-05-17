using System;
using System.Linq;

namespace MyPortal.Logic.Models.Data.Curriculum;

public class SessionDataModel
{
    public Guid AttendanceWeekId { get; set; }
    public Guid? SessionId { get; set; }
    public Guid StudentGroupId { get; set; }
    public DateTime StartTime => Periods.Min(p => p.StartTime);
    public DateTime EndTime => Periods.Max(p => p.EndTime);
    public string ClassCode { get; set; }
    public Guid? TeacherId { get; set; }
    public string TeacherName { get; set; }
    public Guid? RoomId { get; set; }
    public string RoomName { get; set; }
    public bool IsCover { get; set; }
    public SessionPeriodDataModel[] Periods { get; set; }
}