using System;
using MyPortal.Database.Enums;

namespace MyPortalWeb.Attributes
{
    public enum PermissionRequirement
    {
        RequireAny,
        RequireAll
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class PermissionAttribute : Attribute
    {
        public PermissionValue[] Permissions;
        public PermissionRequirement Requirement;

        public PermissionAttribute(PermissionRequirement requirement, params PermissionValue[] permissions)
        {
            Permissions = permissions;
            Requirement = requirement;
        }

        public PermissionAttribute(PermissionValue permissionValue)
        {
            Permissions = new[] { permissionValue };
            Requirement = PermissionRequirement.RequireAll;
        }
    }
}