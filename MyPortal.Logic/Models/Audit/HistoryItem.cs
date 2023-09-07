using System;

namespace MyPortal.Logic.Models.Audit;

public class HistoryItem
{
    public Guid EntityId { get; set; }
    public string UserDisplayName { get; set; }
    public DateTime Date { get; set; }
    public string Action { get; set; }
    public string OldValue { get; set; }
}