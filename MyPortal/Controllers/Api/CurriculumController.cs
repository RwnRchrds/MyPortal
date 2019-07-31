using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/curriculum")]
    public class CurriculumController : MyPortalApiController
    {
        [HttpGet]
        [Route("academicYears/get/all")]
        public IEnumerable<CurriculumAcademicYearDto> GetAcademicYears()
        {
            return PrepareResponseObject(CurriculumProcesses.GetAcademicYears(_context));
        }

        [HttpGet]
        [Route("academicYears/get/byId/{academicYearId:int}")]
        public CurriculumAcademicYearDto GetAcademicYear([FromUri] int academicYearId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetAcademicYear(academicYearId, _context));
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        [Route("academicYears/select")]    
        public IHttpActionResult ChangeSelectedAcademicYear([FromBody] CurriculumAcademicYear year)
        {
            User.ChangeSelectedAcademicYear(year.Id);
            return Ok("Selected academic year changed");
        }
        
        [HttpGet]
        [Route("sessions/get/byTeacher/{teacherId:int}/{date:datetime}")]
        public IEnumerable<CurriculumSessionDto> GetSessionsForTeacher([FromUri] int teacherId, [FromUri] DateTime date)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(
                CurriculumProcesses.GetSessionsForTeacher(teacherId, academicYearId, date, _context));
        }

        [HttpPost]
        [Route("sessions/get/byTeacher/dataGrid/{teacherId:int}/{date:datetime}")]
        public IHttpActionResult GetSessionsForTeacher_DataGrid([FromUri] int teacherId, [FromUri] DateTime date,
            [FromBody] DataManagerRequest dm)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var sessions =
                PrepareResponseObject(
                    CurriculumProcesses.GetSessionsForTeacher_DataGrid(teacherId, academicYearId, date, _context));

            return PrepareDataGridObject(sessions, dm);
        }

        [HttpGet]
        [Route("classes/get/all")]
        public IEnumerable<CurriculumClassDto> GetAllClasses()
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(CurriculumProcesses.GetAllClasses(academicYearId, _context));
        }

        [HttpPost]
        [Route("classes/get/dataGrid/all")]
        public IHttpActionResult GetAllClasses_DataGrid([FromBody] DataManagerRequest dm)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var classes = PrepareResponseObject(CurriculumProcesses.GetAllClasses_DataGrid(academicYearId, _context));

            return PrepareDataGridObject(classes, dm);
        }

        [HttpGet]
        [Route("classes/get/byId/{classId:int}")]
        public CurriculumClassDto GetClassById([FromUri] int classId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetClassById(classId, _context));
        }

        [HttpPost]
        [Route("classes/create")]
        public IHttpActionResult CreateClass([FromBody] CurriculumClass @class)
        {
            @class.AcademicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponse(CurriculumProcesses.CreateClass(@class, _context));
        }

        [HttpPost]
        [Route("classes/update")]
        public IHttpActionResult UpdateClass([FromBody] CurriculumClass @class)
        {
            return PrepareResponse(CurriculumProcesses.UpdateClass(@class, _context));
        }

        [HttpDelete]
        [Route("classes/delete/{classId:int}")]
        public IHttpActionResult DeleteClass([FromUri] int classId)
        {
            return PrepareResponse(CurriculumProcesses.DeleteClass(classId, _context));
        }

        [HttpGet]
        [Route("sessions/get/byClassId/{classId:int}")]
        public IEnumerable<CurriculumSessionDto> GetSessionsForClass([FromUri] int classId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetSessionsForClass(classId, _context));
        }

        [HttpPost]
        [Route("sessions/get/byClassId/dataGrid/{classId:int}")]
        public IHttpActionResult GetSessionsForClassDataGrid([FromUri] int classId, [FromBody] DataManagerRequest dm)
        {
            var sessions = PrepareResponseObject(CurriculumProcesses.GetSessionsForClass_DataGrid(classId, _context));

            return PrepareDataGridObject(sessions, dm);
        }

        [HttpGet]
        [Route("sessions/get/byId/{sessionId:int}")]
        public CurriculumSessionDto GetSessionById([FromUri] int sessionId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetSessionById(sessionId, _context));
        }

        [HttpPost]
        [Route("sessions/create")]
        public IHttpActionResult CreateSession([FromBody] CurriculumSession session)
        {
            return PrepareResponse(CurriculumProcesses.CreateSession(session, _context));
        }

        [HttpPost]
        [Route("sessions/addRegPeriods")]
        public IHttpActionResult CreateSessionsForRegPeriods([FromBody] CurriculumSession session)
        {
            return PrepareResponse(CurriculumProcesses.CreateSessionForRegPeriods(session, _context));
        }

        [HttpPost]
        [Route("sessions/update")]
        public IHttpActionResult UpdateSession([FromBody] CurriculumSession session)
        {
            return PrepareResponse(CurriculumProcesses.UpdateSession(session, _context));
        }

        [HttpDelete]
        [Route("sessions/delete/{sessionId:int}")]
        public IHttpActionResult DeleteSession([FromUri] int sessionId)
        {
            return PrepareResponse(CurriculumProcesses.DeleteSession(sessionId, _context));
        }

        [HttpGet]
        [Route("enrolments/get/byClass/{classId:int}")]
        public IEnumerable<CurriculumEnrolmentDto> GetEnrolmentsForClass([FromUri] int classId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetEnrolmentsForClass(classId, _context));
        }

        [HttpPost]
        [Route("enrolments/get/byClass/dataGrid/{classId:int}")]
        public IHttpActionResult GetEnrolmentsForClassForDataGrid([FromUri] int classId,
            [FromBody] DataManagerRequest dm)
        {
            var enrolments = CurriculumProcesses.GetEnrolmentsForClass_DataGrid(classId, _context).ResponseObject;

            return PrepareDataGridObject(enrolments, dm);
        }

        [HttpGet]
        [Route("enrolments/get/byStudent/{studentId:int}")]
        public IEnumerable<CurriculumEnrolmentDto> GetEnrolmentsForStudent([FromUri] int studentId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetEnrolmentsForStudent(studentId, _context));
        }

        [HttpPost]
        [Route("enrolments/get/byStudent/dataGrid/{studentId:int}")]
        public IHttpActionResult GetEnrolmentsForStudentForDataGrid([FromUri] int studentId,
            [FromBody] DataManagerRequest dm)
        {
            var enrolments =
                PrepareResponseObject(CurriculumProcesses.GetEnrolmentsForStudent_DataGrid(studentId, _context));

            return PrepareDataGridObject(enrolments, dm);
        }

        [HttpGet]
        [Route("enrolments/get/byId/{enrolmentId:int}")]
        public CurriculumEnrolmentDto GetEnrolment([FromUri] int enrolmentId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetEnrolmentById(enrolmentId, _context));
        }

        [HttpPost]
        [Route("enrolments/create")]
        public IHttpActionResult CreateEnrolment([FromBody] CurriculumEnrolment enrolment)
        {
            return PrepareResponse(CurriculumProcesses.CreateEnrolment(enrolment, _context));
        }

        [HttpPost]
        [Route("classes/enrolments/create/regGroup")]
        public IHttpActionResult EnrolRegGroup([FromBody] GroupEnrolment enrolment)
        {
            return PrepareResponse(CurriculumProcesses.CreateEnrolmentsForRegGroup(enrolment, _context));
        }

        [HttpDelete]
        [Route("classes/enrolments/delete/{enrolmentId:int}")]
        public IHttpActionResult DeleteEnrolment([FromUri] int enrolmentId)
        {
            return PrepareResponse(CurriculumProcesses.DeleteEnrolment(enrolmentId, _context));
        }

        [HttpPost]
        [Route("subjects/new")]
        public IHttpActionResult CreateSubject([FromBody] CurriculumSubject subject)
        {
            return PrepareResponse(CurriculumProcesses.CreateSubject(subject, _context));
        }

        [HttpDelete]
        [Route("subjects/delete/{subjectId:int}")]
        public IHttpActionResult DeleteSubject([FromUri] int subjectId)
        {
            return PrepareResponse(CurriculumProcesses.DeleteSubject(subjectId, _context));
        }

        [HttpGet]
        [Route("subjects/get/byId/{subjectId:int}")]
        public CurriculumSubjectDto GetSubjectById([FromUri] int subjectId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetSubjectById(subjectId, _context));
        }

        [HttpGet]
        [Route("subjects/get/all")]
        public IEnumerable<CurriculumSubjectDto> GetAllSubjects()
        {
            return PrepareResponseObject(CurriculumProcesses.GetAllSubjects(_context));
        }

        [HttpPost]
        [Route("subjects/update")]
        public IHttpActionResult UpdateSubject([FromBody] CurriculumSubject subject)
        {
            return PrepareResponse(CurriculumProcesses.UpdateSubject(subject, _context));
        }

        [HttpPost]
        [Route("studyTopics/create")]
        public IHttpActionResult CreateStudyTopic([FromBody] CurriculumStudyTopic studyTopic)
        {
            return PrepareResponse(CurriculumProcesses.CreateStudyTopic(studyTopic, _context));
        }

        [HttpDelete]
        [Route("studyTopics/delete/{studyTopicId:int}")]
        public IHttpActionResult DeleteStudyTopic([FromUri] int studyTopicId)
        {
            return PrepareResponse(CurriculumProcesses.DeleteStudyTopic(studyTopicId, _context));
        }

        [HttpGet]
        [Route("studyTopics/get/byId/{id}")]
        public CurriculumStudyTopicDto GetStudyTopic([FromUri] int studyTopicId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetStudyTopicById(studyTopicId, _context));
        }

        [HttpGet]
        [Route("studyTopics/get/all")]
        public IEnumerable<CurriculumStudyTopicDto> GetAllStudyTopics()
        {
            return PrepareResponseObject(CurriculumProcesses.GetAllStudyTopics(_context));
        }

        [HttpPost]
        [Route("studyTopics/update")]
        public IHttpActionResult UpdateStudyTopic([FromBody] CurriculumStudyTopic studyTopic)
        {
            return PrepareResponse(CurriculumProcesses.UpdateStudyTopic(studyTopic, _context));
        }
        
        [HttpGet]
        [Route("lessonPlans/get/all")]
        public IEnumerable<CurriculumLessonPlanDto> GetLessonPlans()
        {
            return PrepareResponseObject(CurriculumProcesses.GetAllLessonPlans(_context));
        }
        
        [HttpGet]
        [Route("lessonPlans/get/byId/{lessonPlanId:int}")]
        public CurriculumLessonPlanDto GetLessonPlanById([FromUri] int lessonPlanId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetLessonPlanById(lessonPlanId, _context));
        }

        [HttpGet]
        [Route("lessonPlans/byTopic/{studyTopicId:int}")]
        public IEnumerable<CurriculumLessonPlanDto> GetLessonPlansByTopic([FromUri] int studyTopicId)
        {
            return PrepareResponseObject(CurriculumProcesses.GetLessonPlansByStudyTopic(studyTopicId, _context));
        }

        [HttpPost]
        [Route("lessonPlans/create")]
        public IHttpActionResult CreateLessonPlan([FromBody] CurriculumLessonPlan plan)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(CurriculumProcesses.CreateLessonPlan(plan, userId, _context));
        }

        [HttpPost]
        [Route("lessonPlans/update")]
        public IHttpActionResult UpdateLessonPlan([FromBody] CurriculumLessonPlan plan)
        {
            return PrepareResponse(CurriculumProcesses.UpdateLessonPlan(plan, _context));
        }

        [HttpDelete]
        [Route("lessonPlans/delete/{lessonPlanId:int}")]
        public IHttpActionResult DeleteLessonPlan([FromUri] int lessonPlanId)
        {
            return PrepareResponse(CurriculumProcesses.DeleteLessonPlan(lessonPlanId, _context));
        }
    }
}
