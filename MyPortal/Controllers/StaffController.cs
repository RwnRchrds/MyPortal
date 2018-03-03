using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using MyPortal.Models;
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    public class StaffController : Controller
    {
        private MyPortalDbContext _context;

        public StaffController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Students()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        public ActionResult Staff()
        {
            var staff = _context.Staff.ToList();
            return View(staff);
        }        

        [Route("Staff/Students/{id}")]
        public ActionResult StudentDetails(int id)
        {
            var logs = _context.Logs.Where(l => l.Student == id).ToList();

            var results = _context.Results.Where(r => r.Student == id && r.ResultSet1.IsCurrent == true).ToList();

            var student = _context.Students.SingleOrDefault(s => s.Id == id);

            var logTypes = _context.LogTypes.ToList();

            bool upperSchool = student.YearGroup == "Year 11" || student.YearGroup == "Year 10";

            var chartData = GetChartData(results,upperSchool);

            if (student == null)
                return HttpNotFound();

            var viewModel = new StudentDetailsViewModel
            {
                Logs = logs,
                Student = student,
                Results = results,
                IsUpperSchool = upperSchool,
                ChartData = chartData,
                LogTypes = logTypes
            };

            return View(viewModel);
        }

        [Route("Staff/Staff/{id}")]
        public ActionResult StaffDetails(string id)
        {
            var staff = _context.Staff.SingleOrDefault(s => s.Id == id);

            if (staff == null)
                return HttpNotFound();

            return View(staff);
        }

        public ChartData GetChartData(List<Result> results, bool upperSchool)
        {
            ChartData data = new ChartData();

            data.L1 = 0;
            data.L2 = 0;
            data.L3 = 0;
            data.L4 = 0;
            data.L5 = 0;
            data.L6 = 0;
            data.L7 = 0;
            data.L8 = 0;
            data.L9 = 0;

            if (upperSchool == true)
            {
                foreach (var result in results)
                {
                    if (result.Value == "A*")
                    {
                        data.L1++;
                    }
                    if (result.Value == "A")
                    {
                        data.L2++;
                    }
                    if (result.Value == "B")
                    {
                        data.L3++;
                    }
                    if (result.Value == "C")
                    {
                        data.L4++;
                    }
                    if (result.Value == "D")
                    {
                        data.L5++;
                    }
                    if (result.Value == "E")
                    {
                        data.L6++;
                    }
                    if (result.Value == "F")
                    {
                        data.L7++;
                    }
                    if (result.Value == "G")
                    {
                        data.L8++;
                    }
                    if (result.Value == "U")
                    {
                        data.L9++;
                    }
                }
            }
            else
            {
                foreach (var result in results)
                {
                    if (result.Value.Contains("E"))
                    {
                        data.L1++;
                    }
                    if (result.Value.Contains("8"))
                    {
                        data.L2++;
                    }
                    if (result.Value.Contains("7"))
                    {
                        data.L3++;
                    }
                    if (result.Value.Contains("6"))
                    {
                        data.L4++;
                    }
                    if (result.Value.Contains("5"))
                    {
                        data.L5++;
                    }
                    if (result.Value.Contains("4"))
                    {
                        data.L6++;
                    }
                    if (result.Value.Contains("3"))
                    {
                        data.L7++;
                    }
                    if (result.Value.Contains("2"))
                    {
                        data.L8++;
                    }
                    if (result.Value.Contains("1"))
                    {
                        data.L9++;
                    }
                }
            }

            return data;
        }
    }
}