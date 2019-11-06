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
    [RoutePrefix("Assessment")]
    [UserType(UserType.Student)]
    public class AssessmentController : MyPortalController
    {
        [Route("Results")]
        public async Task<ActionResult> Results()
        {
            using (var assessmentService = new AssessmentService(UnitOfWork))
            using (var studentService = new StudentService(UnitOfWork))
            {
                var userId = User.Identity.GetUserId();

                var student = await studentService.GetStudentFromUserId(userId);

                var resultSets = await assessmentService.GetAllResultSets();

                var viewModel = new StudentResultsViewModel
                {
                    Student = student,
                    ResultSets = resultSets
                };

                return View(viewModel);
            }
        }
    }
}