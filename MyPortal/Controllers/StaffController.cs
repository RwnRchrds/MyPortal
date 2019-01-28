using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Controllers.Api;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    //MyPortal Staff Controller --> Controller Methods for Staff Areas
    [Authorize(Roles = "Staff, SeniorStaff")]
    public class StaffController : Controller
    {
        private readonly MyPortalDbContext _context;

        public StaffController()
        {
            _context = new MyPortalDbContext();
        }        

        // HTTP POST request for creating training courses using HTML form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(TrainingCourse course)
        {
            if (!ModelState.IsValid) return View("NewCourse");

            _context.TrainingCourses.Add(course);
            _context.SaveChanges();

            return RedirectToAction("TrainingCourses", "Staff");
        }

        // HTTP POST request for creating students using HTML form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewStudentViewModel
                {
                    Student = student,
                    RegGroups = new RegGroupsController().GetRegGroups().Select(Mapper.Map<RegGroupDto, RegGroup>),
                    YearGroups = new YearGroupsController().GetYearGroups().Select(Mapper.Map<YearGroupDto, YearGroup>)
                };
                return View("NewStudent", viewModel);
            }

            _context.Students.Add(student);
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

        [Route("Staff/Data/Results/Import")]
        public ActionResult ImportResults()
        {
            var resultSets = _context.ResultSets.OrderBy(x => x.Name).ToList();
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

            var staff = _context.Staff.SingleOrDefault(s => s.UserId == userId);

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
        [Authorize(Roles = "SeniorStaff")]
        [Route("Staff/TrainingCourses/New")]
        public ActionResult NewCourse()
        {
            return View();
        }

        // Menu | Students | New Student --> New Student form
        // Accessible by [SeniorStaff] only
        [Authorize(Roles = "SeniorStaff")]
        [Route("Staff/Students/New")]
        public ActionResult NewStudent()
        {
            var yearGroups = _context.YearGroups.ToList();
            var regGroups = _context.RegGroups.ToList();

            var viewModel = new NewStudentViewModel
            {
                RegGroups = regGroups,
                YearGroups = yearGroups
            };

            return View(viewModel);
        }        

        // HTTP POST request for updating student details using HTML form
        [HttpPost]
        public ActionResult SaveStudent(Student student)
        {
            var studentInDb = _context.Students.Single(l => l.Id == student.Id);

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
        [Authorize(Roles = "SeniorStaff")]
        public ActionResult Staff()
        {
            var viewModel = new NewStaffViewModel();
            return View(viewModel);
        }

        // Menu | Staff | X --> Student Details (for Staff X)
        //Accessible by [SeniorStaff] only
        [Authorize(Roles = "SeniorStaff")]
        [Route("Staff/Staff/{id}")]
        public ActionResult StaffDetails(int id)
        {
            var staff = _context.Staff.SingleOrDefault(s => s.Id == id);

            if (staff == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();

            var currentStaffId = 0;

            var currentUser = _context.Staff.SingleOrDefault(x => x.UserId == userId);

            if (currentUser != null)
            {
                currentStaffId = currentUser.Id;
            }

            var certificates = _context.TrainingCertificates.Where(c => c.StaffId == id).ToList();

            var courses = _context.TrainingCourses.ToList();

            var statuses = _context.TrainingStatuses.ToList();

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
        [Route("Staff/Students/{id}")]
        public ActionResult StudentDetails(int id)
        {
            var student = _context.Students.SingleOrDefault(s => s.Id == id);

            if (student == null)
                return HttpNotFound();

            //var logs = _context.Logs.Where(l => l.Student == id).OrderByDescending(x => x.Date).ToList();

            var results = _context.Results.Where(r => r.StudentId == id && r.ResultSet.IsCurrent).ToList();

            var logTypes = _context.LogTypes.ToList();

            var yearGroups = _context.YearGroups.ToList();

            var regGroups = _context.RegGroups.ToList();

            var resultSets = _context.ResultSets.ToList();

            var subjects = _context.Subjects.ToList();

            var commentBanks = _context.CommentBanks.ToList();

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
        [Route("Staff/Students/{id}/Results")]
        public ActionResult StudentResults(int id)
        {
            var student = _context.Students.SingleOrDefault(s => s.Id == id);

            var currentResultSet = _context.ResultSets.SingleOrDefault(r => r.IsCurrent);

            var resultSets = _context.ResultSets.ToList();

            var subjects = _context.Subjects.ToList();

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

        [HttpPost]
        public ActionResult UploadResults(HttpPostedFileBase file)
        {
            if (file.ContentLength <= 0 || Path.GetExtension(file.FileName) != ".csv")
                return RedirectToAction("ImportResults");
            const string path = @"C:/MyPortal/Files/Results/import.csv";
            file.SaveAs(path);

            return RedirectToAction("ImportResults");
        }

        // Menu | Result Sets --> Result Sets List (All)
        [Authorize(Roles = "SeniorStaff")]
        [Route("Staff/Data/ResultSets")]
        public ActionResult ResultSets()
        {
            return View();
        }
        
        // Menu | Comment Banks --> Comment Banks List (All)
        [Authorize(Roles = "SeniorStaff")]
        [Route("Staff/Data/CommentBanks")]
        public ActionResult CommentBanks()
        {
            return View();
        }
        
        //Menu | Comments --> Comments List (All)
        [Authorize(Roles = "SeniorStaff")]
        [Route("Staff/Data/Comments")]
        public ActionResult Comments()
        {
            var viewModel = new CommentsViewModel();
            viewModel.CommentBanks = _context.CommentBanks.OrderBy(x => x.Name).ToList();

            return View(viewModel);
        }
        
        // Menu | Subjects --> Subjects List (All)
        [Authorize(Roles = "SeniorStaff")]
        [Route("Staff/Data/Subjects")]
        public ActionResult Subjects()
        {
            var viewModel = new SubjectsViewModel();
            viewModel.Staff = _context.Staff.OrderBy(x => x.LastName).ToList();

            return View(viewModel);
        }        

    }
}