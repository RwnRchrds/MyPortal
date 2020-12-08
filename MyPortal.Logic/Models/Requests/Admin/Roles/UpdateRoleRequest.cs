using System;

namespace MyPortal.Logic.Models.Requests.Admin.Roles
{
    public class UpdateRoleRequest : CreateRoleRequest
    {
        public Guid Id { get; set; }
    }
}
