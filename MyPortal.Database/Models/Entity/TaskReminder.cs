using System;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity;

[Table("TaskReminders")]
public class TaskReminder : BaseTypes.Entity, IReminder
{
    [Column(Order = 2)]
    public Guid TaskId { get; set; }

    [Column(Order = 3)]
    public Guid UserId { get; set; }
    
    [Column(Order = 4)]
    public DateTime RemindTime { get; set; }

    public virtual Task Task { get; set; }
}