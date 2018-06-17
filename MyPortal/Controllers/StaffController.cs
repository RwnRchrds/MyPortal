using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Staff Landing Page
        public ActionResult Index()
        {
            var staffID = User.Identity.GetUserId();

            var staff = _context.Staff.SingleOrDefault(s => s.Id == staffID);

            var certificates = _context.TrainingCertificates.Where(c => c.Staff == staffID).ToList();

            var viewModel = new StaffHomeViewModel
            {
                CurrentUser = staff,
                TrainingCertificates = certificates
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
        [Authorize(Roles = "SeniorStaff")]
        public ActionResult Staff()
        {
            var staff = _context.Staff.ToList();
            return View(staff);
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

            var results = _context.Results.Where(r => r.Student == id && r.ResultSet1.IsCurrent).ToList();

            var logTypes = _context.LogTypes.ToList();

            var yearGroups = _context.YearGroups.ToList();

            var regGroups = _context.RegGroups.ToList();

            var resultSets = _context.ResultSets.ToList();

            var subjects = _context.Subjects.ToList();

            var upperSchool = student.YearGroup == 11 || student.YearGroup == 10;

            var chartData = GetChartData(results, upperSchool);

            var viewModel = new StudentDetailsViewModel
            {
                //Logs = logs,
                Student = student,
                Results = results,
                IsUpperSchool = upperSchool,
                ChartData = chartData,
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

        // Menu | Staff | X --> Student Details (for Staff X)
        //Accessible by [SeniorStaff] only
        [Authorize(Roles = "SeniorStaff")]
        [Route("Staff/Staff/{id}")]
        public ActionResult StaffDetails(string id)
        {
            var staff = _context.Staff.SingleOrDefault(s => s.Id == id);

            if (staff == null)
                return HttpNotFound();

            var certificates = _context.TrainingCertificates.Where(c => c.Staff == id).ToList();

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

        // Menu | Training Courses --> Training Courses List (All)
        //[Authorize(Roles = "SeniorStaff")]
        public ActionResult TrainingCourses()
        {
            return View();
        }

        // Menu | Training Courses | New Course --> New Course Form
        // Accessible by [SeniorStaff] only
        [Authorize(Roles = "SeniorStaff")]
        [Route("Staff/TrainingCourses/New")]
        public ActionResult NewCourse()
        {
            return View();
        }

        // Generates chart data for Student Details view
        public static ChartData GetChartData(List<Result> results, bool upperSchool)
        {
            var data = new ChartData();

            data.L1 = 0;
            data.L2 = 0;
            data.L3 = 0;
            data.L4 = 0;
            data.L5 = 0;
            data.L6 = 0;
            data.L7 = 0;
            data.L8 = 0;
            data.L9 = 0;

            if (upperSchool)
                foreach (var result in results)
                {
                    if (result.Value == "A*") data.L1++;
                    if (result.Value == "A") data.L2++;
                    if (result.Value == "B") data.L3++;
                    if (result.Value == "C") data.L4++;
                    if (result.Value == "D") data.L5++;
                    if (result.Value == "E") data.L6++;
                    if (result.Value == "F") data.L7++;
                    if (result.Value == "G") data.L8++;
                    if (result.Value == "U") data.L9++;
                }
            else
                foreach (var result in results)
                {
                    if (result.Value.Contains("E")) data.L1++;
                    if (result.Value.Contains("8")) data.L2++;
                    if (result.Value.Contains("7")) data.L3++;
                    if (result.Value.Contains("6")) data.L4++;
                    if (result.Value.Contains("5")) data.L5++;
                    if (result.Value.Contains("4")) data.L6++;
                    if (result.Value.Contains("3")) data.L7++;
                    if (result.Value.Contains("2")) data.L8++;
                    if (result.Value.Contains("1")) data.L9++;
                }

            return data;
        }

        // HTTP POST request for saving/creating logs using HTML form 
        // TODO: [REPLACE WITH AJAX REQUEST]
        [HttpPost]
        public ActionResult SaveLog(Log log)
        {
            if (log.Id == 0)
            {
                _context.Logs.Add(log);
            }

            else
            {
                var logInDb = _context.Logs.Single(l => l.Id == log.Id);

                logInDb.Author = log.Author;
                logInDb.Date = log.Date;
                logInDb.Message = log.Message;
            }

            _context.SaveChanges();
            return RedirectToAction("StudentDetails", "Staff", new {id = log.Student});
        }

        // HTTP POST request for creating certificates using HTML form
        [HttpPost]
        public ActionResult CreateCertificate(TrainingCertificate trainingCertificate)
        {
            _context.TrainingCertificates.Add(trainingCertificate);
            _context.SaveChanges();

            return RedirectToAction("StaffDetails", "Staff", new {id = trainingCertificate.Staff});
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(TrainingCourse course)
        {
            if (!ModelState.IsValid) return View("NewCourse");

            _context.TrainingCourses.Add(course);
            _context.SaveChanges();

            return RedirectToAction("TrainingCourses", "Staff");
        }

        // HTTP POST request for updating student details using HTML form
        [HttpPost]
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
    }
}