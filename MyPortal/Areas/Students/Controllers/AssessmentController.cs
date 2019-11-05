using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Areas.Students.ViewModels;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Services;

namespace MyPortal.Areas.Students.Controllers
{
    [RoutePrefix("Assessment/Results")]
    [UserType(UserType.Student)]
    public class AssessmentController : MyPortalController
    {
        [Route("Results")]
        public async Task<ActionResult> Results()
        {
            var userId = User.Identity.GetUserId();

            var student = await StudentService.GetStudentFromUserId(userId, _context);

            if (student == null)
                return HttpNotFound();

            var list = await new AssessmentService(_context).GetAllResultSets();

            var resultSets = list.ToList();

            var currentResultSet = resultSets.SingleOrDefault(x => x.IsCurrent);

            if (currentResultSet == null)
                return Content("No current result set set");

            var currentResultSetId = currentResultSet.Id;

            var viewModel = new StudentResultsViewModel
            {
                Student = student,
                ResultSets = resultSets
            };

            return View(viewModel);
        }
    }
}