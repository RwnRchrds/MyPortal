using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyPortalWeb.Models.Requests.Attendance;

public class StudentGroupRegisterRequestModel
{
    [BindRequired]
    public Guid StudentGroupId { get; set; }
    
    [BindRequired]
    public Guid AttendanceWeekId { get; set; }
    
    [BindRequired]
    public Guid PeriodId { get; set; }
}