using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MyPortal.Logic.Models.Data.Curriculum;

public class SessionPeriodDataModel
{
    public Guid PeriodId { get; set; }
    public string PeriodName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}