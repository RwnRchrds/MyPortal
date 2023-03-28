using System;

namespace MyPortal.Database.Models.Entity;

public class SessionExtraName : BaseTypes.Entity
{
    public Guid AttendanceWeekId { get; set; }
    public Guid SessionId { get; set; }
    public Guid StudentId { get; set; }

    public virtual AttendanceWeek AttendanceWeek { get; set; }
    public virtual Session Session { get; set; }
    public virtual Student Student { get; set; }
}