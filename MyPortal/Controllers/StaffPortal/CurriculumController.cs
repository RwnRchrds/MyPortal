using System.Collections.Generic;
using System.Web.Mvc;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Models;
using MyPortal.ViewModels;

namespace MyPortal.Controllers.StaffPortal
{
    [System.Web.Http.RoutePrefix("Staff/Curriculum")]
    [UserType(UserType.Staff)]
    public class CurriculumController : MyPortalController
    {
        [RequiresPermission("EditSubjects")]
        [System.Web.Http.Route("Curriculum/Subjects")]
        public ActionResult Subjects()
        {
            var viewModel = new SubjectsViewModel();
            viewModel.Staff = _context.StaffMembers.OrderBy(x => x.Person.LastName).ToList();

            return View("~/Views/Staff/Curriculum/Subjects.cshtml", viewModel);
        }

        
        [RequiresPermission("EditStudyTopics")]
        [System.Web.Http.Route("Curriculum/StudyTopics")]
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
        [System.Web.Http.Route("Curriculum/LessonPlans")]
        public ActionResult LessonPlans()
        {
            var viewModel = new LessonPlansViewModel();

            var studyTopics = _context.CurriculumStudyTopics.OrderBy(x => x.Name).ToList();

            viewModel.StudyTopics = studyTopics;

            return View("~/Views/Staff/Curriculum/LessonPlans.cshtml", viewModel);
        }

        [RequiresPermission("ViewLessonPlans")]
        [System.Web.Http.Route("Curriculum/LessonPlans/View/{id}", Name = "CurriculumLessonPlanDetails")]
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
        [System.Web.Http.Route("Curriculum/Classes")]
        public ActionResult Classes()
        {
            var viewModel = new ClassesViewModel();

            viewModel.Staff = _context.StaffMembers.ToList().OrderBy(x => x.Person.LastName);

            viewModel.Subjects = _context.CurriculumSubjects.ToList().OrderBy(x => x.Name);

            return View("~/Views/Staff/Curriculum/Classes.cshtml", viewModel);
        }

        [RequiresPermission("EditClasses")]
        [System.Web.Http.Route("Curriculum/Classes/Sessions/{classId:int}")]
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
        [System.Web.Http.Route("Curriculum/Classes/Enrolments/{classId:int}", Name = "CurriculumEnrolments")]
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
    }
}