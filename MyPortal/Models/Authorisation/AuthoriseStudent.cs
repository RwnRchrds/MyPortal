using System.Web.Http;
using System.Web.Http.Controllers;

namespace MyPortal.Models.Authorisation
{
    public class AuthoriseStudent : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }
    }
}