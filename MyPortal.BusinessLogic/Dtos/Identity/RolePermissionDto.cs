namespace MyPortal.BusinessLogic.Dtos.Identity
{
    public class RolePermissionDto
    {
        public int Id { get; set; }

        public string RoleId { get; set; }

        public int PermissionId { get; set; }
        
        public virtual PermissionDto Permission { get; set; }
        public virtual ApplicationRoleDto Role { get; set; }
    }
}