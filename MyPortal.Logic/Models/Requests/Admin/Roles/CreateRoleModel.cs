using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Admin.Roles
{
    public class CreateRoleModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public Guid[] PermissionIds { get; set; }
    }
}
