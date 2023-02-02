using System;

namespace MyPortal.Logic.Models.Structures;

public class WeeklyPatternModel
{
    public WeeklyPatternModel(DayOfWeek[] days, DateTime endDate)
    {
        Days = days;
        EndDate = endDate;
    }

    public DayOfWeek[] Days { get; set; }
    public DateTime EndDate { get; set; }
}