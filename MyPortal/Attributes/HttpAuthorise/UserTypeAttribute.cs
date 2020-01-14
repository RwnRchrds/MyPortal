using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.AspNet.Identity;
using MyPortal.BusinessLogic.Models.Identity;
using MyPortal.BusinessLogic.Services.Identity;

namespace MyPortal.Attributes.HttpAuthorise
{
    public class UserTypeAttribute : AuthorizeAttribute
    {
        private readonly UserType _userType;

        public UserTypeAttribute(UserType userType)
        {
            _userType = userType;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return actionContext.ControllerContext.RequestContext.Principal.CheckUserType(_userType);
        }
    }
}