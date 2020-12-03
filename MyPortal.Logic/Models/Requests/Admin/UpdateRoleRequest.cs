using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Requests.Admin
{
    public class UpdateRoleRequest : CreateRoleRequest
    {
        public Guid Id { get; set; }
    }
}
