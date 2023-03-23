using System;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Enums;

namespace MyPortal.Logic.Models.Data.People;

public class ReminderDataModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid? EntityId { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid ReminderType { get; set; }
}