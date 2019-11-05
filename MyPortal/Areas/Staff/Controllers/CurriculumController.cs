using System.Threading.Tasks;
using System.Web.Mvc;
using MyPortal.Areas.Staff.ViewModels;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Services;

namespace MyPortal.Areas.Staff.Controllers
{
    [RoutePrefix("Curriculum")]
    [UserType(UserType.Staff)]
    public class CurriculumController : MyPortalController
    {
        [RequiresPermission("EditSubjects")]
        [Route("Curriculum/Subjects")]
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
        [Route("LessonPlans/View/{id}", Name = "CurriculumLessonPlanDetails")]
        public async Task<ActionResult> LessonPlanDetails(int id)
        {
            using (var curriculumService = new CurriculumService(UnitOfWork))
            {
                var lessonPlan = await curriculumService.GetLessonPlanById(id);

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
        [Route("Classes/Sessions/{classId:int}")]
        public async Task<ActionResult> Sessions(int classId)
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

        [RequiresPermission("EditClasses")]
        [Route("Classes/Enrolments/{classId:int}", Name = "CurriculumEnrolments")]
        public async Task<ActionResult> Enrolments(int classId)
        {
            using (var curriculumService = new CurriculumService(UnitOfWork))
            {
                var viewModel = new ClassEnrolmentsViewModel();

                var currClass = await curriculumService.GetClassById(classId);

                viewModel.Class = currClass;

                return View(viewModel);
            }
        }
    }
}