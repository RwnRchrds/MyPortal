using System;

namespace MyPortal.Logic.Models.Permissions;

public class TaskAccessModel
{
    public Guid PersonId { get; set; }
    public bool IsAssignee { get; set; }
    public bool CanView { get; set; }
    public bool CanEdit { get; set; }
}