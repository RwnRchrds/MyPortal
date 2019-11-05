using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Areas.Staff.ViewModels;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Services;

namespace MyPortal.Areas.Students.Controllers
{
    //MyPortal Students Controller --> Controller methods for Student areas
    [UserType(UserType.Student)]
    [RoutePrefix("Students")]
    public class StudentsController : MyPortalController
    {
        // Student Landing Page
        [Route("Home", Name = "StudentsIndex")]
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();

            var student = await StudentService.GetStudentFromUserId(userId, _context);

            if (student == null)
                return View("~/Views/Students/NoProfileIndex.cshtml");

            var academicYearId = await SystemService.GetCurrentOrSelectedAcademicYearId(_context, User);

            var attendanceData = await AttendanceService.GetSummary(student.Id, academicYearId, _context);
            
            var attendance = attendanceData?.Present + attendanceData?.Late;

            var achievementCount = await BehaviourService.GetAchievementPointsCountByStudent(student.Id, academicYearId, _context);

            var behaviourCount = await BehaviourService.GetBehaviourPointsCountByStudent(student.Id, academicYearId, _context);

            var viewModel = new StudentOverviewViewModel
            {                
                Student = student,
                BehaviourCount = behaviourCount,
                AchievementCount = achievementCount,
                Attendance = attendance,
                HasAttendaceData = attendance != null,
            };
            return View(viewModel);
        }
    }
}