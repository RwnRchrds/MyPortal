using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity;

[Table("UserReminderSettings")]
public class UserReminderSetting : BaseTypes.Entity
{
    [Column(Order = 2)] public Guid UserId { get; set; }

    [Column(Order = 3)] public Guid ReminderType { get; set; }

    [Column(Order = 4)] public TimeSpan RemindBefore { get; set; }

    public virtual User User { get; set; }
}