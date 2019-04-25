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
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    //MyPortal Staff Controller --> Controller Methods for Staff Areas
    [System.Web.Mvc.Authorize(Roles = "Staff, SeniorStaff")]
    public class StaffController : Controller
    {
        private readonly MyPortalDbContext _context;

        public StaffController()
        {
            _context = new MyPortalDbContext();
        }        

        // HTTP POST request for creating training courses using HTML form
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(PersonnelTrainingCourse course)
        {
            if (!ModelState.IsValid) return View("NewCourse");

            _context.PersonnelTrainingCourses.Add(course);
            _context.SaveChanges();

            return RedirectToAction("TrainingCourses", "Staff");
        }

        // HTTP POST request for creating students using HTML form
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent(CoreStudent student)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewStudentViewModel
                {
                    Student = student,
                    RegGroups = new RegGroupsController().GetRegGroups().ToList().Select(Mapper.Map<PastoralRegGroupDto, PastoralRegGroup>),
                    YearGroups = new YearGroupsController().GetYearGroups().ToList().Select(Mapper.Map<PastoralYearGroupDto, PastoralYearGroup>)
                };
                return View("NewStudent", viewModel);
            }

            _context.CoreStudents.Add(student);
            _context.SaveChanges();

            return RedirectToAction("Students", "Staff");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Menu | Documents --> General Controlled Documents List (All)
        //Accessible by [Staff] or [SeniorStaff]
        public ActionResult Documents()
        {
            return View();
        }

        [System.Web.Mvc.Route("Staff/Data/Results/Import")]
        public ActionResult ImportResults()
        {
            var resultSets = _context.AssessmentResultSets.OrderBy(x => x.Name).ToList();
            var fileExists = System.IO.File.Exists(@"C:/MyPortal/Files/Results/import.csv");
            var viewModel = new ImportResultsViewModel
            {
                ResultSets = resultSets,
                FileExists = fileExists
            };

            return View(viewModel);
        }

        // Staff Landing Page
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var staff = _context.CoreStaff.SingleOrDefault(s => s.UserId == userId);

            if (staff == null)
                return View("~/Views/Staff/NoProfileIndex.cshtml");

            var viewModel = new StaffHomeViewModel
            {
                CurrentUser = staff
            };

            return View(viewModel);
        }

        // Menu | Training Courses | New Course --> New Course Form
        // Accessible by [SeniorStaff] only
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Staff/TrainingCourses/New")]
        public ActionResult NewCourse()
        {
            return View();
        }

        // Menu | Students | New Student --> New Student form
        // Accessible by [SeniorStaff] only
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Staff/Students/New")]
        public ActionResult NewStudent()
        {
            var yearGroups = _context.PastoralYearGroups.ToList();
            var regGroups = _context.PastoralRegGroups.ToList();

            var viewModel = new NewStudentViewModel
            {
                RegGroups = regGroups,
                YearGroups = yearGroups
            };

            return View(viewModel);
        }        

        // HTTP POST request for updating student details using HTML form
        [System.Web.Mvc.HttpPost]
        public ActionResult SaveStudent(CoreStudent student)
        {
            var studentInDb = _context.CoreStudents.Single(l => l.Id == student.Id);

            studentInDb.FirstName = student.FirstName;
            studentInDb.LastName = student.LastName;
            studentInDb.Gender = student.Gender;            
            studentInDb.YearGroupId = student.YearGroupId;
            studentInDb.RegGroupId = student.RegGroupId;

            _context.SaveChanges();
            return RedirectToAction("StudentDetails", "Staff", new {id = student.Id});
        }

        // Menu | Staff --> Staff List (All)
        // Accessible by [SeniorStaff] only
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        public ActionResult Staff()
        {
            var viewModel = new NewStaffViewModel();
            return View(viewModel);
        }

        // Menu | Staff | X --> Student Details (for Staff X)
        //Accessible by [SeniorStaff] only
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Staff/Staff/{id}")]
        public ActionResult StaffDetails(int id)
        {
            var staff = _context.CoreStaff.SingleOrDefault(s => s.Id == id);

            if (staff == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();

            var currentStaffId = 0;

            var currentUser = _context.CoreStaff.SingleOrDefault(x => x.UserId == userId);

            if (currentUser != null)
            {
                currentStaffId = currentUser.Id;
            }

            var certificates = _context.PersonnelTrainingCertificates.Where(c => c.StaffId == id).ToList();

            var courses = _context.PersonnelTrainingCourses.ToList();

            var statuses = _context.PersonnelTrainingStatuses.ToList();

            var viewModel = new StaffDetailsViewModel
            {
                Staff = staff,
                TrainingCertificates = certificates,
                TrainingCourses = courses,
                TrainingStatuses = statuses,
                CurrentStaffId = currentStaffId
            };

            return View(viewModel);
        }

        // Menu | Students | X --> Student Details (for Student X)
        //Accessible by [Staff] or [SeniorStaff]
        [System.Web.Mvc.Route("Staff/Students/{id}")]
        public ActionResult StudentDetails(int id)
        {
            var student = _context.CoreStudents.SingleOrDefault(s => s.Id == id);

            if (student == null)
                return HttpNotFound();

            //var logs = _context.Logs.Where(l => l.Student == id).OrderByDescending(x => x.Date).ToList();

            var results = _context.AssessmentResults.Where(r => r.StudentId == id && r.AssessmentResultSet.IsCurrent).ToList();

            var logTypes = _context.ProfileLogTypes.OrderBy(x => x.Name).ToList();

            var yearGroups = _context.PastoralYearGroups.OrderBy(x => x.Name).ToList();

            var regGroups = _context.PastoralRegGroups.OrderBy(x => x.Name).ToList();

            var resultSets = _context.AssessmentResultSets.OrderBy(x => x.Name).ToList();

            var subjects = _context.CurriculumSubjects.OrderBy(x => x.Name).ToList();

            var commentBanks = _context.ProfileCommentBanks.OrderBy(x => x.Name).ToList();

            var viewModel = new StudentDetailsViewModel
            {
                //Logs = logs,
                Student = student,
                Results = results,                
                LogTypes = logTypes,
                YearGroups = yearGroups,
                RegGroups = regGroups,
                ResultSets = resultSets,
                Subjects = subjects,
                CommentBanks = commentBanks
            };

            return View(viewModel);
        }

        //Menu | Students | X | [View Results] --> Student Results (for Student X)
        //Accessible by [Staff] or [SeniorStaff]
        [System.Web.Mvc.Route("Staff/Students/{id}/Results")]
        public ActionResult StudentResults(int id)
        {
            var student = _context.CoreStudents.SingleOrDefault(s => s.Id == id);

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

            return View(viewModel);
        }

        // Menu | Students --> Students List (All)
        // Accessible by [Staff] or [SeniorStaff]
        public ActionResult Students()
        {
            return View();
        }

        // Menu | Training Courses --> Training Courses List (All)
        //[Authorize(Roles = "SeniorStaff")]
        public ActionResult TrainingCourses()
        {
            return View();
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
        [System.Web.Mvc.Route("Staff/Data/ResultSets")]
        public ActionResult ResultSets()
        {
            return View();
        }
        
        // Menu | Comment Banks --> Comment Banks List (All)
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Staff/Data/CommentBanks")]
        public ActionResult CommentBanks()
        {
            return View();
        }
        
        //Menu | Comments --> Comments List (All)
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Staff/Data/Comments")]
        public ActionResult Comments()
        {
            var viewModel = new CommentsViewModel();
            viewModel.CommentBanks = _context.ProfileCommentBanks.OrderBy(x => x.Name).ToList();

            return View(viewModel);
        }
        
        // Menu | Subjects --> Subjects List (All)
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Staff/Data/Subjects")]
        public ActionResult Subjects()
        {
            var viewModel = new SubjectsViewModel();
            viewModel.Staff = _context.CoreStaff.OrderBy(x => x.LastName).ToList();

            return View(viewModel);
        }
        
        // Menu | Study Topics --> Study Topics List (All)
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Staff/Data/StudyTopics")]
        public ActionResult StudyTopics()
        {
            var viewModel = new StudyTopicsViewModel();

            var subjects = _context.CurriculumSubjects.OrderBy(x => x.Name).ToList();

            var yearGroups = _context.PastoralYearGroups.OrderBy(x => x.Name).ToList();

            viewModel.Subjects = subjects;
            viewModel.YearGroups = yearGroups;

            return View(viewModel);
        }
        
        //Menu | Lesson Plans --> Lesson Plans List (All)
        [System.Web.Mvc.Route("Staff/Curriculum/LessonPlans")]
        public ActionResult LessonPlans()
        {
            var viewModel = new LessonPlansViewModel();

            var studyTopics = _context.CurriculumStudyTopics.OrderBy(x => x.Name).ToList();

            viewModel.StudyTopics = studyTopics;

            return View(viewModel);
        }
        
        //Menu | Lesson Plans | X --> Lesson Plan Details for Lesson Plan X
        [System.Web.Mvc.Route("Staff/Curriculum/LessonPlans/View/{id}")]
        public ActionResult LessonPlanDetails(int id)
        {
            var lessonPlan = _context.CurriculumLessonPlans.SingleOrDefault(x => x.Id == id);

            if (lessonPlan == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            var viewModel = new LessonPlanDetailsViewModel();

            viewModel.LessonPlan = lessonPlan;

            return View(viewModel);
        }

    }
}