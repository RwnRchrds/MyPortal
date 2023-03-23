using System;

namespace MyPortal.Logic.Models.Requests.Person.Tasks;

public class TaskReminderRequestModel
{
    public Guid TaskId { get; set; }
    
    public Guid UserId { get; set; }
    
    public DateTime RemindTime { get; set; }
}