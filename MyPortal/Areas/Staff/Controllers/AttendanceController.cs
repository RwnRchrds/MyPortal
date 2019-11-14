using System.Collections.Generic;
using System.Linq;
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
    [UserType(UserType.Staff)]
    [RouteArea("Staff")]
    [RoutePrefix("Attendance")]
    public class AttendanceController : MyPortalController
    {
        [RequiresPermission("EditAttendance")]
        [Route("Registers")]
        public async Task<ActionResult> Registers()
        {
            using (var staffService = new StaffMemberService(UnitOfWork))
            {
                var userId = User.Identity.GetUserId();
                StaffMember currentUser = null;

                if (userId != null)
                {
                    currentUser = await staffService.GetStaffMemberByUserId(userId);
                }

                var staffMembers = await staffService.GetAllStaffMembers();

                var viewModel = new RegistersViewModel {CurrentUser = currentUser, StaffMembers = staffMembers};

                return View(viewModel);
            }
        }

        [RequiresPermission("EditAttendance")]
        [Route("EditAttendance/{weekId:int}/{sessionId:int}")]
        public async Task<ActionResult> TakeRegister(int weekId, int sessionId)
        {
            using (var curriculumService = new CurriculumService(UnitOfWork))
            using (var attendanceService = new AttendanceService(UnitOfWork))
            {
                var attendanceWeek = await attendanceService.GetAttendanceWeekById(weekId);

                if (attendanceWeek.IsHoliday || attendanceWeek.IsNonTimetable)
                {
                    return RedirectToAction("Registers");
                }

                var session = await curriculumService.GetSessionByIdWithRelated(sessionId);
                var sessionDate = await attendanceService.GetAttendancePeriodDate(weekId, session.PeriodId);
                var attendanceMarks = await attendanceService.GetRegisterMarks(weekId, sessionId);
                var periods = await attendanceService.GetPeriodsByDayOfWeek(sessionDate.DayOfWeek);
                var codes = await attendanceService.GetAllAttendanceCodes();

                var viewModel = new TakeRegisterViewModel
                {
                    Session = session,
                    AttendanceMarks = attendanceMarks,
                    Week = attendanceWeek,
                    Periods = periods,
                    SessionDate = sessionDate,
                    AttendanceCodes = codes.Where(x => !x.DoNotUse).Select(attendanceCode => attendanceCode.Code).ToList()
                };

                return View(viewModel);
            }
        }
    }
}