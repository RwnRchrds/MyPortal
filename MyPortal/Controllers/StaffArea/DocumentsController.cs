using System.Web.Mvc;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Models;

namespace MyPortal.Controllers.StaffPortal
{
    [RoutePrefix("Staff/Documents")]
    [UserType(UserType.Staff)]
    public class DocumentsController : MyPortalController
    {
        [RequiresPermission("ViewApprovedDocuments, ViewAllDocuments")]
        [Route("Documents/Documents")]
        public ActionResult Documents()
        {
            return View("~/Views/Staff/Docs/Documents.cshtml");
        }
    }
}