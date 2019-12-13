using System.Web;
using System.Web.Mvc;
using MyPortal.BusinessLogic.Models.Identity;
using MyPortal.BusinessLogic.Services;

namespace MyPortal.Attributes.MvcAuthorise
{
    public class UserTypeAttribute : AuthorizeAttribute
    {
        private readonly UserType _userType;
        
        public UserTypeAttribute(UserType userType)
        {
            _userType = userType;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.User != null && httpContext.User.CheckUserType(_userType);
        }
    }
}