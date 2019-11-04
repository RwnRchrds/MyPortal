using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using MyPortal.Services;

namespace MyPortal.Attributes.HttpAuthorise
{
    public class RequiresPermissionAttribute : AuthorizeAttribute
    {
        private readonly List<string> _permissions;

        public RequiresPermissionAttribute(string permissions)
        {
            _permissions = permissions.Split(',').ToList();
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return _permissions.Any(permission =>
                actionContext.ControllerContext.RequestContext.Principal.HasPermission(permission.Trim()));
        }
    }
}