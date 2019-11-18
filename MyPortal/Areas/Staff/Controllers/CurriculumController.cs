using System.Threading.Tasks;
using System.Web.Mvc;
using MyPortal.Areas.Staff.ViewModels;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Services;

namespace MyPortal.Areas.Staff.Controllers
{
    [UserType(UserType.Staff)]
    [RouteArea("Staff")]
    [RoutePrefix("Curriculum")]
    public class CurriculumController : MyPortalController
    {
        [RequiresPermission("EditSubjects")]
        [Route("Subjects")]
        public async Task<ActionResult> Subjects()
        {
            using (var staffService = new StaffMemberService(UnitOfWork))
            {
                var viewModel = new SubjectsViewModel {Staff = await staffService.GetAllStaffMembers()};

                return View(viewModel);
            }
        }

        
        [RequiresPermission("EditStudyTopics")]
        [Route("StudyTopics")]
        public async Task<ActionResult> StudyTopics()
        {
            using (var curriculumService = new CurriculumService(UnitOfWork))
            using (var pastoralService = new PastoralService(UnitOfWork))
            {
                var viewModel = new StudyTopicsViewModel();

                var subjects = await curriculumService.GetAllSubjects();

                var yearGroups = await pastoralService.GetAllYearGroups();

                viewModel.Subjects = subjects;
                viewModel.YearGroups = yearGroups;

                return View(viewModel);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewLessonPlans")]
        [Route("LessonPlans")]
        public async Task<ActionResult> LessonPlans()
        {
            using (var curriculumService = new CurriculumService(UnitOfWork))
            {
                var viewModel = new LessonPlansViewModel();

                var studyTopics = await curriculumService.GetAllStudyTopics();

                viewModel.StudyTopics = studyTopics;

                return View(viewModel);
            }
        }

        [RequiresPermission("ViewLessonPlans")]
        [Route("LessonPlans/View/{lessonPlanId:int}", Name = "CurriculumLessonPlanDetails")]
        public async Task<ActionResult> LessonPlanDetails(int lessonPlanId)
        {
            using (var curriculumService = new CurriculumService(UnitOfWork))
            {
                var lessonPlan = await curriculumService.GetLessonPlanById(lessonPlanId);

                var viewModel = new LessonPlanDetailsViewModel {LessonPlan = lessonPlan};

                return View(viewModel);
            }
        }

        [RequiresPermission("EditClasses")]
        [Route("Classes")]
        public async Task<ActionResult> Classes()
        {
            using (var staffService = new StaffMemberService(UnitOfWork))
            using (var curriculumService = new CurriculumService(UnitOfWork))
            {
                var viewModel = new ClassesViewModel
                {
                    Staff = await staffService.GetAllStaffMembers(),
                    Subjects = await curriculumService.GetAllSubjects()
                };

                return View(viewModel);
            }
        }

        [RequiresPermission("EditClasses")]
        [Route("Classes/{classId:int}")]
        public async Task<ActionResult> ClassDetails(int classId)
        {
            using (var attendanceService = new AttendanceService(UnitOfWork))
            using (var curriculumService = new CurriculumService(UnitOfWork))
            {
                var viewModel = new SessionsViewModel();

                var currClass = await curriculumService.GetClassById(classId);

                viewModel.Class = currClass;

                viewModel.Periods = await attendanceService.GetAllPeriods();

                return View(viewModel);
            }
        }
    }
}