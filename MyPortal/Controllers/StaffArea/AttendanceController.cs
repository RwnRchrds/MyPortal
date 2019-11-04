using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.ApplicationInsights;
using MyPortal.Attributes;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Services;
using MyPortal.ViewModels;

namespace MyPortal.Controllers.StaffPortal
{
    [Authorize]
    [RoutePrefix("Staff/Attendance")]
    [UserType(UserType.Staff)]
    public class AttendanceController : MyPortalController
    {
        [RequiresPermission("TakeRegister")]
        [Route("Attendance/Registers")]
        public ActionResult Registers()
        {
            var userId = MetricDimensionNames.TelemetryContext.User.Identity.GetUserId();
            StaffMember currentUser = null;

            if (userId != null)
            {
                currentUser = _context.StaffMembers.SingleOrDefault(x => x.Person.UserId == userId);
            }

            var staffMembers = _context.StaffMembers.ToList().OrderBy(x => x.Person.LastName);

            var viewModel = new RegistersViewModel();

            viewModel.CurrentUser = currentUser;
            viewModel.StaffMembers = staffMembers;

            return View("~/Views/Staff/Attendance/Registers.cshtml", viewModel);
        }

        [RequiresPermission("TakeRegister")]
        [Route("Attendance/TakeRegister/{weekId:int}/{sessionId:int}", Name = "AttendanceTakeRegister")]
        public async Task<ActionResult> TakeRegister(int weekId, int sessionId)
        {
            var viewModel = new TakeRegisterViewModel();
            var attendanceWeek = _context.AttendanceWeeks.SingleOrDefault(x => x.Id == weekId);
            var session = _context.CurriculumSessions.SingleOrDefault(x => x.Id == sessionId);

            if (attendanceWeek == null || session == null || attendanceWeek.IsHoliday || attendanceWeek.IsNonTimetable)
            {
                return RedirectToAction("Registers");
            }

            var sessionDate = await AttendanceService.GetAttendancePeriodDate(attendanceWeek.Id, session.PeriodId, _context);

            viewModel.Session = session;
            viewModel.WeekId = attendanceWeek.Id;

            viewModel.SessionDate = sessionDate;

            return View("~/Views/Staff/Attendance/TakeRegister.cshtml", viewModel);
        }
    }
}