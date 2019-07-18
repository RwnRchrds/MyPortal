using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    //MyPortal Students Controller --> Controller methods for Student areas
    [System.Web.Mvc.Authorize(Roles = "Student")]
    [System.Web.Mvc.RoutePrefix("Students")]
    public class StudentsController : MyPortalController
    {
        #region Store

        //Sales History
        [System.Web.Mvc.Route("Store/SalesHistory")]
        public ActionResult SalesHistory()
        {
            var userId = User.Identity.GetUserId();

            var studentInDb = _context.Students.SingleOrDefault(s => s.Person.UserId == userId);

            if (studentInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var viewModel = new StudentSalesHistoryViewModel
            {
                Student = studentInDb
            };

            return View("~/Views/Students/Store/SalesHistory.cshtml", viewModel);
        }

        //Store Page
        [System.Web.Mvc.Route("Store/Store")]
        public ActionResult Store()
        {
            var userId = User.Identity.GetUserId();

            var studentInDb = _context.Students.SingleOrDefault(s => s.Person.UserId == userId);

            if (studentInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var viewModel = new StudentStoreViewModel
            {
                Student = studentInDb
            };

            return View("~/Views/Students/Store/Store.cshtml", viewModel);
        }

        #endregion

        // Student Landing Page
        [System.Web.Mvc.Route("Home")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var student = PeopleProcesses.GetStudentFromUserId(userId, _context).ResponseObject;

            if (student == null)
                return View("~/Views/Students/NoProfileIndex.cshtml");

            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            var attendanceData = AttendanceProcesses.GetSummary(student.Id, academicYearId, _context).ResponseObject;
            
            var attendance = attendanceData?.Present + attendanceData?.Late;

            var achievementCount = BehaviourProcesses.GetAchievementPointsCount(student.Id, academicYearId, _context).ResponseObject;

            var behaviourCount = BehaviourProcesses.GetBehaviourPointsCount(student.Id, academicYearId, _context).ResponseObject;

            var viewModel = new StudentDetailsViewModel
            {                
                Student = student,
                BehaviourCount = behaviourCount,
                AchievementCount = achievementCount,
                Attendance = attendance,
                HasAttendaceData = attendance != null,
            };
            return View(viewModel);
        }

        //MyResults Page
        [System.Web.Mvc.Route("Results")]
        public ActionResult Results()
        {
            var userId = User.Identity.GetUserId();

            var student = PrepareResponseObject(PeopleProcesses.GetStudentFromUserId(userId, _context));

            if (student == null)
                return HttpNotFound();

            var resultSets = PrepareResponseObject(AssessmentProcesses.GetAllResultSets_Model(_context)).ToList();

            var currentResultSet = resultSets.SingleOrDefault(x => x.IsCurrent);

            if (currentResultSet == null)
                return Content("No current result set set");

            var currentResultSetId = currentResultSet.Id;

            var viewModel = new StudentResultsViewModel
            {
                Student = student,
                ResultSets = resultSets,
                CurrentResultSetId = currentResultSetId
            };

            return View(viewModel);
        }

    }
}