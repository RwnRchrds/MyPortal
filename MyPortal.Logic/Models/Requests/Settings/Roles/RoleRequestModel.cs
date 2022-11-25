using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Settings.Roles
{
    public class RoleRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int[] Permissions { get; set; }
    }
}
