using System;
using MyPortal.Logic.Enums;

namespace MyPortal.Logic.Models.Requests.Calendar;

public class RecurringRequestModel
{
    public EventFrequency Frequency { get; set; }
    public DayOfWeek[] Days { get; set; }
    public DateTime? LastOccurrence { get; set; }
}