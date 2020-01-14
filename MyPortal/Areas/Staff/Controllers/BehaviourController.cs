using System.Web.Mvc;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.BusinessLogic.Models.Identity;
using MyPortal.Controllers;

namespace MyPortal.Areas.Staff.Controllers
{
    [UserType(UserType.Staff)]
    [RouteArea("Staff")]
    [System.Web.Http.RoutePrefix("Behaviour")]
    public class BehaviourController : MyPortalController
    {
        
    }
}