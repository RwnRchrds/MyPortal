using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Areas.Students.ViewModels;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Controllers;
using MyPortal.Exceptions;
using MyPortal.Models;
using MyPortal.Services;

namespace MyPortal.Areas.Students.Controllers
{
    [UserType(UserType.Student)]
    [RouteArea("Students")]
    [RoutePrefix("Home")]
    public class HomeController : MyPortalController
    {
        // Student Landing Page
        public async Task<ActionResult> Index()
        {
            using (var curriculumService = new CurriculumService(UnitOfWork))
            using (var attendanceService = new AttendanceService(UnitOfWork))
            using (var behaviourService = new BehaviourService(UnitOfWork))
            using (var studentService = new StudentService(UnitOfWork))
            {
                var userId = User.Identity.GetUserId();

                var student = await studentService.GetStudentByUserId(userId);
                
                var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                var attendanceData = await attendanceService.GetSummary(student.Id, academicYearId);
            
                var attendance = attendanceData?.Present + attendanceData?.Late;

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