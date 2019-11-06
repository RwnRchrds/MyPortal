using System.Web.Mvc;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Controllers;
using MyPortal.Models;

namespace MyPortal.Areas.Staff.Controllers
{
    [UserType(UserType.Staff)]
    [RouteArea("Staff")]
    [RoutePrefix("Documents")]
    public class DocumentsController : MyPortalController
    {
        [RequiresPermission("ViewApprovedDocuments, ViewAllDocuments")]
        [Route("Documents")]
        public ActionResult Documents()
        {
            return View();
        }
    }
}