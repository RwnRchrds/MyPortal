using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Areas.Staff.ViewModels;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Services;

namespace MyPortal.Areas.Staff.Controllers
{
    [Authorize]
    [RoutePrefix("Attendance")]
    [UserType(UserType.Staff)]
    public class AttendanceController : MyPortalController
    {
        [RequiresPermission("TakeRegister")]
        [Route("Registers")]
        public async Task<ActionResult> Registers()
        {
            using (var staffService = new StaffMemberService(UnitOfWork))
            {
                var userId = User.Identity.GetUserId();
                StaffMember currentUser = null;

                if (userId != null)
                {
                    currentUser = await staffService.GetStaffMemberFromUserId(userId);
                }

                var staffMembers = await staffService.GetAllStaffMembers();

                var viewModel = new RegistersViewModel {CurrentUser = currentUser, StaffMembers = staffMembers};

                return View(viewModel);
            }
        }

        [RequiresPermission("TakeRegister")]
        [Route("TakeRegister/{weekId:int}/{sessionId:int}", Name = "AttendanceTakeRegister")]
        public async Task<ActionResult> TakeRegister(int weekId, int sessionId)
        {
            using (var curriculumService = new CurriculumService(UnitOfWork))
            using (var attendanceService = new AttendanceService(UnitOfWork))
            {
                var viewModel = new TakeRegisterViewModel();
                var attendanceWeek = await attendanceService.GetAttendanceWeekById(weekId);
                var session = await curriculumService.GetSessionById(sessionId);

                if (attendanceWeek == null || session == null || attendanceWeek.IsHoliday || attendanceWeek.IsNonTimetable)
                {
                    return RedirectToAction("Registers");
                }

                var sessionDate = await attendanceService.GetAttendancePeriodDate(weekId, session.PeriodId);

                viewModel.Session = session;
                viewModel.WeekId = attendanceWeek.Id;

                viewModel.SessionDate = sessionDate;

                return View(viewModel);
            }
        }
    }
}