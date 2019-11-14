using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyPortal.Areas.Staff.ViewModels;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Services;

namespace MyPortal.Areas.Staff.Controllers
{
    [UserType(UserType.Staff)]
    [RouteArea("Staff")]
    [RoutePrefix("Assessment")]
    public class AssessmentController : MyPortalController
    {
        [RequiresPermission("EditResultSets")]
        [Route("ResultSets")]
        public ActionResult ResultSets()
        {
            return View();
        }
    }
}