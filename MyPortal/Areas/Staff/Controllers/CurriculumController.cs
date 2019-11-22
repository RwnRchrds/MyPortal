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
            using (var staffService = new StaffMemberService())
            {
                var viewModel = new SubjectsViewModel {Staff = await staffService.GetAllStaffMembers()};

                return View(viewModel);
            }
        }

        [RequiresPermission("EditSubjects")]
        [Route("Subjects/{subjectId:int}")]
        public async Task<ActionResult> SubjectDetails(int subjectId)
        {
            using (var staffService = new StaffMemberService())
            using (var curriculumService = new CurriculumService())
            {
                var viewModel = new SubjectDetailsViewModel
                {
                    Subject = await curriculumService.GetSubjectById(subjectId),
                    StaffMembers = await staffService.GetAllStaffMembersLookup(),
                    SubjectRoles = await curriculumService.GetAllSubjectRolesLookup()
                };

                return View(viewModel);
            }
        }

        
        [RequiresPermission("EditStudyTopics")]
        [Route("StudyTopics")]
        public async Task<ActionResult> StudyTopics()
        {
            using (var curriculumService = new CurriculumService())
            using (var pastoralService = new PastoralService())
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
            using (var curriculumService = new CurriculumService())
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
            using (var curriculumService = new CurriculumService())
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
            using (var staffService = new StaffMemberService())
            using (var curriculumService = new CurriculumService())
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
            using (var attendanceService = new AttendanceService())
            using (var curriculumService = new CurriculumService())
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