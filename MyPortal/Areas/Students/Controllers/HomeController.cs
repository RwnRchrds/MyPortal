using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Areas.Students.ViewModels;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.BusinessLogic.Services;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Models.Identity;
using MyPortal.Services;

namespace MyPortal.Areas.Students.Controllers
{
    [UserType(UserType.Student)]
    [RouteArea("Students")]
    [RoutePrefix("Home")]
    public class HomeController : MyPortalController
    {
        public async Task<ActionResult> Index()
        {
            using (var attendanceService = new AttendanceService())
            using (var behaviourService = new BehaviourService())
            using (var studentService = new StudentService())
            {
                var userId = User.Identity.GetUserId();

                var student = await studentService.GetStudentByUserId(userId);
                
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var attendanceData = await attendanceService.GetSummary(student.Id, academicYearId);
            
                var attendance = attendanceData?.Present;

                var achievementCount =
                    await behaviourService.GetAchievementPointsCountByStudent(student.Id, academicYearId);

                var behaviourCount =
                    await behaviourService.GetBehaviourPointsCountByStudent(student.Id, academicYearId);

                var viewModel = new StudentHomeViewModel
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
}