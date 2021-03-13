using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortalWeb.Models.Requests
{
    public class TaskToggleRequestModel
    {
        public Guid TaskId { get; set; }
        public bool Completed { get; set; }
    }
}
