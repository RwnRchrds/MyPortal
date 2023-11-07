using System;

namespace MyPortalWeb.Models.Requests
{
    public class TaskToggleRequestModel
    {
        public Guid TaskId { get; set; }
        public bool Completed { get; set; }
    }
}