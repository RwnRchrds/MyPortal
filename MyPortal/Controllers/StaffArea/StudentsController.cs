using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Services;
using MyPortal.ViewModels;

namespace MyPortal.Controllers.StaffPortal
{
    [RoutePrefix("Staff/People/Students")]
    [UserType(UserType.Staff)]
    public class StudentsController : MyPortalController
    {
        [RequiresPermission("ViewStudents")]
        [Route("People/Students")]
        public ActionResult Students()
        {
            return View("~/Views/Staff/People/Students/Students.cshtml");
        }

        
        


        [RequiresPermission("EditStudents")]
        [Route("People/Students/New")]
        public ActionResult NewStudent()
        {
            var yearGroups = _context.PastoralYearGroups.ToList();
            var regGroups = _context.PastoralRegGroups.ToList();

            var viewModel = new NewStudentViewModel
            {

            };

            return View("~/Views/Staff/People/Students/NewStudent.cshtml", viewModel);
        }

        
        [HttpPost]
        [RequiresPermission("EditStudents")]
        public ActionResult SaveStudent(Student student)
        {
            var studentInDb = _context.Students.Single(l => l.Id == student.Id);

            studentInDb.Person.FirstName = student.Person.FirstName;
            studentInDb.Person.LastName = student.Person.LastName;
            studentInDb.Person.Gender = student.Person.Gender;
            studentInDb.YearGroupId = student.YearGroupId;
            studentInDb.RegGroupId = student.RegGroupId;

            _context.SaveChanges();
            return RedirectToAction("StudentDetails", "Portal", new { id = student.Id });
        }

        
        [RequiresPermission("ViewStudents")]
        [Route("People/Students/{studentId:int}", Name = "PeopleStudentDetails")]
        public async Task<ActionResult> StudentDetails(int studentId)
        {
            var student = _context.Students.SingleOrDefault(s => s.Id == studentId);

            if (student == null)
                return HttpNotFound();

            var logTypes = await ProfilesService.GetAllLogTypesLookup(_context);

            var commentBanks = ProfilesService.GetAllCommentBanksLookup(_context);

            var academicYearId = await SystemService.GetCurrentOrSelectedAcademicYearId(_context, User);

            double? attendance = null;

            var attendanceData = await AttendanceService.GetSummary(student.Id, academicYearId, _context, true);

            if (attendanceData != null)
            {
                attendance = attendanceData.Present + attendanceData.Late;
            }

            int? achievementCount = await BehaviourService.GetAchievementPointsCountByStudent(student.Id, academicYearId, _context);

            int? behaviourCount = await BehaviourService.GetBehaviourPointsCountByStudent(student.Id, academicYearId, _context);

            var viewModel = new StudentDetailsViewModel
            {
                
                Student = student,
                LogTypes = logTypes,
                BehaviourCount = behaviourCount,
                AchievementCount = achievementCount,
                HasAttendaceData = attendance != null,
                CommentBanks = commentBanks,
                Attendance = attendance,
            };

            return View("~/Views/Staff/People/Students/StudentOverview.cshtml", viewModel);
        }

        [RequiresPermission("EditStudents")]
        [Route("People/Students/{studentId:int}/ExtendedDetails", Name = "PeopleStudentExtendedDetails")]
        public async Task<ActionResult> StudentExtendedDetails(int studentId)
        {
            var student = _context.Students.SingleOrDefault(x => x.Id == studentId);

            if (student == null)
            {
                return HttpNotFound();
            }

            var yearGroups = await PastoralService.GetAllYearGroupsLookup(_context);

            var regGroups = await PastoralService.GetAllRegGroupsLookup(_context);

            var houses = await PastoralService.GetAllHousesLookup(_context);

            var viewModel = new StudentExtendedDetailsViewModel
            {
                Student = student,
                YearGroups = yearGroups,
                RegGroups = regGroups,
                Houses = houses
            };

            return View("~/Views/Staff/People/Students/ExtendedDetails.cshtml", viewModel);
        }

        [RequiresPermission("ViewStudents")]
        [Route("People/Students/{studentId:int}/Results", Name = "PeopleStudentAssessmentResults")]
        public ActionResult StudentResults(int studentId)
        {
            var student = _context.Students.SingleOrDefault(s => s.Id == studentId);

            var currentResultSet = _context.AssessmentResultSets.SingleOrDefault(r => r.IsCurrent);

            var resultSets = _context.AssessmentResultSets.OrderBy(x => x.Name).ToList();

            var subjects = _context.CurriculumSubjects.OrderBy(x => x.Name).ToList();

            if (student == null)
                return HttpNotFound();

            if (currentResultSet == null)
                return Content("ERROR: No Current Result Set Found");


            var viewModel = new StudentResultsViewModel
            {
                Student = student,
                CurrentResultSetId = currentResultSet.Id,
                ResultSets = resultSets,
                Subjects = subjects
            };

            return View("~/Views/Staff/People/Students/StudentResults.cshtml", viewModel);
        }

        [RequiresPermission("ViewStudents")]
        [Route("People/Students/{studentId:int}/Behaviour", Name = "PeopleStudentBehaviourDetails")]
        public ActionResult StudentBehaviour(int studentId)
        {
            var student = _context.Students.SingleOrDefault(x => x.Id == studentId);

            if (student == null)
            {
                return HttpNotFound();
            }

            var achievementTypes = _context.BehaviourAchievementTypes.OrderBy(x => x.Description).ToList();

            var behaviourTypes = _context.BehaviourIncidentTypes.OrderBy(x => x.Description).ToList();

            var behaviourLocations = _context.SchoolLocations.OrderBy(x => x.Description).ToList();

            var viewModel = new StudentBehaviourManagementViewModel
            {
                AchievementTypes = achievementTypes,
                BehaviourTypes = behaviourTypes,
                BehaviourLocations = behaviourLocations,
                Student = student
            };

            return View("~/Views/Staff/People/Students/BehaviourManagement.cshtml", viewModel);
        }
    }
}