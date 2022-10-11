using System;

namespace MyPortal.Logic.Models.Requests.Attendance;

public class RegisterSearchRequestModel
{
    public Guid? TeacherId { get; set; }
    public Guid? PeriodId { get; set; }
    public DateTime Date { get; set; }
}