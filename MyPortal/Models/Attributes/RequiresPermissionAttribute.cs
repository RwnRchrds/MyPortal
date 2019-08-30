using System.Web.Http;
using System.Web.Http.Controllers;
using MyPortal.Processes;

namespace MyPortal.Models.Attributes
{
    public class RequiresPermissionAttribute : AuthorizeAttribute
    {
        private string _permission;

        public RequiresPermissionAttribute(string permission)
        {
            _permission = permission;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return actionContext.ControllerContext.RequestContext.Principal.HasPermission(_permission);
        }
    }
}