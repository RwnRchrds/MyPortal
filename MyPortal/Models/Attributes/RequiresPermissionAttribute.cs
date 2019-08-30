using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using MyPortal.Processes;

namespace MyPortal.Models.Attributes
{
    public class RequiresPermissionAttribute : AuthorizeAttribute
    {
        private List<string> _permissions;

        public RequiresPermissionAttribute(string permissions)
        {
            _permissions = permissions.Split(',').ToList();
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            foreach (var permission in _permissions)
            {
                if (actionContext.ControllerContext.RequestContext.Principal.HasPermission(permission.Trim()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}