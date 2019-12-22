using System.Web.Mvc;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Models.Identity;

namespace MyPortal.Areas.Staff.Controllers
{
    [UserType(UserType.Staff)]
    [RouteArea("Staff")]
    [System.Web.Http.RoutePrefix("Behaviour")]
    public class BehaviourController : MyPortalController
    {
        
    }
}