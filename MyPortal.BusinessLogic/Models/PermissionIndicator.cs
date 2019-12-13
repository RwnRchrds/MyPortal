using MyPortal.BusinessLogic.Dtos.Identity;

namespace MyPortal.BusinessLogic.Models
{
    public class PermissionIndicator
    {
        public PermissionDto Permission { get; set; }
        public bool HasPermission { get; set; }
    }
}