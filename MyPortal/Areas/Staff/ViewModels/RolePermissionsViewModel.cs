using MyPortal.Models;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class RolePermissionsViewModel
    {
        public ApplicationRole Role { get; set; }
        public RolePermission RolePermission { get; set; }
    }
}