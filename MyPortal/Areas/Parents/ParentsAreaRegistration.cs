using System.Web.Mvc;

namespace MyPortal.Areas.Parents
{
    public class ParentsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Parents";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Parents_default",
                "Parents/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] {"MyPortal.Areas.Parents.Controllers"}
            );
        }
    }
}