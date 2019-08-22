using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Controllers.Api;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    //MyPortal Staff Controller --> Controller Methods for Staff Areas
    [System.Web.Mvc.Authorize(Roles = "Staff, SeniorStaff")]
    [System.Web.Mvc.RoutePrefix("Staff")]
    public class StaffController : MyPortalController
    {

        #region Admission
        #endregion

        #region Assessment

        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Assessment/Results/Import")]
        public ActionResult ImportResults()
        {
            var resultSets = _context.AssessmentResultSets.OrderBy(x => x.Name).ToList();
            var fileExists = System.IO.File.Exists(@"C:/MyPortal/Files/Results/import.csv");
            var viewModel = new ImportResultsViewModel
            {
                ResultSets = resultSets,
                FileExists = fileExists
            };

            return View("~/Views/Staff/Assessment/ImportResults.cshtml", viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult UploadResults(HttpPostedFileBase file)
        {
            if (file.ContentLength <= 0 || Path.GetExtension(file.FileName) != ".csv")
                return RedirectToAction("ImportResults");
            const string path = @"C:/MyPortal/Files/Results/import.csv";
            file.SaveAs(path);

            return RedirectToAction("ImportResults");
        }

        // Menu | Result Sets --> Result Sets List (All)
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Assessment/ResultSets")]
        public ActionResult ResultSets()
        {
            return View("~/Views/Staff/Assessment/ResultSets.cshtml");
        }

        #endregion

        #region Attendance

        [System.Web.Mvc.Route("Attendance/Registers")]
        public ActionResult Registers()
        {
            var userId = User.Identity.GetUserId();
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

        [System.Web.Mvc.Route("Attendance/TakeRegister/{weekId:int}/{sessionId:int}")]
        public ActionResult TakeRegister(int weekId, int sessionId)
        {
            var viewModel = new TakeRegisterViewModel();
            var attendanceWeek = _context.AttendanceWeeks.SingleOrDefault(x => x.Id == weekId);
            var session = _context.CurriculumSessions.SingleOrDefault(x => x.Id == sessionId);

            if (attendanceWeek == null || session == null || attendanceWeek.IsHoliday || attendanceWeek.IsNonTimetable)
            {
                return RedirectToAction("Registers");
            }

            var sessionDate = PrepareResponseObject(AttendanceProcesses.GetPeriodDate(attendanceWeek.Id, session.PeriodId, _context));

            viewModel.Session = session;
            viewModel.WeekId = attendanceWeek.Id;

            viewModel.Periods = _context.AttendancePeriods.Where(x => x.Weekday == session.AttendancePeriod.Weekday).OrderBy(x => x.StartTime).ToList();

            viewModel.SessionDate = sessionDate;

            return View("~/Views/Staff/Attendance/TakeRegister.cshtml", viewModel);
        }

        #endregion

        #region Behaviour
        #endregion

        #region Communication
        #endregion

        #region Curriculum

        // Menu | Subjects --> Subjects List (All)
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Curriculum/Subjects")]
        public ActionResult Subjects()
        {
            var viewModel = new SubjectsViewModel();
            viewModel.Staff = _context.StaffMembers.OrderBy(x => x.Person.LastName).ToList();

            return View("~/Views/Staff/Curriculum/Subjects.cshtml", viewModel);
        }

        // Menu | Study Topics --> Study Topics List (All)
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Curriculum/StudyTopics")]
        public ActionResult StudyTopics()
        {
            var viewModel = new StudyTopicsViewModel();

            var subjects = _context.CurriculumSubjects.OrderBy(x => x.Name).ToList();

            var yearGroups = _context.PastoralYearGroups.OrderBy(x => x.Name).ToList();

            viewModel.Subjects = subjects;
            viewModel.YearGroups = yearGroups;

            return View("~/Views/Staff/Curriculum/StudyTopics.cshtml", viewModel);
        }

        //Menu | Lesson Plans --> Lesson Plans List (All)
        [System.Web.Mvc.Route("Curriculum/LessonPlans")]
        public ActionResult LessonPlans()
        {
            var viewModel = new LessonPlansViewModel();

            var studyTopics = _context.CurriculumStudyTopics.OrderBy(x => x.Name).ToList();

            viewModel.StudyTopics = studyTopics;

            return View("~/Views/Staff/Curriculum/LessonPlans.cshtml", viewModel);
        }

        //Menu | Lesson Plans | X --> Lesson Plan Details for Lesson Plan X
        [System.Web.Mvc.Route("Curriculum/LessonPlans/View/{id}")]
        public ActionResult LessonPlanDetails(int id)
        {
            var lessonPlan = _context.CurriculumLessonPlans.SingleOrDefault(x => x.Id == id);

            if (lessonPlan == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var viewModel = new LessonPlanDetailsViewModel();

            viewModel.LessonPlan = lessonPlan;

            return View("~/Views/Staff/Curriculum/LessonPlanDetails.cshtml", viewModel);
        }

        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Curriculum/Classes")]
        public ActionResult Classes()
        {
            var viewModel = new ClassesViewModel();

            viewModel.Staff = _context.StaffMembers.ToList().OrderBy(x => x.Person.LastName);

            viewModel.Subjects = _context.CurriculumSubjects.ToList().OrderBy(x => x.Name);

            return View("~/Views/Staff/Curriculum/Classes.cshtml", viewModel);
        }

        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Curriculum/Classes/Sessions/{classId:int}")]
        public ActionResult ClassSchedule(int classId)
        {
            var viewModel = new SessionsViewModel();

            var currClass = _context.CurriculumClasses.SingleOrDefault(x => x.Id == classId);

            viewModel.Class = currClass ?? throw new HttpResponseException(HttpStatusCode.NotFound);

            var dayIndex = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            viewModel.Periods = _context.AttendancePeriods.ToList().OrderBy(x => dayIndex.IndexOf(x.Weekday))
                .ThenBy(x => x.StartTime);

            return View("~/Views/Staff/Curriculum/Sessions.cshtml", viewModel);
        }

        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Curriculum/Classes/Enrolments/{classId:int}")]
        public ActionResult ClassEnrolments(int classId)
        {
            var viewModel = new ClassEnrolmentsViewModel();

            var currClass = _context.CurriculumClasses.SingleOrDefault(x => x.Id == classId);

            viewModel.Class = currClass ?? throw new HttpResponseException(HttpStatusCode.NotFound);

            return View("~/Views/Staff/Curriculum/Enrolments.cshtml", viewModel);
        }

        #endregion

        #region Documents

        // Menu | Documents --> General Controlled Documents List (All)
        //Accessible by [Staff] or [SeniorStaff]
        [System.Web.Mvc.Route("Documents/Documents")]
        public ActionResult Documents()
        {
            return View("~/Views/Staff/Docs/Documents.cshtml");
        }


        #endregion

        #region Homework
        #endregion

        #region Medical
        #endregion

        #region People

        // Menu | Students --> Students List (All)
        // Accessible by [Staff] or [SeniorStaff]
        [System.Web.Mvc.Route("People/Students")]
        public ActionResult Students()
        {
            return View("~/Views/Staff/People/Students/Students.cshtml");
        }

        // HTTP POST request for creating students using HTML form
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewStudentViewModel
                {
                    Student = student,
                    RegGroups = _context.PastoralRegGroups.ToList(),
                    YearGroups = _context.PastoralYearGroups.ToList()
                };
                return View("~/Views/Staff/People/Students/NewStudent.cshtml", viewModel);
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            return RedirectToAction("Students", "Staff");
        }

        // Menu | Students | New Student --> New Student form
        // Accessible by [SeniorStaff] only
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("People/Students/New")]
        public ActionResult NewStudent()
        {
            var yearGroups = _context.PastoralYearGroups.ToList();
            var regGroups = _context.PastoralRegGroups.ToList();

            var viewModel = new NewStudentViewModel
            {
                RegGroups = regGroups,
                YearGroups = yearGroups
            };

            return View("~/Views/Staff/People/Students/NewStudent.cshtml", viewModel);
        }

        // HTTP POST request for updating student details using HTML form
        [System.Web.Mvc.HttpPost]
        public ActionResult SaveStudent(Student student)
        {
            var studentInDb = _context.Students.Single(l => l.Id == student.Id);

            studentInDb.Person.FirstName = student.Person.FirstName;
            studentInDb.Person.LastName = student.Person.LastName;
            studentInDb.Person.Gender = student.Person.Gender;
            studentInDb.YearGroupId = student.YearGroupId;
            studentInDb.RegGroupId = student.RegGroupId;

            _context.SaveChanges();
            return RedirectToAction("StudentDetails", "Staff", new { id = student.Id });
        }

        // Menu | Students | X --> Student Details (for Student X)
        //Accessible by [Staff] or [SeniorStaff]
        [System.Web.Mvc.Route("People/Students/{id:int}", Name = "StudentDetails")]
        public ActionResult StudentDetails(int id)
        {
            var student = _context.Students.SingleOrDefault(s => s.Id == id);

            if (student == null)
                return HttpNotFound();

            //var logs = _context.Logs.Where(l => l.Student == id).OrderByDescending(x => x.Date).ToList();

            var logTypes = _context.ProfileLogTypes.OrderBy(x => x.Name).ToList();

            var yearGroups = _context.PastoralYearGroups.OrderBy(x => x.Name).ToList();

            var regGroups = _context.PastoralRegGroups.OrderBy(x => x.Name).ToList();

            var resultSets = _context.AssessmentResultSets.OrderBy(x => x.Name).ToList();

            var subjects = _context.CurriculumSubjects.OrderBy(x => x.Name).ToList();

            var commentBanks = _context.ProfileCommentBanks.OrderBy(x => x.Name).ToList();

            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            var attendanceData = AttendanceProcesses.GetSummary(student.Id, academicYearId, _context, true).ResponseObject;

            double? attendance = null;

            if (attendanceData != null)
            {
                attendance = attendanceData.Present + attendanceData.Late;
            } 

            var achievementCount = BehaviourProcesses.GetAchievementPointsCount(student.Id, academicYearId, _context).ResponseObject;

            var behaviourCount = BehaviourProcesses.GetBehaviourPointsCount(student.Id, academicYearId, _context).ResponseObject;

            var viewModel = new StudentDetailsViewModel
            {
                //Logs = logs,
                Student = student,
                LogTypes = logTypes,
                YearGroups = yearGroups,
                RegGroups = regGroups,
                ResultSets = resultSets,
                Subjects = subjects,
                CommentBanks = commentBanks,
                BehaviourCount = behaviourCount,
                AchievementCount = achievementCount,
                HasAttendaceData = attendance != null,
                Attendance = attendance
            };

            return View("~/Views/Staff/People/Students/StudentDetails.cshtml", viewModel);
        }

        //Menu | Students | X | [View Results] --> Student Results (for Student X)
        //Accessible by [Staff] or [SeniorStaff]
        [System.Web.Mvc.Route("People/Students/{id}/Results")]
        public ActionResult StudentResults(int id)
        {
            var student = _context.Students.SingleOrDefault(s => s.Id == id);

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

        [System.Web.Mvc.Route("People/Students/{id}/Behaviour")]
        public ActionResult StudentBehaviour(int id)
        {
            var student = _context.Students.SingleOrDefault(x => x.Id == id);

            if (student == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var achievementTypes = _context.BehaviourAchievementTypes.OrderBy(x => x.Description).ToList();

            var behaviourTypes = _context.BehaviourIncidentTypes.OrderBy(x => x.Description).ToList();

            var behaviourLocations = _context.BehaviourLocations.OrderBy(x => x.Description).ToList();

            var viewModel = new StudentBehaviourManagementViewModel
            {
                AchievementTypes = achievementTypes,
                BehaviourTypes = behaviourTypes,
                BehaviourLocations = behaviourLocations,
                Student = student
            };

            return View("~/Views/Staff/People/Students/BehaviourManagement.cshtml", viewModel);
        }

        // Menu | Staff --> Staff List (All)
        // Accessible by [SeniorStaff] only
        [System.Web.Mvc.Route("People/Staff")]
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        public ActionResult Staff()
        {
            var viewModel = new NewStaffViewModel();
            return View("~/Views/Staff/People/Staff/Staff.cshtml", viewModel);
        }

        // Menu | Staff | X --> Student Details (for Staff X)
        //Accessible by [SeniorStaff] only
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("People/Staff/{id}")]
        public ActionResult StaffDetails(int id)
        {
            var staff = _context.StaffMembers.SingleOrDefault(s => s.Id == id);

            if (staff == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();

            var currentStaffId = 0;

            var currentUser = _context.StaffMembers.SingleOrDefault(x => x.Person.UserId == userId);

            if (currentUser != null)
            {
                currentStaffId = currentUser.Id;
            }

            var certificates = _context.PersonnelTrainingCertificates.Where(c => c.StaffId == id).ToList();

            var courses = _context.PersonnelTrainingCourses.ToList();

            var viewModel = new StaffDetailsViewModel
            {
                Staff = staff,
                TrainingCertificates = certificates,
                TrainingCourses = courses,
                CurrentStaffId = currentStaffId
            };

            return View("~/Views/Staff/People/Staff/StaffDetails.cshtml", viewModel);
        }

        #endregion

        #region Personnel

        // Menu | Training Courses --> Training Courses List (All)
        //[Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Personnel/TrainingCourses")]
        public ActionResult TrainingCourses()
        {
            return View("~/Views/Staff/Personnel/TrainingCourses.cshtml");
        }

        // HTTP POST request for creating training courses using HTML form
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(PersonnelTrainingCourse course)
        {
            if (!ModelState.IsValid) return View("~/Views/Staff/Personnel/NewTrainingCourse.cshtml");

            _context.PersonnelTrainingCourses.Add(course);
            _context.SaveChanges();

            return RedirectToAction("TrainingCourses", "Staff");
        }

        // Menu | Training Courses | New Course --> New Course Form
        // Accessible by [SeniorStaff] only
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Personnel/TrainingCourses/New")]
        public ActionResult NewCourse()
        {
            return View("~/Views/Staff/Personnel/NewTrainingCourse.cshtml");
        }

        #endregion

        #region Profile

        // Menu | Comment Banks --> Comment Banks List (All)
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Profile/CommentBanks")]
        public ActionResult CommentBanks()
        {
            return View("~/Views/Staff/Profile/CommentBanks.cshtml");
        }

        //Menu | Comments --> Comments List (All)
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Profile/Comments")]
        public ActionResult Comments()
        {
            var viewModel = new CommentsViewModel();
            viewModel.CommentBanks = _context.ProfileCommentBanks.OrderBy(x => x.Name).ToList();

            return View("~/Views/Staff/Profile/Comments.cshtml", viewModel);
        }

        #endregion

        // Staff Landing Page
        [System.Web.Mvc.Route("Home")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var person = _context.Persons.SingleOrDefault(s => s.UserId == userId);

            var staff = _context.StaffMembers.SingleOrDefault(x => x.PersonId == person.Id);

            var academicYears = _context.CurriculumAcademicYears.ToList().OrderByDescending(x => x.FirstDate);

            var selectedAcademicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            if (staff == null)
                return View("~/Views/Staff/NoProfileIndex.cshtml");

            var viewModel = new StaffHomeViewModel
            {
                CurrentUser = staff,
                CurriculumAcademicYears = academicYears,
                SelectedAcademicYearId = selectedAcademicYearId
            };

            return View(viewModel);
        }
    }
}