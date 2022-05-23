using System;

namespace MyPortal.Logic.Models.Requests.Admin.Roles
{
    public class UpdateRoleRequestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] PermissionValues { get; set; }
    }
}
