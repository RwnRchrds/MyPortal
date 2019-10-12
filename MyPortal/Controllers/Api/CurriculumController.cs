using System;
using System.Collections.Generic;
using System.Net;
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
        public async Task<IEnumerable<CurriculumAcademicYearDto>> GetAcademicYears()
        {
            try
            {
                return await CurriculumProcesses.GetAcademicYears(_context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpGet]
        [Route("academicYears/get/byId/{academicYearId:int}", Name = "ApiCurriculumGetAcademicYearById")]
        public async Task<CurriculumAcademicYearDto> GetAcademicYearById([FromUri] int academicYearId)
        {
            try
            {
                return await CurriculumProcesses.GetAcademicYearById(academicYearId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpPost]
        [RequiresPermission("ChangeAcademicYear")]
        [Route("academicYears/select", Name = "ApiCurriculumChangeSelectedAcademicYear")]
        public IHttpActionResult ChangeSelectedAcademicYear([FromBody] CurriculumAcademicYear year)
        {
            //TODO: MAKE ASYNC
            User.ChangeSelectedAcademicYear(year.Id);
            return Ok("Selected academic year changed");
        }

        [HttpGet]
        [RequiresPermission("ViewSessions")]
        [Route("sessions/get/byTeacherAndDate/{teacherId:int}/{date:datetime}", Name = "ApiCurriculumGetSessionsByTeacherAndDate")]
        public async Task<IEnumerable<CurriculumSessionDto>> GetSessionsByTeacherOnDayOfWeek([FromUri] int teacherId, [FromUri] DateTime date)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            try
            {
                return await CurriculumProcesses.GetSessionsByTeacherOnDayOfWeek(teacherId, academicYearId, date, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpPost]
        [RequiresPermission("ViewSessions")]
        [Route("sessions/get/byTeacherAndDate/dataGrid/{teacherId:int}/{date:datetime}", Name = "ApiCurriculumGetSessionsByTeacherAndDateDataGrid")]
        public async Task<IHttpActionResult> GetSessionsByTeacherDataGrid([FromUri] int teacherId, [FromUri] DateTime date,
            [FromBody] DataManagerRequest dm)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            try
            {
                var sessions =
                    await CurriculumProcesses.GetSessionsByTeacherOnDayOfWeek(teacherId, academicYearId, date,
                        _context);

                return PrepareDataGridObject(sessions, dm);
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
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            try
            {
                return await CurriculumProcesses.GetAllClasses(academicYearId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpPost]
        [RequiresPermission("ViewClasses")]
        [Route("classes/get/dataGrid/all", Name = "ApiCurriculumGetAllClassesDataGrid")]
        public async Task<IHttpActionResult> GetAllClassesDataGrid([FromBody] DataManagerRequest dm)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            try
            {
                var classes = await CurriculumProcesses.GetAllClasses(academicYearId, _context);

                return PrepareDataGridObject(classes, dm);
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
                return await CurriculumProcesses.GetClassById(classId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("classes/create", Name = "ApiCurriculumCreateClass")]
        public async Task<IHttpActionResult> CreateClass([FromBody] CurriculumClass @class)
        {
            @class.AcademicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            try
            {
                await CurriculumProcesses.CreateClass(@class, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Class created");
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("classes/update", Name = "ApiCurriculumUpdateClass")]
        public async Task<IHttpActionResult> UpdateClass([FromBody] CurriculumClass @class)
        {
            try
            {
                await CurriculumProcesses.UpdateClass(@class, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Class updated");
        }

        [HttpDelete]
        [RequiresPermission("EditClasses")]
        [Route("classes/delete/{classId:int}", Name = "ApiCurriculumDeleteClass")]
        public async Task<IHttpActionResult> DeleteClass([FromUri] int classId)
        {
            try
            {
                await CurriculumProcesses.DeleteClass(classId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Class deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewSessions")]
        [Route("sessions/get/byClass/{classId:int}", Name = "ApiCurriculumGetSessionsByClass")]
        public async Task<IEnumerable<CurriculumSessionDto>> GetSessionsByClass([FromUri] int classId)
        {
            try
            {
                return await CurriculumProcesses.GetSessionsByClass(classId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [RequiresPermission("ViewSessions")]
        [HttpPost]
        [Route("sessions/get/byClass/dataGrid/{classId:int}", Name = "ApiCurriculumGetSessionsByClassDataGrid")]
        public async Task<IHttpActionResult> GetSessionsByClassDataGrid([FromUri] int classId, [FromBody] DataManagerRequest dm)
        {
            try
            {
                var sessions = await CurriculumProcesses.GetSessionsByClass(classId, _context);

                return PrepareDataGridObject(sessions, dm);
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
                return await CurriculumProcesses.GetSessionById(sessionId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("sessions/create", Name = "ApiCurriculumCreateSession")]
        public async Task<IHttpActionResult> CreateSession([FromBody] CurriculumSession session)
        {
            try
            {
                await CurriculumProcesses.CreateSession(session, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Session created");
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("sessions/addRegPeriods", Name = "ApiCurriculumCreateSessionsForRegPeriods")]
        public async Task<IHttpActionResult> CreateSessionsForRegPeriods([FromBody] CurriculumSession session)
        {
            try
            {
                await CurriculumProcesses.CreateSessionForRegPeriods(session, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Sessions created");
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("sessions/update", Name = "ApiCurriculumUpdateSession")]
        public async Task<IHttpActionResult> UpdateSession([FromBody] CurriculumSession session)
        {
            try
            {
                await CurriculumProcesses.UpdateSession(session, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Session updated");
        }

        [HttpDelete]
        [RequiresPermission("EditClasses")]
        [Route("sessions/delete/{sessionId:int}", Name = "ApiCurriculumDeleteSession")]
        public async Task<IHttpActionResult> DeleteSession([FromUri] int sessionId)
        {
            try
            {
                await CurriculumProcesses.DeleteSession(sessionId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Session deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byClass/{classId:int}", Name = "ApiCurriculumGetEnrolmentsByClass")]
        public async Task<IEnumerable<CurriculumEnrolmentDto>> GetEnrolmentsByClass([FromUri] int classId)
        {
            try
            {
                return await CurriculumProcesses.GetEnrolmentsForClass(classId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
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
                var enrolments = await CurriculumProcesses.GetEnrolmentsForClassDataGrid(classId, _context);

                return PrepareDataGridObject(enrolments, dm);
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
                return await CurriculumProcesses.GetEnrolmentsForStudent(studentId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
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
                var enrolments = await CurriculumProcesses.GetEnrolmentsForStudentDataGrid(studentId, _context);

                return PrepareDataGridObject(enrolments, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewEnrolments")]
        [Route("enrolments/get/byId/{enrolmentId:int}", Name = "ApiCurriculumGetEnrolment")]
        public async Task<CurriculumEnrolmentDto> GetEnrolment([FromUri] int enrolmentId)
        {
            try
            {
                return await CurriculumProcesses.GetEnrolmentById(enrolmentId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("enrolments/create", Name = "ApiCurriculumCreateEnrolment")]
        public async Task<IHttpActionResult> CreateEnrolment([FromBody] CurriculumEnrolment enrolment)
        {
            try
            {
                await CurriculumProcesses.CreateEnrolment(enrolment, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Enrolment created");
        }

        [HttpPost]
        [RequiresPermission("EditClasses")]
        [Route("enrolments/create/group", Name = "ApiCurriculumEnrolRegGroup")]
        public async Task<IHttpActionResult> EnrolRegGroup([FromBody] GroupEnrolment enrolment)
        {
            try
            {
                await CurriculumProcesses.CreateEnrolmentsForRegGroup(enrolment, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Group enrolled");
        }

        [HttpDelete]
        [RequiresPermission("EditClasses")]
        [Route("classes/enrolments/delete/{enrolmentId:int}", Name = "ApiCurriculumDeleteEnrolment")]
        public async Task<IHttpActionResult> DeleteEnrolment([FromUri] int enrolmentId)
        {
            try
            {
                await CurriculumProcesses.DeleteEnrolment(enrolmentId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Enrolment deleted");
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/new", Name = "ApiCurriculumCreateSubject")]
        public async Task<IHttpActionResult> CreateSubject([FromBody] CurriculumSubject subject)
        {
            try
            {
                await CurriculumProcesses.CreateSubject(subject, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Subject created");
        }

        [HttpDelete]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/delete/{subjectId:int}", Name = "ApiCurriculumDeleteSubject")]
        public async Task<IHttpActionResult> DeleteSubject([FromUri] int subjectId)
        {
            try
            {
                await CurriculumProcesses.DeleteSubject(subjectId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Subject deleted");
        }

        [HttpGet]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/get/byId/{subjectId:int}", Name = "ApiCurriculumGetSubjectById")]
        public async Task<CurriculumSubjectDto> GetSubjectById([FromUri] int subjectId)
        {
            try
            {
                return await CurriculumProcesses.GetSubjectById(subjectId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpGet]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/get/all", Name = "ApiCurriculumGetAllSubjects")]
        public async Task<IEnumerable<CurriculumSubjectDto>> GetAllSubjects()
        {
            try
            {
                return await CurriculumProcesses.GetAllSubjects(_context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpPost]
        [RequiresPermission("EditSubjects")]
        [Route("subjects/get/dataGrid/all", Name = "ApiCurriculumGetAllSubjectsDataGrid")]
        public async Task<IHttpActionResult> GetAllSubjectsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var subjects = await CurriculumProcesses.GetAllSubjectsDataGrid(_context);

                return PrepareDataGridObject(subjects, dm);
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
                await CurriculumProcesses.UpdateSubject(subject, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Subject updated");
        }

        [HttpPost]
        [RequiresPermission("EditStudyTopics")]
        [Route("studyTopics/create", Name = "ApiCurriculumCreateStudyTopic")]
        public async Task<IHttpActionResult> CreateStudyTopic([FromBody] CurriculumStudyTopic studyTopic)
        {
            try
            {
                await CurriculumProcesses.CreateStudyTopic(studyTopic, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Study topic created");
        }

        [HttpDelete]
        [RequiresPermission("EditStudyTopics")]
        [Route("studyTopics/delete/{studyTopicId:int}", Name = "ApiCurriculumDeleteStudyTopic")]
        public async Task<IHttpActionResult> DeleteStudyTopic([FromUri] int studyTopicId)
        {
            try
            {
                await CurriculumProcesses.DeleteStudyTopic(studyTopicId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Study topic deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewStudyTopics")]
        [Route("studyTopics/get/byId/{studyTopicId:int}", Name = "ApiCurriculumGetStudyTopicById")]
        public async Task<CurriculumStudyTopicDto> GetStudyTopicById([FromUri] int studyTopicId)
        {
            try
            {
                return await CurriculumProcesses.GetStudyTopicById(studyTopicId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpGet]
        [RequiresPermission("ViewStudyTopics")]
        [Route("studyTopics/get/all", Name = "ApiCurriculumGetAllStudyTopics")]
        public async Task<IEnumerable<CurriculumStudyTopicDto>> GetAllStudyTopics()
        {
            try
            {
                return await CurriculumProcesses.GetAllStudyTopics(_context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpPost]
        [RequiresPermission("ViewStudyTopics")]
        [Route("studyTopics/get/dataGrid/all", Name = "ApiCurriculumGetAllStudyTopicsDataGrid")]
        public async Task<IHttpActionResult> GetAllStudyTopicsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var studyTopics = await CurriculumProcesses.GetAllStudyTopicsDataGrid(_context);

                return PrepareDataGridObject(studyTopics, dm);
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
                await CurriculumProcesses.UpdateStudyTopic(studyTopic, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Study topic updated");
        }

        [HttpGet]
        [RequiresPermission("ViewLessonPlans")]
        [Route("lessonPlans/get/all", Name = "ApiCurriculumGetAllLessonPlans")]
        public async Task<IEnumerable<CurriculumLessonPlanDto>> GetAllLessonPlans()
        {
            try
            {
                return await CurriculumProcesses.GetAllLessonPlans(_context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpGet]
        [RequiresPermission("ViewLessonPlans")]
        [Route("lessonPlans/get/byId/{lessonPlanId:int}", Name = "ApiCurriculumGetLessonPlanById")]
        public async Task<CurriculumLessonPlanDto> GetLessonPlanById([FromUri] int lessonPlanId)
        {
            try
            {
                return await CurriculumProcesses.GetLessonPlanById(lessonPlanId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
            }
        }

        [HttpGet]
        [RequiresPermission("ViewLessonPlans")]
        [Route("lessonPlans/get/byTopic/{studyTopicId:int}", Name = "ApiCurriculumGetLessonPlansByTopic")]
        public async Task<IEnumerable<CurriculumLessonPlanDto>> GetLessonPlansByTopic([FromUri] int studyTopicId)
        {
            try
            {
                return await CurriculumProcesses.GetLessonPlansByStudyTopic(studyTopicId, _context);
            }
            catch (Exception e)
            {
                ThrowException(e);
                return null;
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
                var lessonPlans = await CurriculumProcesses.GetLessonPlansByStudyTopicDataGrid(studyTopicId, _context);

                return PrepareDataGridObject(lessonPlans, dm);
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
            var userId = User.Identity.GetUserId();
            try
            {
                await CurriculumProcesses.CreateLessonPlan(plan, userId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Lesson plan created");
        }

        [HttpPost]
        [RequiresPermission("EditLessonPlans")]
        [Route("lessonPlans/update", Name = "ApiCurriculumUpdateLessonPlan")]
        public async Task<IHttpActionResult> UpdateLessonPlan([FromBody] CurriculumLessonPlan plan)
        {
            try
            {
                await CurriculumProcesses.UpdateLessonPlan(plan, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Lesson plan updated");
        }

        [HttpDelete]
        [RequiresPermission("EditLessonPlans")]
        [Route("lessonPlans/delete/{lessonPlanId:int}", Name = "ApiCurriculumDeleteLessonPlan")]
        public async Task<IHttpActionResult> DeleteLessonPlan([FromUri] int lessonPlanId)
        {
            var staffId = PeopleProcesses.GetStaffFromUserId(User.Identity.GetUserId(), _context).ResponseObject.Id;
            try
            {
                await CurriculumProcesses.DeleteLessonPlan(lessonPlanId, staffId,
                    User.HasPermission("DeleteAllLessonPlans"), _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Content(HttpStatusCode.OK, "Lesson plan deleted");
        }
    }
}
