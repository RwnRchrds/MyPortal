using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/curriculum")]
    public class CurriculumController : MyPortalApiController
    {
        protected readonly CurriculumService _service;

        public CurriculumController()
        {
            _service = new CurriculumService(UnitOfWork);
        }
        
        [HttpGet]
        [Route("academicYears/get/all", Name = "ApiCurriculumGetAcademicYears")]
        public async Task<IEnumerable<CurriculumAcademicYearDto>> GetAcademicYears()
        {
            try
            {
                var academicYears = await _service.GetAcademicYears();

                return academicYears.Select(Mapper.Map<CurriculumAcademicYear, CurriculumAcademicYearDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [Route("academicYears/get/byId/{academicYearId:int}", Name = "ApiCurriculumGetAcademicYearById")]
        public async Task<CurriculumAcademicYearDto> GetAcademicYearById([FromUri] int academicYearId)
        {
            try
            {
                var academicYear = await _service.GetAcademicYearById(academicYearId);

                return Mapper.Map<CurriculumAcademicYear, CurriculumAcademicYearDto>(academicYear);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ChangeAcademicYear")]
        [Route("academicYears/select", Name = "ApiCurriculumChangeSelectedAcademicYear")]
        public async Task<IHttpActionResult> ChangeSelectedAcademicYear([FromBody] CurriculumAcademicYear year)
        {
            using (var userService = new UserService(UnitOfWork))
            {
                var userId = User.Identity.GetUserId();
                await userService.ChangeSelectedAcademicYear(userId, year.Id);
            }
            
            return Ok("Selected academic year changed");
        }

        [HttpGet]
        [RequiresPermission("ViewSessions")]
        [Route("sessions/get/byTeacherAndDate/{teacherId:int}/{date:datetime}", Name = "ApiCurriculumGetSessionsByTeacherAndDate")]
        public async Task<IEnumerable<CurriculumSessionDto>> GetSessionsByTeacherOnDayOfWeek([FromUri] int teacherId, [FromUri] DateTime date)
        {
            try
            {
                var academicYearId = await _service.GetCurrentOrSelectedAcademicYearId(User);

                var sessions = await _service.GetSessionsByDate(teacherId, academicYearId, date);

                return sessions.Select(Mapper.Map<CurriculumSession, CurriculumSessionDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewSessions")]
        [Route("sessions/get/byTeacherAndDate/dataGrid/{teacherId:int}/{date:datetime}", Name = "ApiCurriculumGetSessionsByTeacherAndDateDataGrid")]
        public async Task<IHttpActionResult> GetSessionsByTeacherDataGrid([FromUri] int teacherId, [FromUri] DateTime date,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var academicYearId = await _service.GetCurrentOrSelectedAcademicYearId(User);

                var sessions = await _service.GetSessionsByDate(teacherId, academicYearId, date);

                var list = sessions.Select(Mapper.Map<CurriculumSession, GridCurriculumSessionDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewClasses")]
        [Route("classes/get/all", Name = "ApiCurriculumGetAllClasses")]
        public async Task<IEnumerable<CurriculumClassDto>> GetAllClasses()
        {
            try
            {
                var academicYearId = await _service.GetCurrentOrSelectedAcademicYearId(User);
                var classes = await _service.GetAllClasses(academicYearId);

                return classes.Select(Mapper.Map<CurriculumClass, CurriculumClassDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewClasses")]
        [Route("classes/get/dataGrid/all", Name = "ApiCurriculumGetAllClassesDataGrid")]
        public async Task<IHttpActionResult> GetAllClassesDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var academicYearId = await _service.GetCurrentOrSelectedAcademicYearId(User);

                var classes = await _service.GetAllClasses(academicYearId);

                var list = classes.Select(Mapper.Map<CurriculumClass, GridCurriculumClassDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewClasses")]
        [Route("classes/get/byId/{classId:int}", Name = "ApiCurriculumGetClassById")]
        public async Task<CurriculumClassDto> GetClassById([FromUri] int classId)
        {
            try
            {
                var @class = await _service.GetClassById(classId);

                return Mapper.Map<CurriculumClass, CurriculumClassDto>(@class);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("classes/create", Name = "ApiCurriculumCreateClass")]
        public async Task<IHttpActionResult> CreateClass([FromBody] CurriculumClass @class)
        {
            try
            {
                var academicYearId = await _service.GetCurrentOrSelectedAcademicYearId(User);

                @class.AcademicYearId = academicYearId;

                await _service.CreateClass(@class);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Class created");
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("classes/update", Name = "ApiCurriculumUpdateClass")]
        public async Task<IHttpActionResult> UpdateClass([FromBody] CurriculumClass @class)
        {
            try
            {
                await _service.UpdateClass(@class);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Class updated");
        }

        [HttpDelete]
        [RequiresPermission("EditClasses")]
        [Route("classes/delete/{classId:int}", Name = "ApiCurriculumDeleteClass")]
        public async Task<IHttpActionResult> DeleteClass([FromUri] int classId)
        {
            try
            {
                await _service.DeleteClass(classId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Class deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewSessions")]
        [Route("sessions/get/byClass/{classId:int}", Name = "ApiCurriculumGetSessionsByClass")]
        public async Task<IEnumerable<CurriculumSessionDto>> GetSessionsByClass([FromUri] int classId)
        {
            try
            {
                var sessions = await _service.GetSessionsByClass(classId);

                return sessions.Select(Mapper.Map<CurriculumSession, CurriculumSessionDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [RequiresPermission("ViewSessions")]
        [HttpPost]
        [Route("sessions/get/byClass/dataGrid/{classId:int}", Name = "ApiCurriculumGetSessionsByClassDataGrid")]
        public async Task<IHttpActionResult> GetSessionsByClassDataGrid([FromUri] int classId, [FromBody] DataManagerRequest dm)
        {
            try
            {
                var sessions = await _service.GetSessionsByClass(classId);

                var list = sessions.Select(Mapper.Map<CurriculumSession, GridCurriculumSessionDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewSessions")]
        [Route("sessions/get/byId/{sessionId:int}", Name = "ApiCurriculumGetSessionById")]
        public async Task<CurriculumSessionDto> GetSessionById([FromUri] int sessionId)
        {
            try
            {
                var session = await _service.GetSessionById(sessionId);

                return Mapper.Map<CurriculumSession, CurriculumSessionDto>(session);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("sessions/create", Name = "ApiCurriculumCreateSession")]
        public async Task<IHttpActionResult> CreateSession([FromBody] CurriculumSession session)
        {
            try
            {
                await _service.CreateSession(session);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Session created");
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("sessions/addRegPeriods", Name = "ApiCurriculumCreateSessionsForRegPeriods")]
        public async Task<IHttpActionResult> CreateSessionsForRegPeriods([FromBody] CurriculumSession session)
        {
            try
            {
                await _service.CreateSessionForRegPeriods(session);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Sessions created");
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("sessions/update", Name = "ApiCurriculumUpdateSession")]
        public async Task<IHttpActionResult> UpdateSession([FromBody] CurriculumSession session)
        {
            try
            {
                await _service.UpdateSession(session);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Session updated");
        }

        [HttpDelete]
        [RequiresPermission("EditClasses")]
        [Route("sessions/delete/{sessionId:int}", Name = "ApiCurriculumDeleteSession")]
        public async Task<IHttpActionResult> DeleteSession([FromUri] int sessionId)
        {
            try
            {
                await _service.DeleteSession(sessionId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Session deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byClass/{classId:int}", Name = "ApiCurriculumGetEnrolmentsByClass")]
        public async Task<IEnumerable<CurriculumEnrolmentDto>> GetEnrolmentsByClass([FromUri] int classId)
        {
            try
            {
                var enrolments = await _service.GetEnrolmentsByClass(classId);

                return enrolments.Select(Mapper.Map<CurriculumEnrolment, CurriculumEnrolmentDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byClass/dataGrid/{classId:int}", Name = "ApiCurriculumGetEnrolmentsByClassDataGrid")]
        public async Task<IHttpActionResult> GetEnrolmentsByClassDataGrid([FromUri] int classId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var enrolments = await _service.GetEnrolmentsByClass(classId);

                var list = enrolments.Select(Mapper.Map<CurriculumEnrolment, GridCurriculumEnrolmentDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byStudent/{studentId:int}", Name = "ApiCurriculumGetEnrolmentsByStudent")]
        public async Task<IEnumerable<CurriculumEnrolmentDto>> GetEnrolmentsByStudent([FromUri] int studentId)
        {
            try
            {
                var enrolments = await _service.GetEnrolmentsByStudent(studentId);

                return enrolments.Select(Mapper.Map<CurriculumEnrolment, CurriculumEnrolmentDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byStudent/dataGrid/{studentId:int}", Name = "ApiCurriculumGetEnrolmentsByStudentDataGrid")]
        public async Task<IHttpActionResult> GetEnrolmentsByStudentDataGrid([FromUri] int studentId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var enrolments = await _service.GetEnrolmentsByStudent(studentId);

                var list = enrolments.Select(Mapper.Map<CurriculumEnrolment, GridCurriculumEnrolmentDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byId/{enrolmentId:int}", Name = "ApiCurriculumGetEnrolment")]
        public async Task<CurriculumEnrolmentDto> GetEnrolmentById([FromUri] int enrolmentId)
        {
            try
            {
                var enrolment = await _service.GetEnrolmentById(enrolmentId);

                return Mapper.Map<CurriculumEnrolment, CurriculumEnrolmentDto>(enrolment);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("enrolments/create", Name = "ApiCurriculumCreateEnrolment")]
        public async Task<IHttpActionResult> CreateEnrolment([FromBody] CurriculumEnrolment enrolment)
        {
            try
            {
                await _service.CreateEnrolment(enrolment);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Enrolment created");
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("enrolments/create/group", Name = "ApiCurriculumEnrolRegGroup")]
        public async Task<IHttpActionResult> EnrolRegGroup([FromBody] GroupEnrolment enrolment)
        {
            try
            {
                await _service.CreateEnrolmentsForRegGroup(enrolment);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Group enrolled");
        }

        [HttpDelete]
        [RequiresPermission("EditClasses")]
        [Route("classes/enrolments/delete/{enrolmentId:int}", Name = "ApiCurriculumDeleteEnrolment")]
        public async Task<IHttpActionResult> DeleteEnrolment([FromUri] int enrolmentId)
        {
            try
            {
                await _service.DeleteEnrolment(enrolmentId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Enrolment deleted");
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/new", Name = "ApiCurriculumCreateSubject")]
        public async Task<IHttpActionResult> CreateSubject([FromBody] CurriculumSubject subject)
        {
            try
            {
                await _service.CreateSubject(subject);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Subject created");
        }

        [HttpDelete]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/delete/{subjectId:int}", Name = "ApiCurriculumDeleteSubject")]
        public async Task<IHttpActionResult> DeleteSubject([FromUri] int subjectId)
        {
            try
            {
                await _service.DeleteSubject(subjectId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Subject deleted");
        }

        [HttpGet]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/get/byId/{subjectId:int}", Name = "ApiCurriculumGetSubjectById")]
        public async Task<CurriculumSubjectDto> GetSubjectById([FromUri] int subjectId)
        {
            try
            {
                var subject = await _service.GetSubjectById(subjectId);

                return Mapper.Map<CurriculumSubject, CurriculumSubjectDto>(subject);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/get/all", Name = "ApiCurriculumGetAllSubjects")]
        public async Task<IEnumerable<CurriculumSubjectDto>> GetAllSubjects()
        {
            try
            {
                var subjects = await _service.GetAllSubjects();

                return subjects.Select(Mapper.Map<CurriculumSubject, CurriculumSubjectDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/get/dataGrid/all", Name = "ApiCurriculumGetAllSubjectsDataGrid")]
        public async Task<IHttpActionResult> GetAllSubjectsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var subjects = await _service.GetAllSubjects();

                var list = subjects.Select(Mapper.Map<CurriculumSubject, GridCurriculumSubjectDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/update", Name = "ApiCurriculumUpdateSubject")]
        public async Task<IHttpActionResult> UpdateSubject([FromBody] CurriculumSubject subject)
        {
            try
            {
                await _service.UpdateSubject(subject);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Subject updated");
        }

        [HttpPost]
        [RequiresPermission("EditStudyTopics")]
        [Route("studyTopics/create", Name = "ApiCurriculumCreateStudyTopic")]
        public async Task<IHttpActionResult> CreateStudyTopic([FromBody] CurriculumStudyTopic studyTopic)
        {
            try
            {
                await _service.UpdateStudyTopic(studyTopic);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Study topic created");
        }

        [HttpDelete]
        [RequiresPermission("EditStudyTopics")]
        [Route("studyTopics/delete/{studyTopicId:int}", Name = "ApiCurriculumDeleteStudyTopic")]
        public async Task<IHttpActionResult> DeleteStudyTopic([FromUri] int studyTopicId)
        {
            try
            {
                await _service.DeleteStudyTopic(studyTopicId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Study topic deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewStudyTopics")]
        [Route("studyTopics/get/byId/{studyTopicId:int}", Name = "ApiCurriculumGetStudyTopicById")]
        public async Task<CurriculumStudyTopicDto> GetStudyTopicById([FromUri] int studyTopicId)
        {
            try
            {
                var studyTopic = await _service.GetStudyTopicById(studyTopicId);

                return Mapper.Map<CurriculumStudyTopic, CurriculumStudyTopicDto>(studyTopic);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewStudyTopics")]
        [Route("studyTopics/get/all", Name = "ApiCurriculumGetAllStudyTopics")]
        public async Task<IEnumerable<CurriculumStudyTopicDto>> GetAllStudyTopics()
        {
            try
            {
                var studyTopics = await _service.GetAllStudyTopics();

                return studyTopics.Select(Mapper.Map<CurriculumStudyTopic, CurriculumStudyTopicDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewStudyTopics")]
        [Route("studyTopics/get/dataGrid/all", Name = "ApiCurriculumGetAllStudyTopicsDataGrid")]
        public async Task<IHttpActionResult> GetAllStudyTopicsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var studyTopics = await _service.GetAllStudyTopics();

                var list = studyTopics.Select(Mapper.Map<CurriculumStudyTopic, GridCurriculumStudyTopicDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditStudyTopics")]
        [Route("studyTopics/update", Name = "ApiCurriculumUpdateStudyTopic")]
        public async Task<IHttpActionResult> UpdateStudyTopic([FromBody] CurriculumStudyTopic studyTopic)
        {
            try
            {
                await _service.UpdateStudyTopic(studyTopic);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Study topic updated");
        }

        [HttpGet]
        [RequiresPermission("ViewLessonPlans")]
        [Route("lessonPlans/get/all", Name = "ApiCurriculumGetAllLessonPlans")]
        public async Task<IEnumerable<CurriculumLessonPlanDto>> GetAllLessonPlans()
        {
            try
            {
                var lessonPlans = await _service.GetAllLessonPlans();

                return lessonPlans.Select(Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewLessonPlans")]
        [Route("lessonPlans/get/byId/{lessonPlanId:int}", Name = "ApiCurriculumGetLessonPlanById")]
        public async Task<CurriculumLessonPlanDto> GetLessonPlanById([FromUri] int lessonPlanId)
        {
            try
            {
                var lessonPlan = await _service.GetLessonPlanById(lessonPlanId);

                return Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>(lessonPlan);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewLessonPlans")]
        [Route("lessonPlans/get/byTopic/{studyTopicId:int}", Name = "ApiCurriculumGetLessonPlansByTopic")]
        public async Task<IEnumerable<CurriculumLessonPlanDto>> GetLessonPlansByStudyTopic([FromUri] int studyTopicId)
        {
            try
            {
                var lessonPlans = await _service.GetLessonPlansByStudyTopic(studyTopicId);

                return lessonPlans.Select(Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewLessonPlans")]
        [Route("lessonPlans/get/byTopic/dataGrid/{studyTopicId:int}", Name = "ApiCurriculumGetLessonPlansByStudyTopicDatagrid")]
        public async Task<IHttpActionResult> GetLessonPlansByStudyTopicDataGrid([FromUri] int studyTopicId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var lessonPlans = await _service.GetLessonPlansByStudyTopic(studyTopicId);

                var list = lessonPlans.Select(Mapper.Map<CurriculumLessonPlan, GridCurriculumLessonPlanDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditLessonPlans")]
        [Route("lessonPlans/create", Name = "ApiCurriculumCreateLessonPlan")]
        public async Task<IHttpActionResult> CreateLessonPlan([FromBody] CurriculumLessonPlan plan)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                await _service.CreateLessonPlan(plan, userId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Lesson plan created");
        }

        [HttpPost]
        [RequiresPermission("EditLessonPlans")]
        [Route("lessonPlans/update", Name = "ApiCurriculumUpdateLessonPlan")]
        public async Task<IHttpActionResult> UpdateLessonPlan([FromBody] CurriculumLessonPlan plan)
        {
            try
            {
                await _service.UpdateLessonPlan(plan);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Lesson plan updated");
        }

        [HttpDelete]
        [RequiresPermission("EditLessonPlans")]
        [Route("lessonPlans/delete/{lessonPlanId:int}", Name = "ApiCurriculumDeleteLessonPlan")]
        public async Task<IHttpActionResult> DeleteLessonPlan([FromUri] int lessonPlanId)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                var canDeleteAll = User.HasPermission("DeleteAllLessonPlans");

                await _service.DeleteLessonPlan(lessonPlanId, userId, canDeleteAll);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Lesson plan deleted");
        }
    }
}
