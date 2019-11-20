using System.Threading.Tasks;
using System.Web.Mvc;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Services;

namespace MyPortal.Areas.Staff.Controllers
{
    [UserType(UserType.Staff)]
    [RouteArea("Staff")]
    [RoutePrefix("Personnel")]
    public class PersonnelController : MyPortalController
    {
        [RequiresPermission("ViewTrainingCourses")]
        [Route("TrainingCourses")]
        public ActionResult TrainingCourses()
        {
            return View();
        }

        [RequiresPermission("EditTrainingCourses")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCourse(PersonnelTrainingCourse course)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("NewTrainingCourse");
            }

            using (var personnelService = new PersonnelService())
            {
                await personnelService.CreateCourse(course);

                return RedirectToAction("TrainingCourses");
            }
        }

        [RequiresPermission("EditTrainingCourses")]
        [Route("TrainingCourses/New")]
        public ActionResult NewTrainingCourse()
        {
            return View();
        }
    }
}