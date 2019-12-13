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
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Dtos.DataGrid;
using MyPortal.BusinessLogic.Models;
using MyPortal.BusinessLogic.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/curriculum")]
    public class CurriculumController : MyPortalApiController
    {
        private readonly CurriculumService _service;

        public CurriculumController()
        {
            _service = new CurriculumService();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
        
        [HttpGet]
        [Route("academicYears/get/all", Name = "ApiGetAcademicYears")]
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
        [Route("academicYears/get/byId/{academicYearId:int}", Name = "ApiGetAcademicYearById")]
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
        [Route("academicYears/select", Name = "ApiChangeSelectedAcademicYear")]
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
        [Route("sessions/get/byTeacherAndDate/{teacherId:int}/{date:datetime}", Name = "ApiGetSessionsByTeacherAndDate")]
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
        [Route("sessions/get/byTeacherAndDate/dataGrid/{teacherId:int}/{date:datetime}", Name = "ApiGetSessionsByTeacherAndDateDataGrid")]
        public async Task<IHttpActionResult> GetSessionsByTeacherDataGrid([FromUri] int teacherId, [FromUri] DateTime date,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var academicYearId = await _service.GetCurrentOrSelectedAcademicYearId(User);

                var sessions = await _service.GetSessionsByDate(teacherId, academicYearId, date);

                var list = sessions.Select(Mapper.Map<CurriculumSession, DataGridSessionDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewClasses")]
        [Route("classes/get/all", Name = "ApiGetAllClasses")]
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
        [Route("classes/get/dataGrid/all", Name = "ApiGetAllClassesDataGrid")]
        public async Task<IHttpActionResult> GetAllClassesDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var academicYearId = await _service.GetCurrentOrSelectedAcademicYearId(User);

                var classes = await _service.GetAllClasses(academicYearId);

                var list = classes.Select(Mapper.Map<CurriculumClass, DataGridClassDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewClasses")]
        [Route("classes/get/dataGrid/bySubject/{subjectId:int}", Name = "ApiGetClassesBySubjectDataGrid")]
        public async Task<IHttpActionResult> GetClassesBySubjectDataGrid([FromUri] int subjectId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var academicYearId = await _service.GetCurrentOrSelectedAcademicYearId(User);

                var classes = await _service.GetClassesBySubject(subjectId, academicYearId);

                var list = classes.Select(Mapper.Map<CurriculumClass, DataGridClassDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewClasses")]
        [Route("classes/get/byId/{classId:int}", Name = "ApiGetClassById")]
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
        [Route("classes/create", Name = "ApiCreateClass")]
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
        [Route("classes/update", Name = "ApiUpdateClass")]
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
        [Route("classes/delete/{classId:int}", Name = "ApiDeleteClass")]
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
        [Route("sessions/get/byClass/{classId:int}", Name = "ApiGetSessionsByClass")]
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
        [Route("sessions/get/byClass/dataGrid/{classId:int}", Name = "ApiGetSessionsByClassDataGrid")]
        public async Task<IHttpActionResult> GetSessionsByClassDataGrid([FromUri] int classId, [FromBody] DataManagerRequest dm)
        {
            try
            {
                var sessions = await _service.GetSessionsByClass(classId);

                var list = sessions.Select(Mapper.Map<CurriculumSession, DataGridSessionDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewSessions")]
        [Route("sessions/get/byId/{sessionId:int}", Name = "ApiGetSessionById")]
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
        [Route("sessions/create", Name = "ApiCreateSession")]
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
        [Route("sessions/addRegPeriods", Name = "ApiCreateSessionsForRegPeriods")]
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
        [Route("sessions/update", Name = "ApiUpdateSession")]
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
        [Route("sessions/delete/{sessionId:int}", Name = "ApiDeleteSession")]
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
        [Route("enrolments/get/byClass/{classId:int}", Name = "ApiGetEnrolmentsByClass")]
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
        [Route("enrolments/get/byClass/dataGrid/{classId:int}", Name = "ApiGetEnrolmentsByClassDataGrid")]
        public async Task<IHttpActionResult> GetEnrolmentsByClassDataGrid([FromUri] int classId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var enrolments = await _service.GetEnrolmentsByClass(classId);

                var list = enrolments.Select(Mapper.Map<CurriculumEnrolment, DataGridEnrolmentDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byStudent/{studentId:int}", Name = "ApiGetEnrolmentsByStudent")]
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
        [Route("enrolments/get/byStudent/dataGrid/{studentId:int}", Name = "ApiGetEnrolmentsByStudentDataGrid")]
        public async Task<IHttpActionResult> GetEnrolmentsByStudentDataGrid([FromUri] int studentId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var enrolments = await _service.GetEnrolmentsByStudent(studentId);

                var list = enrolments.Select(Mapper.Map<CurriculumEnrolment, DataGridEnrolmentDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byId/{enrolmentId:int}", Name = "ApiGetEnrolment")]
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
        [Route("enrolments/create", Name = "ApiCreateEnrolment")]
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
        [Route("enrolments/create/group", Name = "ApiEnrolRegGroup")]
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
        [Route("classes/enrolments/delete/{enrolmentId:int}", Name = "ApiDeleteEnrolment")]
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
        [Route("subjects/new", Name = "ApiCreateSubject")]
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
        [Route("subjects/delete/{subjectId:int}", Name = "ApiDeleteSubject")]
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
        [Route("subjects/get/byId/{subjectId:int}", Name = "ApiGetSubjectById")]
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
        [Route("subjects/get/all", Name = "ApiGetAllSubjects")]
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
        [Route("subjects/staff/get/dataGrid/{subjectId:int}", Name = "ApiGetAllSubjectStaffBySubjectDataGrid")]
        public async Task<IHttpActionResult> GetAllSubjectStaffBySubject([FromUri] int subjectId, [FromBody] DataManagerRequest dm)
        {
            try
            {
                var staff = await _service.GetSubjectStaffBySubject(subjectId);

                var list = staff.Select(Mapper
                    .Map<CurriculumSubjectStaffMember, DataGridCurriculumSubjectStaffMemberDto>);

                return PrepareDataGridObject(list, dm);

            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/staff/get/byId/{subjectStaffId:int}", Name = "ApiGetSubjectStaffById")]
        public async Task<CurriculumSubjectStaffMemberDto> GetSubjectStaffById([FromUri] int subjectStaffId)
        {
            try
            {
                var subjectStaff = await _service.GetSubjectStaffById(subjectStaffId);

                return Mapper.Map<CurriculumSubjectStaffMember, CurriculumSubjectStaffMemberDto>(subjectStaff);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/staff/create", Name = "ApiCreateSubjectStaff")]
        public async Task<IHttpActionResult> CreateSubjectStaff([FromBody] CurriculumSubjectStaffMember subjectStaff)
        {
            try
            {
                await _service.CreateSubjectStaff(subjectStaff);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Subject staff member created");
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/staff/update", Name = "ApiUpdateSubjectStaff")]
        public async Task<IHttpActionResult> UpdateSubjectStaff([FromBody] CurriculumSubjectStaffMember subjectStaff)
        {
            try
            {
                await _service.UpdateSubjectStaff(subjectStaff);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Subject staff member updated");
        }

        [HttpDelete]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/staff/delete/{subjectStaffId:int}", Name = "ApiDeleteSubjectStaff")]
        public async Task<IHttpActionResult> DeleteSubjectStaff([FromUri] int subjectStaffId)
        {
            try
            {
                await _service.DeleteSubjectStaff(subjectStaffId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Subject staff deleted");
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/get/dataGrid/all", Name = "ApiGetAllSubjectsDataGrid")]
        public async Task<IHttpActionResult> GetAllSubjectsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var subjects = await _service.GetAllSubjects();

                var list = subjects.Select(Mapper.Map<CurriculumSubject, DataGridSubjectDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/update", Name = "ApiUpdateSubject")]
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
        [Route("studyTopics/create", Name = "ApiCreateStudyTopic")]
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
        [Route("studyTopics/delete/{studyTopicId:int}", Name = "ApiDeleteStudyTopic")]
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
        [Route("studyTopics/get/byId/{studyTopicId:int}", Name = "ApiGetStudyTopicById")]
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
        [Route("studyTopics/get/all", Name = "ApiGetAllStudyTopics")]
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
        [Route("studyTopics/get/dataGrid/all", Name = "ApiGetAllStudyTopicsDataGrid")]
        public async Task<IHttpActionResult> GetAllStudyTopicsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var studyTopics = await _service.GetAllStudyTopics();

                var list = studyTopics.Select(Mapper.Map<CurriculumStudyTopic, DataGridStudyTopicDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditStudyTopics")]
        [Route("studyTopics/update", Name = "ApiUpdateStudyTopic")]
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
        [Route("lessonPlans/get/all", Name = "ApiGetAllLessonPlans")]
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
        [Route("lessonPlans/get/byId/{lessonPlanId:int}", Name = "ApiGetLessonPlanById")]
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
        [Route("lessonPlans/get/byTopic/{studyTopicId:int}", Name = "ApiGetLessonPlansByTopic")]
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
        [Route("lessonPlans/get/byTopic/dataGrid/{studyTopicId:int}", Name = "ApiGetLessonPlansByStudyTopicDatagrid")]
        public async Task<IHttpActionResult> GetLessonPlansByStudyTopicDataGrid([FromUri] int studyTopicId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var lessonPlans = await _service.GetLessonPlansByStudyTopic(studyTopicId);

                var list = lessonPlans.Select(Mapper.Map<CurriculumLessonPlan, DataGridLessonPlanDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditLessonPlans")]
        [Route("lessonPlans/create", Name = "ApiCreateLessonPlan")]
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
        [Route("lessonPlans/update", Name = "ApiUpdateLessonPlan")]
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
        [Route("lessonPlans/delete/{lessonPlanId:int}", Name = "ApiDeleteLessonPlan")]
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
