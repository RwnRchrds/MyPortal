using System;

namespace MyPortal.Logic.Models.Data.People;

public class ReminderDataModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid? EntityId { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid ReminderType { get; set; }
}