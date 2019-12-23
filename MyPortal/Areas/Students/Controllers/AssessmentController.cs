using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Areas.Students.ViewModels;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.BusinessLogic.Services;
using MyPortal.Models.Identity;
using MyPortal.Services;

namespace MyPortal.Areas.Students.Controllers
{
    [UserType(UserType.Student)]
    [RouteArea("Students")]
    [RoutePrefix("Assessment")]
    public class AssessmentController : MyPortalController
    {
        [Route("Results")]
        public async Task<ActionResult> Results()
        {
            using (var assessmentService = new AssessmentService())
            using (var studentService = new StudentService())
            {
                var userId = User.Identity.GetUserId();

                var student = await studentService.GetStudentByUserId(userId);

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