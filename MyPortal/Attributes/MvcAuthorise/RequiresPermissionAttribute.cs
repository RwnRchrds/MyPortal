using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPortal.Services;

namespace MyPortal.Attributes.MvcAuthorise
{
    public class RequiresPermissionAttribute : AuthorizeAttribute
    {
        private readonly List<string> _permissions;
        
        public RequiresPermissionAttribute(string permissions)
        {
            _permissions = permissions.Split(',').ToList();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.User != null && _permissions.Any(permission =>
                httpContext.User.HasPermission(permission.Trim()));
        }
    }
}