using System.IO;
using System.Web;
using System.Web.Mvc;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Models;
using MyPortal.ViewModels;

namespace MyPortal.Controllers.StaffPortal
{
    [Authorize]
    [RoutePrefix("Staff/Assessment")]
    [UserType(UserType.Staff)]
    public class AssessmentController : MyPortalController
    {
        [RequiresPermission("ImportResults")]
        [Route("Results/Import")]
        public ActionResult ImportResults()
        {
            var resultSets = _context.AssessmentResultSets.OrderBy(x => x.Name).ToList();
            var fileExists = System.IO.File.Exists(@"C:/MyPortal/Files/Results/import.csv");
            var viewModel = new ImportResultsViewModel
            {
                ResultSets = resultSets,
                FileExists = fileExists
            };

            return View("~/Views/Staff/Assessment/ImportResults.cshtml", viewModel);
        }

        [HttpPost]
        [RequiresPermission("ImportResults")]
        public ActionResult UploadResults(HttpPostedFileBase file)
        {
            if (file.ContentLength <= 0 || Path.GetExtension(file.FileName) != ".csv")
                return RedirectToAction("ImportResults");
            const string path = @"C:/MyPortal/Files/Results/import.csv";
            file.SaveAs(path);

            return RedirectToAction("ImportResults");
        }

        
        [RequiresPermission("EditResultSets")]
        [Route("ResultSets")]
        public ActionResult ResultSets()
        {
            return View("~/Views/Staff/Assessment/ResultSets.cshtml");
        }
    }
}