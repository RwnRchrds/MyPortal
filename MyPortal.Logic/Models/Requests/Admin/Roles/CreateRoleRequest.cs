using System;

namespace MyPortal.Logic.Models.Requests.Admin.Roles
{
    public class CreateRoleRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid[] PermissionIds { get; set; }
    }
}
