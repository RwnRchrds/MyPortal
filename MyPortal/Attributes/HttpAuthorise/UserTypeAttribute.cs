using MyPortal.Models;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.AspNet.Identity;
using MyPortal.Models.Database;
using MyPortal.Persistence;
using MyPortal.Services;

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