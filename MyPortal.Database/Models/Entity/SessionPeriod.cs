using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity;

[Table("SessionPeriods")]
public class SessionPeriod : BaseTypes.Entity
{
    public Guid SessionId { get; set; }
    public Guid PeriodId { get; set; }

    public virtual Session Session { get; set; }
    public virtual AttendancePeriod Period { get; set; }
}