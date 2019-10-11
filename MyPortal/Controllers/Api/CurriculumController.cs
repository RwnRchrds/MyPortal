using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models.Attributes;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/curriculum")]
    public class CurriculumController : MyPortalApiController
    {
        [HttpGet]
        [Route("academicYears/get/all", Name = "ApiCurriculumGetAcademicYears")]
        public IEnumerable<CurriculumAcademicYearDto> GetAcademicYears()
        {
            return PrepareResponseObject(CurriculumProcesses.GetAcademicYears(_context));
        }

        [HttpGet]
        [Route("academicYears/get/byId/{academicYearId:int}", Name = "ApiCurriculumGetAcademicYearById")]
        public CurriculumAcademicYearDto GetAcademicYearById([FromUri] int academicYearId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetAcademicYearById(academicYearId, _context));
        }

        [HttpPost]
        [RequiresPermission("ChangeAcademicYear")]
        [Route("academicYears/select", Name = "ApiCurriculumChangeSelectedAcademicYear")]
        public IHttpActionResult ChangeSelectedAcademicYear([FromBody] CurriculumAcademicYear year)
        {
            User.ChangeSelectedAcademicYear(year.Id);
            return Ok("Selected academic year changed");
        }
        
        [HttpGet]
        [RequiresPermission("ViewSessions")]
        [Route("sessions/get/byTeacher/{teacherId:int}/{date:datetime}", Name = "ApiCurriculumGetSessionsByTeacher")]
        public async Task<IEnumerable<CurriculumSessionDto>> GetSessionsByTeacher([FromUri] int teacherId, [FromUri] DateTime date)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(
                CurriculumProcesses.GetSessionsByTeacher(teacherId, academicYearId, date, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewSessions")]
        [Route("sessions/get/byTeacher/dataGrid/{teacherId:int}/{date:datetime}", Name = "ApiCurriculumGetSessionsByTeacherDataGrid")]
        public async Task<IHttpActionResult> GetSessionsByTeacherDataGrid([FromUri] int teacherId, [FromUri] DateTime date,
            [FromBody] DataManagerRequest dm)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var sessions =
                PrepareResponseObject(
                    CurriculumProcesses.GetSessionsByTeacher_DataGrid(teacherId, academicYearId, date, _context));

            return PrepareDataGridObject(sessions, dm);
        }

        [HttpGet]
        [RequiresPermission("ViewClasses")]
        [Route("classes/get/all", Name = "ApiCurriculumGetAllClasses")]
        public async Task<IEnumerable<CurriculumClassDto>> GetAllClasses()
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(CurriculumProcesses.GetAllClasses(academicYearId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewClasses")]
        [Route("classes/get/dataGrid/all", Name = "ApiCurriculumGetAllClassesDataGrid")]
        public async Task<IHttpActionResult> GetAllClassesDataGrid([FromBody] DataManagerRequest dm)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var classes = PrepareResponseObject(CurriculumProcesses.GetAllClassesDataGrid(academicYearId, _context));

            return PrepareDataGridObject(classes, dm);
        }

        [HttpGet]
        [RequiresPermission("ViewClasses")]
        [Route("classes/get/byId/{classId:int}", Name = "ApiCurriculumGetClassById")]
        public CurriculumClassDto GetClassById([FromUri] int classId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetClassById(classId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("classes/create", Name = "ApiCurriculumCreateClass")]
        public async Task<IHttpActionResult> CreateClass([FromBody] CurriculumClass @class)
        {
            @class.AcademicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponse(CurriculumProcesses.CreateClass(@class, _context));
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("classes/update", Name = "ApiCurriculumUpdateClass")]
        public IHttpActionResult UpdateClass([FromBody] CurriculumClass @class)
        {
            return PrepareResponse(CurriculumProcesses.UpdateClass(@class, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditClasses")]
        [Route("classes/delete/{classId:int}", Name = "ApiCurriculumDeleteClass")]
        public IHttpActionResult DeleteClass([FromUri] int classId)
        {
            return PrepareResponse(CurriculumProcesses.DeleteClass(classId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewSessions")]
        [Route("sessions/get/byClass/{classId:int}", Name = "ApiCurriculumGetSessionsByClass")]
        public IEnumerable<CurriculumSessionDto> GetSessionsByClass([FromUri] int classId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetSessionsForClass(classId, _context));
        }

        [RequiresPermission("ViewSessions")]
        [HttpPost]
        [Route("sessions/get/byClass/dataGrid/{classId:int}", Name = "ApiCurriculumGetSessionsByClassDataGrid")]
        public IHttpActionResult GetSessionsByClassDataGrid([FromUri] int classId, [FromBody] DataManagerRequest dm)
        {
            var sessions = PrepareResponseObject(CurriculumProcesses.GetSessionsForClass_DataGrid(classId, _context));

            return PrepareDataGridObject(sessions, dm);
        }

        [HttpGet]
        [RequiresPermission("ViewSessions")]
        [Route("sessions/get/byId/{sessionId:int}", Name = "ApiCurriculumGetSessionById")]
        public CurriculumSessionDto GetSessionById([FromUri] int sessionId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetSessionById(sessionId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("sessions/create", Name = "ApiCurriculumCreateSession")]
        public IHttpActionResult CreateSession([FromBody] CurriculumSession session)
        {
            return PrepareResponse(CurriculumProcesses.CreateSession(session, _context));
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("sessions/addRegPeriods", Name = "ApiCurriculumCreateSessionsForRegPeriods")]
        public IHttpActionResult CreateSessionsForRegPeriods([FromBody] CurriculumSession session)
        {
            return PrepareResponse(CurriculumProcesses.CreateSessionForRegPeriods(session, _context));
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("sessions/update", Name = "ApiCurriculumUpdateSession")]
        public IHttpActionResult UpdateSession([FromBody] CurriculumSession session)
        {
            return PrepareResponse(CurriculumProcesses.UpdateSession(session, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditClasses")]
        [Route("sessions/delete/{sessionId:int}", Name = "ApiCurriculumDeleteSession")]
        public IHttpActionResult DeleteSession([FromUri] int sessionId)
        {
            return PrepareResponse(CurriculumProcesses.DeleteSession(sessionId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byClass/{classId:int}", Name = "ApiCurriculumGetEnrolmentsByClass")]
        public IEnumerable<CurriculumEnrolmentDto> GetEnrolmentsByClass([FromUri] int classId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetEnrolmentsForClass(classId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byClass/dataGrid/{classId:int}", Name = "ApiCurriculumGetEnrolmentsByClassDataGrid")]
        public IHttpActionResult GetEnrolmentsByClassDataGrid([FromUri] int classId,
            [FromBody] DataManagerRequest dm)
        {
            var enrolments =
                PrepareResponseObject(CurriculumProcesses.GetEnrolmentsForClassDataGrid(classId, _context));

            return PrepareDataGridObject(enrolments, dm);
        }

        [HttpGet]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byStudent/{studentId:int}", Name = "ApiCurriculumGetEnrolmentsByStudent")]
        public IEnumerable<CurriculumEnrolmentDto> GetEnrolmentsByStudent([FromUri] int studentId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetEnrolmentsForStudent(studentId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byStudent/dataGrid/{studentId:int}", Name = "ApiCurriculumGetEnrolmentsByStudentDataGrid")]
        public IHttpActionResult GetEnrolmentsByStudentDataGrid([FromUri] int studentId,
            [FromBody] DataManagerRequest dm)
        {
            var enrolments =
                PrepareResponseObject(CurriculumProcesses.GetEnrolmentsForStudent_DataGrid(studentId, _context));

            return PrepareDataGridObject(enrolments, dm);
        }

        [HttpGet]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byId/{enrolmentId:int}", Name = "ApiCurriculumGetEnrolment")]
        public CurriculumEnrolmentDto GetEnrolment([FromUri] int enrolmentId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetEnrolmentById(enrolmentId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("enrolments/create", Name = "ApiCurriculumCreateEnrolment")]
        public IHttpActionResult CreateEnrolment([FromBody] CurriculumEnrolment enrolment)
        {
            return PrepareResponse(CurriculumProcesses.CreateEnrolment(enrolment, _context));
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("enrolments/create/group", Name = "ApiCurriculumEnrolRegGroup")]
        public IHttpActionResult EnrolRegGroup([FromBody] GroupEnrolment enrolment)
        {
            return PrepareResponse(CurriculumProcesses.CreateEnrolmentsForRegGroup(enrolment, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditClasses")]
        [Route("classes/enrolments/delete/{enrolmentId:int}", Name = "ApiCurriculumDeleteEnrolment")]
        public IHttpActionResult DeleteEnrolment([FromUri] int enrolmentId)
        {
            return PrepareResponse(CurriculumProcesses.DeleteEnrolment(enrolmentId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/new", Name = "ApiCurriculumCreateSubject")]
        public IHttpActionResult CreateSubject([FromBody] CurriculumSubject subject)
        {
            return PrepareResponse(CurriculumProcesses.CreateSubject(subject, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/delete/{subjectId:int}", Name = "ApiCurriculumDeleteSubject")]
        public IHttpActionResult DeleteSubject([FromUri] int subjectId)
        {
            return PrepareResponse(CurriculumProcesses.DeleteSubject(subjectId, _context));
        }

        [HttpGet]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/get/byId/{subjectId:int}", Name = "ApiCurriculumGetSubjectById")]
        public CurriculumSubjectDto GetSubjectById([FromUri] int subjectId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetSubjectById(subjectId, _context));
        }

        [HttpGet]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/get/all", Name = "ApiCurriculumGetAllSubjects")]
        public IEnumerable<CurriculumSubjectDto> GetAllSubjects()
        {
            return PrepareResponseObject(CurriculumProcesses.GetAllSubjects(_context));
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/get/dataGrid/all", Name = "ApiCurriculumGetAllSubjectsDataGrid")]
        public IHttpActionResult GetAllSubjectsDataGrid([FromBody] DataManagerRequest dm)
        {
            var subjects = PrepareResponseObject(CurriculumProcesses.GetAllSubjects_DataGrid(_context));

            return PrepareDataGridObject(subjects, dm);
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/update", Name = "ApiCurriculumUpdateSubject")]
        public IHttpActionResult UpdateSubject([FromBody] CurriculumSubject subject)
        {
            return PrepareResponse(CurriculumProcesses.UpdateSubject(subject, _context));
        }

        [HttpPost]
        [RequiresPermission("EditStudyTopics")]
        [Route("studyTopics/create", Name = "ApiCurriculumCreateStudyTopic")]
        public IHttpActionResult CreateStudyTopic([FromBody] CurriculumStudyTopic studyTopic)
        {
            return PrepareResponse(CurriculumProcesses.CreateStudyTopic(studyTopic, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditStudyTopics")]
        [Route("studyTopics/delete/{studyTopicId:int}", Name = "ApiCurriculumDeleteStudyTopic")]
        public IHttpActionResult DeleteStudyTopic([FromUri] int studyTopicId)
        {
            return PrepareResponse(CurriculumProcesses.DeleteStudyTopic(studyTopicId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudyTopics")]
        [Route("studyTopics/get/byId/{studyTopicId:int}", Name = "ApiCurriculumGetStudyTopicById")]
        public CurriculumStudyTopicDto GetStudyTopicById([FromUri] int studyTopicId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetStudyTopicById(studyTopicId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudyTopics")]
        [Route("studyTopics/get/all", Name = "ApiCurriculumGetAllStudyTopics")]
        public IEnumerable<CurriculumStudyTopicDto> GetAllStudyTopics()
        {
            return PrepareResponseObject(CurriculumProcesses.GetAllStudyTopics(_context));
        }

        [HttpPost]
        [RequiresPermission("ViewStudyTopics")]
        [Route("studyTopics/get/dataGrid/all", Name = "ApiCurriculumGetAllStudyTopicsDataGrid")]
        public IHttpActionResult GetAllStudyTopicsDataGrid([FromBody] DataManagerRequest dm)
        {
            var studyTopics = PrepareResponseObject(CurriculumProcesses.GetAllStudyTopicsDataGrid(_context));

            return PrepareDataGridObject(studyTopics, dm);
        }

        [HttpPost]
        [RequiresPermission("EditStudyTopics")]
        [Route("studyTopics/update", Name = "ApiCurriculumUpdateStudyTopic")]
        public IHttpActionResult UpdateStudyTopic([FromBody] CurriculumStudyTopic studyTopic)
        {
            return PrepareResponse(CurriculumProcesses.UpdateStudyTopic(studyTopic, _context));
        }
        
        [HttpGet]
        [RequiresPermission("ViewLessonPlans")]
        [Route("lessonPlans/get/all", Name = "ApiCurriculumGetAllLessonPlans")]
        public IEnumerable<CurriculumLessonPlanDto> GetAllLessonPlans()
        {
            return PrepareResponseObject(CurriculumProcesses.GetAllLessonPlans(_context));
        }
        
        [HttpGet]
        [RequiresPermission("ViewLessonPlans")]
        [Route("lessonPlans/get/byId/{lessonPlanId:int}", Name = "ApiCurriculumGetLessonPlanById")]
        public CurriculumLessonPlanDto GetLessonPlanById([FromUri] int lessonPlanId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetLessonPlanById(lessonPlanId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewLessonPlans")]
        [Route("lessonPlans/get/byTopic/{studyTopicId:int}", Name = "ApiCurriculumGetLessonPlansByTopic")]
        public IEnumerable<CurriculumLessonPlanDto> GetLessonPlansByTopic([FromUri] int studyTopicId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetLessonPlansByStudyTopic(studyTopicId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewLessonPlans")]
        [Route("lessonPlans/get/byTopic/dataGrid/{studyTopicId:int}", Name = "ApiCurriculumGetLessonPlansByStudyTopicDatagrid")]
        public IHttpActionResult GetLessonPlansByStudyTopicDataGrid([FromUri] int studyTopicId,
            [FromBody] DataManagerRequest dm)
        {
            var lessonPlans =
                PrepareResponseObject(CurriculumProcesses.GetLessonPlansByStudyTopic_DataGrid(studyTopicId, _context));

            return PrepareDataGridObject(lessonPlans, dm);
        }

        [HttpPost]
        [RequiresPermission("EditLessonPlans")]
        [Route("lessonPlans/create", Name = "ApiCurriculumCreateLessonPlan")]
        public IHttpActionResult CreateLessonPlan([FromBody] CurriculumLessonPlan plan)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(CurriculumProcesses.CreateLessonPlan(plan, userId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditLessonPlans")]
        [Route("lessonPlans/update", Name = "ApiCurriculumUpdateLessonPlan")]
        public IHttpActionResult UpdateLessonPlan([FromBody] CurriculumLessonPlan plan)
        {
            return PrepareResponse(CurriculumProcesses.UpdateLessonPlan(plan, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditLessonPlans")]
        [Route("lessonPlans/delete/{lessonPlanId:int}", Name = "ApiCurriculumDeleteLessonPlan")]
        public IHttpActionResult DeleteLessonPlan([FromUri] int lessonPlanId)
        {
            var userId = User.Identity.GetUserId();
            var canDeleteAll = User.HasPermission("DeleteAllLessonPlans");
            var staffId = PrepareResponseObject(PeopleProcesses.GetStaffFromUserId(userId, _context)).Id;

            return PrepareResponse(CurriculumProcesses.DeleteLessonPlan(lessonPlanId, staffId, canDeleteAll, _context));
        }
    }
}
