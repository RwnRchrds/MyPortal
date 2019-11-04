using System.Web.Http;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Models;

namespace MyPortal.Controllers.StaffPortal
{
    [RoutePrefix("Staff/Behaviour")]
    [UserType(UserType.Staff)]
    public class BehaviourController : MyPortalController
    {
        
    }
}