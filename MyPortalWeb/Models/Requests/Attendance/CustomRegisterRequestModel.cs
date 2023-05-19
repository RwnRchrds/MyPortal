using System;

namespace MyPortalWeb.Models.Requests.Attendance;

public class CustomRegisterRequestModel
{
    public Guid StudentGroupId { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}