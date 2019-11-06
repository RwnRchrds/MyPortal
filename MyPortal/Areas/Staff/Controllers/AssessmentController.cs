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
        [RequiresPermission("ImportResults")]
        [Route("Results/Import")]
        public async Task<ActionResult> ImportResults()
        {
            using (var assessmentService = new AssessmentService(UnitOfWork))
            {
                var resultSets = await assessmentService.GetAllResultSets();
                var fileExists = System.IO.File.Exists(@"C:/MyPortal/Files/Results/import.csv");
                var viewModel = new ImportResultsViewModel
                {
                    ResultSets = resultSets,
                    FileExists = fileExists
                };

                return View(viewModel);
            }
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
            return View();
        }
    }
}