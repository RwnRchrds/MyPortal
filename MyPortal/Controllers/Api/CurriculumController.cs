using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Attributes;
using MyPortal.Attributes.Filters;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Dtos.DataGrid;
using MyPortal.BusinessLogic.Models;
using MyPortal.BusinessLogic.Models.Data;
using MyPortal.BusinessLogic.Services;
using MyPortal.Services;
using MyPortal.Services.Identity;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/curriculum")]
    [ValidateModel]
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
        public async Task<IEnumerable<AcademicYearDto>> GetAcademicYears()
        {
            try
            {
                return await _service.GetAcademicYears();
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [Route("academicYears/get/byId/{academicYearId:int}", Name = "ApiGetAcademicYearById")]
        public async Task<AcademicYearDto> GetAcademicYearById([FromUri] int academicYearId)
        {
            try
            {
                return await _service.GetAcademicYearById(academicYearId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ChangeAcademicYear")]
        [Route("academicYears/select", Name = "ApiChangeSelectedAcademicYear")]
        public async Task<IHttpActionResult> ChangeSelectedAcademicYear([FromBody] AcademicYearDto year)
        {
            using (var userService = new UserService())
            {
                var userId = User.Identity.GetUserId();
                await userService.ChangeSelectedAcademicYear(userId, year.Id);
            }
            
            return Ok("Selected academic year changed");
        }

        [HttpGet]
        [RequiresPermission("ViewSessions")]
        [Route("sessions/get/byTeacherAndDate/{teacherId:int}/{date:datetime}", Name = "ApiGetSessionsByTeacherAndDate")]
        public async Task<IEnumerable<SessionDto>> GetSessionsByTeacherOnDayOfWeek([FromUri] int teacherId, [FromUri] DateTime date)
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                return await _service.GetSessionsByDate(teacherId, academicYearId, date);
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
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var sessions = await _service.GetSessionsByDate(teacherId, academicYearId, date);

                var list = sessions.Select(_mapper.Map<DataGridSessionDto>);

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
        public async Task<IEnumerable<ClassDto>> GetAllClasses()
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();
                
                return await _service.GetAllClasses(academicYearId);
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
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var classes = await _service.GetAllClasses(academicYearId);

                var list = classes.Select(_mapper.Map<DataGridClassDto>);

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
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var classes = await _service.GetClassesBySubject(subjectId, academicYearId);

                var list = classes.Select(_mapper.Map<DataGridClassDto>);

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
        public async Task<ClassDto> GetClassById([FromUri] int classId)
        {
            try
            {
                return await _service.GetClassById(classId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("classes/create", Name = "ApiCreateClass")]
        public async Task<IHttpActionResult> CreateClass([FromBody] ClassDto @class)
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                @class.AcademicYearId = academicYearId;

                await _service.CreateClass(@class);

                return Ok("Class created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("classes/update", Name = "ApiUpdateClass")]
        public async Task<IHttpActionResult> UpdateClass([FromBody] ClassDto @class)
        {
            try
            {
                await _service.UpdateClass(@class);

                return Ok("Class updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditClasses")]
        [Route("classes/delete/{classId:int}", Name = "ApiDeleteClass")]
        public async Task<IHttpActionResult> DeleteClass([FromUri] int classId)
        {
            try
            {
                await _service.DeleteClass(classId);

                return Ok("Class deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewSessions")]
        [Route("sessions/get/byClass/{classId:int}", Name = "ApiGetSessionsByClass")]
        public async Task<IEnumerable<SessionDto>> GetSessionsByClass([FromUri] int classId)
        {
            try
            {
                return await _service.GetSessionsByClass(classId);
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

                var list = sessions.Select(_mapper.Map<DataGridSessionDto>);

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
        public async Task<SessionDto> GetSessionById([FromUri] int sessionId)
        {
            try
            {
                var session = await _service.GetSessionById(sessionId);

                return _mapper.Map<SessionDto>(session);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("sessions/create", Name = "ApiCreateSession")]
        public async Task<IHttpActionResult> CreateSession([FromBody] SessionDto session)
        {
            try
            {
                await _service.CreateSession(session);

                return Ok("Session created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("sessions/addRegPeriods", Name = "ApiCreateSessionsForRegPeriods")]
        public async Task<IHttpActionResult> CreateSessionsForRegPeriods([FromBody] SessionDto session)
        {
            try
            {
                await _service.CreateSessionForRegPeriods(session);

                return Ok("Sessions created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("sessions/update", Name = "ApiUpdateSession")]
        public async Task<IHttpActionResult> UpdateSession([FromBody] SessionDto session)
        {
            try
            {
                await _service.UpdateSession(session);

                return Ok("Session updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditClasses")]
        [Route("sessions/delete/{sessionId:int}", Name = "ApiDeleteSession")]
        public async Task<IHttpActionResult> DeleteSession([FromUri] int sessionId)
        {
            try
            {
                await _service.DeleteSession(sessionId);

                return Ok("Session deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byClass/{classId:int}", Name = "ApiGetEnrolmentsByClass")]
        public async Task<IEnumerable<EnrolmentDto>> GetEnrolmentsByClass([FromUri] int classId)
        {
            try
            {
                return await _service.GetEnrolmentsByClass(classId);
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

                var list = enrolments.Select(_mapper.Map<DataGridEnrolmentDto>);

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
        public async Task<IEnumerable<EnrolmentDto>> GetEnrolmentsByStudent([FromUri] int studentId)
        {
            try
            {
                return await _service.GetEnrolmentsByStudent(studentId);
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

                var list = enrolments.Select(_mapper.Map<DataGridEnrolmentDto>);

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
        public async Task<EnrolmentDto> GetEnrolmentById([FromUri] int enrolmentId)
        {
            try
            {
                return await _service.GetEnrolmentById(enrolmentId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("enrolments/create", Name = "ApiCreateEnrolment")]
        public async Task<IHttpActionResult> CreateEnrolment([FromBody] EnrolmentDto enrolment)
        {
            try
            {
                await _service.CreateEnrolment(enrolment);

                return Ok("Enrolment created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("enrolments/create/group", Name = "ApiEnrolRegGroup")]
        public async Task<IHttpActionResult> EnrolRegGroup([FromBody] GroupEnrolment enrolment)
        {
            try
            {
                await _service.CreateEnrolmentsForRegGroup(enrolment);

                return Ok("Group enrolled");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditClasses")]
        [Route("classes/enrolments/delete/{enrolmentId:int}", Name = "ApiDeleteEnrolment")]
        public async Task<IHttpActionResult> DeleteEnrolment([FromUri] int enrolmentId)
        {
            try
            {
                await _service.DeleteEnrolment(enrolmentId);

                return Ok("Enrolment deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/new", Name = "ApiCreateSubject")]
        public async Task<IHttpActionResult> CreateSubject([FromBody] SubjectDto subject)
        {
            try
            {
                await _service.CreateSubject(subject);

                return Ok("Subject created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/delete/{subjectId:int}", Name = "ApiDeleteSubject")]
        public async Task<IHttpActionResult> DeleteSubject([FromUri] int subjectId)
        {
            try
            {
                await _service.DeleteSubject(subjectId);

                return Ok("Subject deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/get/byId/{subjectId:int}", Name = "ApiGetSubjectById")]
        public async Task<SubjectDto> GetSubjectById([FromUri] int subjectId)
        {
            try
            {
                var subject = await _service.GetSubjectById(subjectId);

                return _mapper.Map<SubjectDto>(subject);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/get/all", Name = "ApiGetAllSubjects")]
        public async Task<IEnumerable<SubjectDto>> GetAllSubjects()
        {
            try
            {
                return await _service.GetAllSubjects();
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

                var list = staff.Select(_mapper.Map<DataGridSubjectStaffMemberDto>);

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
        public async Task<SubjectStaffMemberDto> GetSubjectStaffById([FromUri] int subjectStaffId)
        {
            try
            {
                return await _service.GetSubjectStaffById(subjectStaffId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/staff/create", Name = "ApiCreateSubjectStaff")]
        public async Task<IHttpActionResult> CreateSubjectStaff([FromBody] SubjectStaffMemberDto subjectStaff)
        {
            try
            {
                await _service.CreateSubjectStaff(subjectStaff);

                return Ok("Subject staff member created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/staff/update", Name = "ApiUpdateSubjectStaff")]
        public async Task<IHttpActionResult> UpdateSubjectStaff([FromBody] SubjectStaffMemberDto subjectStaff)
        {
            try
            {
                await _service.UpdateSubjectStaff(subjectStaff);

                return Ok("Subject staff member updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/staff/delete/{subjectStaffId:int}", Name = "ApiDeleteSubjectStaff")]
        public async Task<IHttpActionResult> DeleteSubjectStaff([FromUri] int subjectStaffId)
        {
            try
            {
                await _service.DeleteSubjectStaff(subjectStaffId);

                return Ok("Subject staff deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/get/dataGrid/all", Name = "ApiGetAllSubjectsDataGrid")]
        public async Task<IHttpActionResult> GetAllSubjectsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var subjects = await _service.GetAllSubjects();

                var list = subjects.Select(_mapper.Map<DataGridSubjectDto>);

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
        public async Task<IHttpActionResult> UpdateSubject([FromBody] SubjectDto subject)
        {
            try
            {
                await _service.UpdateSubject(subject);

                return Ok("Subject updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditStudyTopics")]
        [Route("studyTopics/create", Name = "ApiCreateStudyTopic")]
        public async Task<IHttpActionResult> CreateStudyTopic([FromBody] StudyTopicDto studyTopic)
        {
            try
            {
                await _service.UpdateStudyTopic(studyTopic);

                return Ok("Study topic created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditStudyTopics")]
        [Route("studyTopics/delete/{studyTopicId:int}", Name = "ApiDeleteStudyTopic")]
        public async Task<IHttpActionResult> DeleteStudyTopic([FromUri] int studyTopicId)
        {
            try
            {
                await _service.DeleteStudyTopic(studyTopicId);

                return Ok("Study topic deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewStudyTopics")]
        [Route("studyTopics/get/byId/{studyTopicId:int}", Name = "ApiGetStudyTopicById")]
        public async Task<StudyTopicDto> GetStudyTopicById([FromUri] int studyTopicId)
        {
            try
            {
                return await _service.GetStudyTopicById(studyTopicId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewStudyTopics")]
        [Route("studyTopics/get/all", Name = "ApiGetAllStudyTopics")]
        public async Task<IEnumerable<StudyTopicDto>> GetAllStudyTopics()
        {
            try
            {
                return await _service.GetAllStudyTopics();
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

                var list = studyTopics.Select(_mapper.Map<DataGridStudyTopicDto>);

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
        public async Task<IHttpActionResult> UpdateStudyTopic([FromBody] StudyTopicDto studyTopic)
        {
            try
            {
                await _service.UpdateStudyTopic(studyTopic);

                return Ok("Study topic updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewLessonPlans")]
        [Route("lessonPlans/get/all", Name = "ApiGetAllLessonPlans")]
        public async Task<IEnumerable<LessonPlanDto>> GetAllLessonPlans()
        {
            try
            {
                var lessonPlans = await _service.GetAllLessonPlans();

                return lessonPlans.Select(_mapper.Map<LessonPlanDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewLessonPlans")]
        [Route("lessonPlans/get/byId/{lessonPlanId:int}", Name = "ApiGetLessonPlanById")]
        public async Task<LessonPlanDto> GetLessonPlanById([FromUri] int lessonPlanId)
        {
            try
            {
                var lessonPlan = await _service.GetLessonPlanById(lessonPlanId);

                return _mapper.Map<LessonPlanDto>(lessonPlan);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewLessonPlans")]
        [Route("lessonPlans/get/byTopic/{studyTopicId:int}", Name = "ApiGetLessonPlansByTopic")]
        public async Task<IEnumerable<LessonPlanDto>> GetLessonPlansByStudyTopic([FromUri] int studyTopicId)
        {
            try
            {
                var lessonPlans = await _service.GetLessonPlansByStudyTopic(studyTopicId);

                return lessonPlans.Select(_mapper.Map<LessonPlanDto>);
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

                var list = lessonPlans.Select(_mapper.Map<DataGridLessonPlanDto>);

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
        public async Task<IHttpActionResult> CreateLessonPlan([FromBody] LessonPlanDto plan)
        {
            try
            {
                using (var staffMemberService = new StaffMemberService())
                {
                    var userId = User.Identity.GetUserId();

                    var author = await staffMemberService.GetStaffMemberByUserId(userId);

                    plan.AuthorId = author.Id;

                    await _service.CreateLessonPlan(plan);

                    return Ok("Lesson plan created");
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditLessonPlans")]
        [Route("lessonPlans/update", Name = "ApiUpdateLessonPlan")]
        public async Task<IHttpActionResult> UpdateLessonPlan([FromBody] LessonPlanDto plan)
        {
            try
            {
                await _service.UpdateLessonPlan(plan);

                return Ok("Lesson plan updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("DeleteLessonPlans")]
        [Route("lessonPlans/delete/{lessonPlanId:int}", Name = "ApiDeleteLessonPlan")]
        public async Task<IHttpActionResult> DeleteLessonPlan([FromUri] int lessonPlanId)
        {
            try
            {
                await _service.DeleteLessonPlan(lessonPlanId);

                return Ok("Lesson plan deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
