using System.Web.Http;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Controllers;
using MyPortal.Models;

namespace MyPortal.Areas.Staff.Controllers
{
    [RoutePrefix("Behaviour")]
    [UserType(UserType.Staff)]
    public class BehaviourController : MyPortalController
    {
        
    }
}