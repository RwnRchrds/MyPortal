using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyPortalWeb.Models.Requests.Attendance;

public class SessionRegisterRequestModel
{
    [BindRequired] public Guid AttendanceWeekId { get; set; }

    [BindRequired] public Guid SessionId { get; set; }

    [BindRequired] public Guid PeriodId { get; set; }
}