using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Requests.Admin.Users
{
    public class RoleEditModel
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
