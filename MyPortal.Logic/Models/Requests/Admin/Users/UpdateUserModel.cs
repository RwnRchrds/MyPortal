using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Requests.Admin.Users
{
    public class UpdateUserModel
    {
        public Guid Id { get; set; }
        public Guid? PersonId { get; set; }
        public Guid[] RoleIds { get; set; }
    }
}
