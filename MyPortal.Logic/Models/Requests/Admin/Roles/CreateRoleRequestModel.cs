using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Admin.Roles
{
    public class CreateRoleRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int[] Permissions { get; set; }
    }
}
