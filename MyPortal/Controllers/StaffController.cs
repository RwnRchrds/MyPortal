using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Provider;
using MyPortal.Attributes;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    
    [Authorize]
    [RequiresPermission("AccessStaffPortal")]
    [RoutePrefix("Staff")]
    public class StaffController : MyPortalController
    {

        #region Admission
        #endregion

        #region Assessment

        [RequiresPermission("ImportResults")]
        [Route("Assessment/Results/Import")]
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

        [HttpPost]
        [RequiresPermission("ImportResults")]
        public ActionResult UploadResults(HttpPostedFileBase file)
        {
            if (file.ContentLength <= 0 || Path.GetExtension(file.FileName) != ".csv")
                return RedirectToAction("ImportResults");
            const string path = @"C:/MyPortal/Files/Results/import.csv";
            file.SaveAs(path);

            return RedirectToAction("ImportResults");
        }

        
        [RequiresPermission("EditResultSets")]
        [Route("Assessment/ResultSets")]
        public ActionResult ResultSets()
        {
            return View("~/Views/Staff/Assessment/ResultSets.cshtml");
        }

        #endregion

        #region Attendance

        [RequiresPermission("TakeRegister")]
        [Route("Attendance/Registers")]
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

            var sessionDate = await AttendanceProcesses.GetPeriodDate(attendanceWeek.Id, session.PeriodId, _context);

            viewModel.Session = session;
            viewModel.WeekId = attendanceWeek.Id;

            viewModel.SessionDate = sessionDate;

            return View("~/Views/Staff/Attendance/TakeRegister.cshtml", viewModel);
        }

        #endregion

        #region Behaviour
        #endregion

        #region Communication
        #endregion

        #region Curriculum

        
        [RequiresPermission("EditSubjects")]
        [Route("Curriculum/Subjects")]
        public ActionResult Subjects()
        {
            var viewModel = new SubjectsViewModel();
            viewModel.Staff = _context.StaffMembers.OrderBy(x => x.Person.LastName).ToList();

            return View("~/Views/Staff/Curriculum/Subjects.cshtml", viewModel);
        }

        
        [RequiresPermission("EditStudyTopics")]
        [Route("Curriculum/StudyTopics")]
        public ActionResult StudyTopics()
        {
            var viewModel = new StudyTopicsViewModel();

            var subjects = _context.CurriculumSubjects.OrderBy(x => x.Name).ToList();

            var yearGroups = _context.PastoralYearGroups.OrderBy(x => x.Name).ToList();

            viewModel.Subjects = subjects;
            viewModel.YearGroups = yearGroups;

            return View("~/Views/Staff/Curriculum/StudyTopics.cshtml", viewModel);
        }

        [RequiresPermission("ViewLessonPlans")]
        [Route("Curriculum/LessonPlans")]
        public ActionResult LessonPlans()
        {
            var viewModel = new LessonPlansViewModel();

            var studyTopics = _context.CurriculumStudyTopics.OrderBy(x => x.Name).ToList();

            viewModel.StudyTopics = studyTopics;

            return View("~/Views/Staff/Curriculum/LessonPlans.cshtml", viewModel);
        }

        [RequiresPermission("ViewLessonPlans")]
        [Route("Curriculum/LessonPlans/View/{id}", Name = "CurriculumLessonPlanDetails")]
        public ActionResult LessonPlanDetails(int id)
        {
            var lessonPlan = _context.CurriculumLessonPlans.SingleOrDefault(x => x.Id == id);

            if (lessonPlan == null)
            {
                return HttpNotFound();
            }

            var viewModel = new LessonPlanDetailsViewModel();

            viewModel.LessonPlan = lessonPlan;

            return View("~/Views/Staff/Curriculum/LessonPlanDetails.cshtml", viewModel);
        }

        [RequiresPermission("EditClasses")]
        [Route("Curriculum/Classes")]
        public ActionResult Classes()
        {
            var viewModel = new ClassesViewModel();

            viewModel.Staff = _context.StaffMembers.ToList().OrderBy(x => x.Person.LastName);

            viewModel.Subjects = _context.CurriculumSubjects.ToList().OrderBy(x => x.Name);

            return View("~/Views/Staff/Curriculum/Classes.cshtml", viewModel);
        }

        [RequiresPermission("EditClasses")]
        [Route("Curriculum/Classes/Sessions/{classId:int}")]
        public ActionResult ClassSchedule(int classId)
        {
            var viewModel = new SessionsViewModel();

            var currClass = _context.CurriculumClasses.SingleOrDefault(x => x.Id == classId);

            if (currClass == null)
            {
                return HttpNotFound();
            }

            viewModel.Class = currClass;

            var dayIndex = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            viewModel.Periods = _context.AttendancePeriods.ToList().OrderBy(x => x)
                .ThenBy(x => x.StartTime);

            return View("~/Views/Staff/Curriculum/Sessions.cshtml", viewModel);
        }

        [RequiresPermission("EditClasses")]
        [Route("Curriculum/Classes/Enrolments/{classId:int}", Name = "CurriculumEnrolments")]
        public ActionResult ClassEnrolments(int classId)
        {
            var viewModel = new ClassEnrolmentsViewModel();

            var currClass = _context.CurriculumClasses.SingleOrDefault(x => x.Id == classId);

            if (currClass == null)
            {
                return HttpNotFound();
            }

            viewModel.Class = currClass;

            return View("~/Views/Staff/Curriculum/Enrolments.cshtml", viewModel);
        }

        #endregion

        #region Documents

        [RequiresPermission("ViewApprovedDocuments, ViewAllDocuments")]
        [Route("Documents/Documents")]
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

        [RequiresPermission("ViewStudents")]
        [Route("People/Students")]
        public ActionResult Students()
        {
            return View("~/Views/Staff/People/Students/Students.cshtml");
        }

        
        [HttpPost]
        [RequiresPermission("EditStudents")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewStudentViewModel
                {
                    Student = student
                };
                return View("~/Views/Staff/People/Students/NewStudent.cshtml", viewModel);
            }

            PeopleProcesses.CreateStudent(student, _context);
            
            return RedirectToAction("Students", "Staff");
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
            return RedirectToAction("StudentDetails", "Staff", new { id = student.Id });
        }

        
        [RequiresPermission("ViewStudents")]
        [Route("People/Students/{studentId:int}", Name = "PeopleStudentDetails")]
        public async Task<ActionResult> StudentDetails(int studentId)
        {
            var student = _context.Students.SingleOrDefault(s => s.Id == studentId);

            if (student == null)
                return HttpNotFound();

            var logTypes = await ProfilesProcesses.GetAllLogTypesLookup(_context);

            var commentBanks = ProfilesProcesses.GetAllCommentBanksLookup(_context);

            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            double? attendance = null;

            var attendanceData = await AttendanceProcesses.GetSummary(student.Id, academicYearId, _context, true);

            if (attendanceData != null)
            {
                attendance = attendanceData.Present + attendanceData.Late;
            }

            int? achievementCount = await BehaviourProcesses.GetAchievementPointsCountByStudent(student.Id, academicYearId, _context);

            int? behaviourCount = await BehaviourProcesses.GetBehaviourPointsCountByStudent(student.Id, academicYearId, _context);

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

            var yearGroups = await PastoralProcesses.GetAllYearGroupsLookup(_context);

            var regGroups = await PastoralProcesses.GetAllRegGroupsLookup(_context);

            var houses = await PastoralProcesses.GetAllHousesLookup(_context);

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

        [Route("People/Staff")]
        [RequiresPermission("ViewStaff")]
        public ActionResult Staff()
        {
            var viewModel = new NewStaffViewModel();
            return View("~/Views/Staff/People/Staff/Staff.cshtml", viewModel);
        }

        [RequiresPermission("ViewStaff")]
        [Route("People/Staff/{id}", Name = "PeopleStaffDetails")]
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

        [RequiresPermission("ViewTrainingCourses")]
        [Route("Personnel/TrainingCourses")]
        public ActionResult TrainingCourses()
        {
            return View("~/Views/Staff/Personnel/TrainingCourses.cshtml");
        }

        [RequiresPermission("EditTrainingCourses")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(PersonnelTrainingCourse course)
        {
            if (!ModelState.IsValid) return View("~/Views/Staff/Personnel/NewTrainingCourse.cshtml");

            PersonnelProcesses.CreateCourse(course, _context);

            return RedirectToAction("TrainingCourses", "Staff");
        }

        [RequiresPermission("EditTrainingCourses")]
        [Route("Personnel/TrainingCourses/New")]
        public ActionResult NewCourse()
        {
            return View("~/Views/Staff/Personnel/NewTrainingCourse.cshtml");
        }

        #endregion

        #region Profile

        [RequiresPermission("EditComments")]
        [Route("Profile/CommentBanks")]
        public ActionResult CommentBanks()
        {
            return View("~/Views/Staff/Profile/CommentBanks.cshtml");
        }

        [RequiresPermission("EditComments")]
        [Route("Profile/Comments")]
        public ActionResult Comments()
        {
            var viewModel = new CommentsViewModel();
            viewModel.CommentBanks = _context.ProfileCommentBanks.OrderBy(x => x.Name).ToList();

            return View("~/Views/Staff/Profile/Comments.cshtml", viewModel);
        }

        #endregion

        
        [Route("Home", Name = "StaffIndex")]
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();

            var staff = PeopleProcesses.GetStaffFromUserId(userId, _context);

            var academicYears = await CurriculumProcesses.GetAcademicYearsModel(_context);

            var selectedAcademicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            if (staff.ResponseType == ResponseType.NotFound)
                return View("~/Views/Staff/NoProfileIndex.cshtml");

            var viewModel = new StaffHomeViewModel
            {
                CurrentUser = PrepareResponseObject(staff),
                CurriculumAcademicYears = academicYears,
                SelectedAcademicYearId = selectedAcademicYearId
            };

            return View(viewModel);
        }
    }
}