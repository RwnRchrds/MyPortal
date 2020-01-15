using MyPortal.BusinessLogic.Models.Identity;

namespace MyPortal.Areas.Staff.ViewModels.Admin
{
    public class RolePermissionsViewModel
    {
        public ApplicationRole Role { get; set; }
        public RolePermission RolePermission { get; set; }
    }
}