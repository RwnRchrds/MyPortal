using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Models;
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

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
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

        // Menu | Students --> Students List (All)
        // Accessible by [Staff] or [SeniorStaff]
        public ActionResult Students()
        {
            return View();
        }

        // Menu | Staff --> Staff List (All)
        // Accessible by [SeniorStaff] only
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        public ActionResult Staff()
        {
            var viewModel = new NewStaffViewModel();
            return View(viewModel);
        }

        // Menu | Students | X --> Student Details (for Student X)
        //Accessible by [Staff] or [SeniorStaff]
        [System.Web.Mvc.Route("Staff/Students/{id}")]
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

            var upperSchool = student.YearGroupId == 11 || student.YearGroupId == 10;

            var viewModel = new StudentDetailsViewModel
            {
                //Logs = logs,
                Student = student,
                Results = results,
                IsUpperSchool = upperSchool,
                LogTypes = logTypes,
                YearGroups = yearGroups,
                RegGroups = regGroups,
                ResultSets = resultSets,
                Subjects = subjects
            };

            return View(viewModel);
        }

        //Menu | Students | X | [View Results] --> Student Results (for Student X)
        //Accessible by [Staff] or [SeniorStaff]
        [System.Web.Mvc.Route("Staff/Students/{id}/Results")]
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

        // Menu | Staff | X --> Student Details (for Staff X)
        //Accessible by [SeniorStaff] only
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Staff/Staff/{id}")]
        public ActionResult StaffDetails(int id)
        {
            var staff = _context.Staff.SingleOrDefault(s => s.Id == id);

            if (staff == null)
                return HttpNotFound();

            var certificates = _context.TrainingCertificates.Where(c => c.StaffId == id).ToList();

            var courses = _context.TrainingCourses.ToList();

            var statuses = _context.TrainingStatuses.ToList();

            var viewModel = new StaffDetailsViewModel
            {
                Staff = staff,
                TrainingCertificates = certificates,
                TrainingCourses = courses,
                TrainingStatuses = statuses
            };

            return View(viewModel);
        }

        // Menu | Students | New Student --> New Student form
        // Accessible by [SeniorStaff] only
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Staff/Students/New")]
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

        // Menu | Training Courses --> Training Courses List (All)
        //[Authorize(Roles = "SeniorStaff")]
        public ActionResult TrainingCourses()
        {
            return View();
        }

        // Menu | Training Courses | New Course --> New Course Form
        // Accessible by [SeniorStaff] only
        [System.Web.Mvc.Authorize(Roles = "SeniorStaff")]
        [System.Web.Mvc.Route("Staff/TrainingCourses/New")]
        public ActionResult NewCourse()
        {
            return View();
        }

        // Menu | Documents --> General Controlled Documents List (All)
        //Accessible by [Staff] or [SeniorStaff]
        public ActionResult Documents()
        {
            return View();
        }

        // HTTP POST request for saving/creating logs using HTML form 
        // TODO: [REPLACE WITH AJAX REQUEST]
        [System.Web.Mvc.HttpPost]
        public ActionResult SaveLog(Log log)
        {
            if (log.Id == 0)
            {
                _context.Logs.Add(log);
            }

            else
            {
                var logInDb = _context.Logs.Single(l => l.Id == log.Id);

                logInDb.AuthorId = log.AuthorId;
                logInDb.Date = log.Date;
                logInDb.Message = log.Message;
            }

            _context.SaveChanges();
            return RedirectToAction("StudentDetails", "Staff", new {id = log.Student});
        }

        // HTTP POST request for creating certificates using HTML form
        [System.Web.Mvc.HttpPost]
        public ActionResult CreateCertificate(TrainingCertificate trainingCertificate)
        {
            _context.TrainingCertificates.Add(trainingCertificate);
            _context.SaveChanges();

            return RedirectToAction("StaffDetails", "Staff", new {id = trainingCertificate.Staff});
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
                    RegGroups = _context.RegGroups.ToList(),
                    YearGroups = _context.YearGroups.ToList()
                };
                return View("NewStudent", viewModel);
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            return RedirectToAction("Students", "Staff");
        }

        // HTTP POST request for creating training courses using HTML form
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(TrainingCourse course)
        {
            if (!ModelState.IsValid) return View("NewCourse");

            _context.TrainingCourses.Add(course);
            _context.SaveChanges();

            return RedirectToAction("TrainingCourses", "Staff");
        }

        // HTTP POST request for updating student details using HTML form
        [System.Web.Mvc.HttpPost]
        public ActionResult SaveStudent(Student student)
        {
            var studentInDb = _context.Students.Single(l => l.Id == student.Id);

            studentInDb.FirstName = student.FirstName;
            studentInDb.LastName = student.LastName;
            studentInDb.YearGroup = student.YearGroup;
            studentInDb.RegGroup = student.RegGroup;

            _context.SaveChanges();
            return RedirectToAction("StudentDetails", "Staff", new {id = student.Id});
        }

        [System.Web.Mvc.Route("Staff/Data/Results/Import")]
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

        [System.Web.Mvc.HttpPost]
        public ActionResult UploadResults(HttpPostedFileBase file)
        {
            if (file.ContentLength <= 0 || Path.GetExtension(file.FileName) != ".csv")
                return RedirectToAction("ImportResults");
            const string path = @"C:/MyPortal/Files/Results/import.csv";
            file.SaveAs(path);

            return RedirectToAction("ImportResults");
        }
    }
}