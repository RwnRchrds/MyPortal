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

namespace MyPortal.Controllers.Api
{
    public class CurriculumController : MyPortalApiController
    {
        #region Academic Years
        [HttpGet]
        [Route("api/curriculum/academicYears/get/all")]
        public IEnumerable<CurriculumAcademicYearDto> GetAcademicYears()
        {
            return _context.CurriculumAcademicYears.ToList().OrderByDescending(x => x.FirstDate)
                .Select(Mapper.Map<CurriculumAcademicYear, CurriculumAcademicYearDto>);
        }

        [HttpGet]
        [Route("api/curriculum/academicYears/get/byId")]
        public CurriculumAcademicYearDto GetAcademicYear(int id)
        {
            var academicYear = _context.CurriculumAcademicYears.SingleOrDefault(x => x.Id == id);

            if (academicYear == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<CurriculumAcademicYear, CurriculumAcademicYearDto>(academicYear);
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        [Route("api/curriculum/academicYears/select")]    
        public IHttpActionResult ChangeSelectedAcademicYear(CurriculumAcademicYear year)
        {
            User.ChangeSelectedAcademicYear(year.Id);
            return Ok("Selected academic year changed");
        }

        #endregion

        #region Classes
        [HttpGet]
        [Route("api/curriculum/classes/byTeacher/{teacherId}/{dateString}")]
        public IEnumerable<CurriculumClassPeriodDto> GetClassesByTeacher(int teacherId, int dateString)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            int year = dateString / 10000;
            int month = ((dateString - (10000 * year)) / 100);
            int day = (dateString - (10000 * year) - (100 * month));

            var date = new DateTime(year, month, day);
            var weekBeginning = date.StartOfWeek();

            var academicYear = _context.CurriculumAcademicYears.SingleOrDefault(x => x.Id == academicYearId);

            if (academicYear == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (weekBeginning < academicYear.FirstDate || weekBeginning > academicYear.LastDate)
            {
                return new List<CurriculumClassPeriodDto>();
            }

            var currentWeek = _context.AttendanceWeeks.SingleOrDefault(x => x.Beginning == weekBeginning && x.AcademicYearId == academicYearId);

            if (currentWeek == null || currentWeek.IsHoliday)
            {
                return new List<CurriculumClassPeriodDto>();
            }

            return _context.CurriculumClassPeriods
                .Where(x =>
                    x.CurriculumClass.AcademicYearId == academicYearId && x.AttendancePeriod.Weekday ==
                    date.DayOfWeek
                        .ToString().Substring(0, 3) && x.CurriculumClass.TeacherId == teacherId)
                .OrderBy(x => x.AttendancePeriod.StartTime)
                .ToList()
                .Select(Mapper.Map<CurriculumClassPeriod, CurriculumClassPeriodDto>);
        }

        [HttpGet]
        [Route("api/curriculum/classes/get/all")]
        public IEnumerable<CurriculumClassDto> GetAllClasses()
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return _context.CurriculumClasses.Where(x => x.AcademicYearId == academicYearId).ToList()
                .OrderBy(x => x.Name).Select(Mapper.Map<CurriculumClass, CurriculumClassDto>);
        }

        [HttpGet]
        [Route("api/curriculum/classes/get/byId/{id}")]
        public CurriculumClassDto GetClassById(int id)
        {
            var currClass = _context.CurriculumClasses.SingleOrDefault(x => x.Id == id);

            if (currClass == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<CurriculumClass, CurriculumClassDto>(currClass);
        }

        [HttpPost]
        [Route("api/curriculum/classes/create")]
        public IHttpActionResult CreateClass(CurriculumClass currClass)
        {
            currClass.AcademicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            if (_context.CurriculumClasses.Any(x => x.Name == currClass.Name))
            {
                return Content(HttpStatusCode.BadRequest, "Class with that name already exists");
            }

            _context.CurriculumClasses.Add(currClass);

            _context.SaveChanges();

            return Ok("Class added");
        }

        [HttpPost]
        [Route("api/curriculum/classes/update")]
        public IHttpActionResult UpdateClass(CurriculumClass currClass)
        {
            var classInDb = _context.CurriculumClasses.SingleOrDefault(x => x.Id == currClass.Id);

            if (classInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Class not found");
            }

            classInDb.Name = currClass.Name;
            classInDb.SubjectId = currClass.SubjectId;
            classInDb.TeacherId = currClass.TeacherId;
            classInDb.YearGroupId = currClass.YearGroupId;

            _context.SaveChanges();

            return Ok("Class updated");
        }

        [HttpDelete]
        [Route("api/curriculum/classes/delete/{id}")]
        public IHttpActionResult DeleteClass(int id)
        {
            var currClass = _context.CurriculumClasses.SingleOrDefault(x => x.Id == id);

            if (currClass == null)
            {
                return Content(HttpStatusCode.NotFound, "CLass not found");
            }

            if (!currClass.HasPeriods() && !currClass.HasEnrolments())
            {
                _context.CurriculumClasses.Remove(currClass);
                _context.SaveChanges();

                return Ok("Class deleted");
            }

            return Content(HttpStatusCode.BadRequest, "Class cannot be deleted");
        }

        [HttpGet]
        [Route("api/curriculum/classes/schedule/get/{classId}")]
        public IEnumerable<CurriculumClassPeriodDto> GetSchedule(int classId)
        {
            return _context.CurriculumClassPeriods.Where(x => x.ClassId == classId).ToList()
                .Select(Mapper.Map<CurriculumClassPeriod, CurriculumClassPeriodDto>);
        }

        [HttpGet]
        [Route("api/curriculum/classes/schedule/getAssignment/{id}")]
        public CurriculumClassPeriodDto GetAssignment(int id)
        {
            var assignment = _context.CurriculumClassPeriods.SingleOrDefault(x => x.Id == id);

            if (assignment == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<CurriculumClassPeriod, CurriculumClassPeriodDto>(assignment);
        }

        [HttpPost]
        [Route("api/curriculum/classes/schedule/createAssignment")]
        public IHttpActionResult CreateTimetableAssignment(CurriculumClassPeriod assignment)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var currClass = _context.CurriculumClasses.SingleOrDefault(x => x.Id == assignment.ClassId);

            if (currClass == null)
            {
                return Content(HttpStatusCode.NotFound, "Class not found");
            }

            if (currClass.HasEnrolments())
            {
                return Content(HttpStatusCode.BadRequest, "Cannot modify class schedule while students are enrolled");
            }

            if (_context.CurriculumClassPeriods.Any(x =>
                x.ClassId == assignment.ClassId && x.PeriodId == assignment.PeriodId))
            {
                return Content(HttpStatusCode.BadRequest, "Class already assigned to this period");
            }

            _context.CurriculumClassPeriods.Add(assignment);
            _context.SaveChanges();

            return Ok("Assignment added");
        }

        [HttpPost]
        [Route("api/curriculum/classes/schedule/regPeriods")]
        public IHttpActionResult AssignAllRegPeriods(CurriculumClassPeriod assignment)
        {
            assignment.PeriodId = 0;

            var regPeriods = _context.AttendancePeriods.Where(x => x.IsAm || x.IsPm);

            var currClass = _context.CurriculumClasses.SingleOrDefault(x => x.Id == assignment.ClassId);

            if (currClass == null)
            {
                return Content(HttpStatusCode.NotFound, "Class not found");
            }

            if (currClass.HasEnrolments())
            {
                return Content(HttpStatusCode.BadRequest, "Cannot modify class schedule while students are enrolled");
            }

            if (_context.CurriculumClassPeriods.Any(x =>
                x.ClassId == assignment.ClassId && x.PeriodId == assignment.PeriodId))
            {
                return Content(HttpStatusCode.BadRequest, "Class already assigned to this period");
            }

            foreach (var period in regPeriods)
            {
                var newAssignment = new CurriculumClassPeriod {PeriodId = period.Id, ClassId = currClass.Id};

                _context.CurriculumClassPeriods.Add(newAssignment);
            }

            _context.SaveChanges();

            return Ok("Class assigned to reg periods");
        }

        [HttpPost]
        [Route("api/curriculum/classes/schedule/updateAssignment")]
        public IHttpActionResult UpdateTimetableAssignment(CurriculumClassPeriod assignment)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var assignmentInDb = _context.CurriculumClassPeriods.SingleOrDefault(x => x.Id == assignment.Id);

            var currClass = _context.CurriculumClasses.SingleOrDefault(x => x.Id == assignment.ClassId);

            if (assignmentInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Assignment not found");
            }

            if (currClass.HasEnrolments())
            {
                return Content(HttpStatusCode.BadRequest, "Cannot modify class schedule while students are enrolled");
            }

            if (_context.CurriculumClassPeriods.Any(x =>
                x.ClassId == assignment.ClassId && x.PeriodId == assignment.PeriodId))
            {
                return Content(HttpStatusCode.BadRequest, "Class already assigned to this period");
            }

            assignmentInDb.PeriodId = assignment.PeriodId;
            _context.SaveChanges();

            return Ok("Assignment updated");
        }

        [HttpDelete]
        [Route("api/curriculum/classes/schedule/deleteAssignment/{assignmentId}")]
        public IHttpActionResult DeleteTimetableAssignment(int assignmentId)
        {
            var assignmentInDb = _context.CurriculumClassPeriods.SingleOrDefault(x => x.Id == assignmentId);

            if (assignmentInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Assignment not found");
            }

            _context.CurriculumClassPeriods.Remove(assignmentInDb);
            _context.SaveChanges();

            return Ok("Assignment deleted");
        }

        [HttpGet]
        [Route("api/curriculum/classes/enrolments/get/{classId}")]
        public IEnumerable<CurriculumClassEnrolmentDto> GetEnrolments(int classId)
        {
            return _context.CurriculumClassEnrolments.Where(x => x.ClassId == classId).ToList()
                .OrderBy(x => x.Student.Person.LastName)
                .Select(Mapper.Map<CurriculumClassEnrolment, CurriculumClassEnrolmentDto>);
        }

        [HttpGet]
        [Route("api/curriculum/classes/enrolments/get/byId/{id}")]
        public CurriculumClassEnrolmentDto GetEnrolment(int id)
        {
            var enrolment = _context.CurriculumClassEnrolments.SingleOrDefault(x => x.Id == id);

            if (enrolment == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<CurriculumClassEnrolment, CurriculumClassEnrolmentDto>(enrolment);
        }

        [HttpPost]
        [Route("api/curriculum/classes/enrolments/create")]
        public IHttpActionResult CreateEnrolment(CurriculumClassEnrolment enrolment)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            if (!_context.CurriculumClasses.Any(x => x.Id == enrolment.ClassId))
            {
                return Content(HttpStatusCode.NotFound, "Class not found");
            }

            if (!_context.Students.Any(x => x.Id == enrolment.StudentId))
            {
                return Content(HttpStatusCode.NotFound, "Student not found");
            }

            if (!_context.CurriculumClassPeriods.Any(x => x.ClassId == enrolment.ClassId))
            {
                return Content(HttpStatusCode.BadRequest,
                    "Cannot add students to class before schedule has been set up");
            }

            if (_context.CurriculumClassEnrolments.Any(x =>
                x.ClassId == enrolment.ClassId && x.StudentId == enrolment.StudentId))
            {
                return Content(HttpStatusCode.BadRequest,
                    enrolment.Student.Person.LastName + ", " + enrolment.Student.Person.FirstName + " is already enrolled in " +
                    enrolment.CurriculumClass.Name);
            }

            bool canEnroll;

            try
            {
                canEnroll = CurriculumProcesses.StudentCanEnroll(_context, enrolment.StudentId, enrolment.ClassId);
            }
            catch (EntityNotFoundException e)
            {
                return Content(HttpStatusCode.NotFound, e.Message);
            }
            catch (PersonNotFreeException e)
            {
                return Content(HttpStatusCode.BadRequest, e.Message);
            }

            if (canEnroll)
            {
                _context.CurriculumClassEnrolments.Add(enrolment);
                _context.SaveChanges();

                return Ok("Student enrolled in class");
            }

            return Content(HttpStatusCode.BadRequest, "An unknown error occurred");
        }

        [HttpPost]
        [Route("api/curriculum/classes/enrolments/create/group")]
        public IHttpActionResult EnrolGroup(GroupEnrolment enrolment)
        {
            var group = _context.PastoralRegGroups.SingleOrDefault(x => x.Id == enrolment.GroupId);

            if (group == null)
            {
                return Content(HttpStatusCode.NotFound, "Group not found");
            }

            foreach (var student in group.Students)
            {
                var studentEnrolment = new CurriculumClassEnrolment
                {
                    ClassId = enrolment.ClassId,
                    StudentId = student.Id
                };

                CreateEnrolment(studentEnrolment);
            }

            return Ok("Group enrolled");
        }

        [HttpDelete]
        [Route("api/curriculum/classes/enrolments/delete/{id}")]
        public IHttpActionResult DeleteEnrolment(int id)
        {
            var enrolment = _context.CurriculumClassEnrolments.SingleOrDefault(x => x.Id == id);

            if (enrolment == null)
            {
                return Content(HttpStatusCode.NotFound, "Enrolment not found");
            }

            _context.CurriculumClassEnrolments.Remove(enrolment);
            _context.SaveChanges();

            return Ok(enrolment.Student.Person.LastName + ", " + enrolment.Student.Person.FirstName + " has been unenrolled from " + enrolment.CurriculumClass.Name);
        }

        #endregion

        #region Subjects
        [HttpPost]
        [Route("api/subjects/new")]
        public IHttpActionResult CreateSubject(CurriculumSubject data)
        {
            if (data.Name.IsNullOrWhiteSpace() || !ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var subjectToAdd = data;

            _context.CurriculumSubjects.Add(subjectToAdd);
            _context.SaveChanges();
            return Ok("Subject created");
        }

        [HttpDelete]
        [Route("api/subjects/delete/{subjectId}")]
        public IHttpActionResult DeleteSubject(int subjectId)
        {
            var subjectInDb = _context.CurriculumSubjects.SingleOrDefault(x => x.Id == subjectId);

            if (subjectInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Subject not found");
            }

            _context.AssessmentResults.RemoveRange(subjectInDb.AssessmentResults);
            _context.CurriculumSubjects.Remove(subjectInDb);
            _context.SaveChanges();
            return Ok("Subject deleted");
        }

        [HttpGet]
        [Route("api/subjects/byId/{subjectId}")]
        public CurriculumSubjectDto GetSubject(int subjectId)
        {
            var subject = _context.CurriculumSubjects.SingleOrDefault(x => x.Id == subjectId);

            if (subject == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<CurriculumSubject, CurriculumSubjectDto>(subject);
        }

        [HttpGet]
        [Route("api/subjects/all")]
        public IEnumerable<CurriculumSubjectDto> GetSubjects()
        {
            return _context.CurriculumSubjects.OrderBy(x => x.Name).ToList().Select(Mapper.Map<CurriculumSubject, CurriculumSubjectDto>);
        }

        [HttpPost]
        [Route("api/subjects/update")]
        public IHttpActionResult UpdateSubject(CurriculumSubject data)
        {
            var subjectInDb = _context.CurriculumSubjects.SingleOrDefault(x => x.Id == data.Id);

            if (subjectInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Subject not found");
            }

            subjectInDb.Name = data.Name;
            subjectInDb.LeaderId = data.LeaderId;
            _context.SaveChanges();
            return Ok("Subject updated");
        }

        #endregion

        #region Study Topics
        [HttpPost]
        [Route("api/studyTopics/create")]
        public IHttpActionResult CreateStudyTopic(CurriculumStudyTopic studyTopic)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            _context.CurriculumStudyTopics.Add(studyTopic);
            _context.SaveChanges();

            return Ok("Study topic added");
        }

        [HttpDelete]
        [Route("api/studyTopics/delete/{id}")]
        public IHttpActionResult DeleteStudyTopic(int id)
        {
            var studyTopic = _context.CurriculumStudyTopics.SingleOrDefault(x => x.Id == id);

            if (studyTopic == null)
            {
                return Content(HttpStatusCode.NotFound, "Study topic not found");
            }

            _context.CurriculumStudyTopics.Remove(studyTopic);
            _context.SaveChanges();

            return Ok("Study topic deleted");
        }

        [HttpGet]
        [Route("api/studyTopics/hasLessonPlans/{id}")]
        public bool HasLessonPlans(int id)
        {
            var studyTopic = _context.CurriculumStudyTopics.SingleOrDefault(x => x.Id == id);

            if (studyTopic == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return studyTopic.LessonPlans.Any();
        }
        
        [HttpGet]
        [Route("api/studyTopics/fetch/byId/{id}")]
        public CurriculumStudyTopicDto GetStudyTopic(int id)
        {
            var studyTopic = _context.CurriculumStudyTopics.SingleOrDefault(x => x.Id == id);

            if (studyTopic == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<CurriculumStudyTopic, CurriculumStudyTopicDto>(studyTopic);
        }

        [HttpGet]
        [Route("api/studyTopics/fetch/all")]
        public IEnumerable<CurriculumStudyTopicDto> GetStudyTopics()
        {
            return _context.CurriculumStudyTopics.ToList().Select(Mapper.Map<CurriculumStudyTopic, CurriculumStudyTopicDto>);
        }

        [HttpPost]
        [Route("api/studyTopics/update")]
        public IHttpActionResult UpdateStudyTopic(CurriculumStudyTopic studyTopic)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var studyTopicInDb = _context.CurriculumStudyTopics.SingleOrDefault(x => x.Id == studyTopic.Id);

            if (studyTopicInDb == null)
            {
                return Content(HttpStatusCode.BadRequest, "Study topic not found");
            }

            studyTopicInDb.Name = studyTopic.Name;
            studyTopicInDb.SubjectId = studyTopic.SubjectId;
            studyTopicInDb.YearGroupId = studyTopic.YearGroupId;

            _context.SaveChanges();

            return Ok("Study topic updated");
        }

        #endregion

        #region Lesson Plans
        /// <summary>
        /// Gets all lesson plans from the database (in alphabetical order).
        /// </summary>
        /// <returns>Returns a list of DTOs of all lesson plans from the database.</returns>
        [HttpGet]
        [Route("api/lessonPlans/all")]
        public IEnumerable<CurriculumLessonPlanDto> GetLessonPlans()
        {
            return _context.CurriculumLessonPlans.OrderBy(x => x.Title).ToList().Select(Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>);
        }

        /// <summary>
        /// Gets lesson plan from the database with the specified ID.
        /// </summary>
        /// <param name="id">ID of the lesson plan to fetch from the database.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("api/lessonPlans/byId/{id}")]
        public CurriculumLessonPlanDto GetLessonPlanById(int id)
        {
            var lessonPlan = _context.CurriculumLessonPlans.SingleOrDefault(x => x.Id == id);

            if (lessonPlan == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>(lessonPlan);
        }

        /// <summary>
        /// Gets lesson plans from the specified study topic.
        /// </summary>
        /// <param name="id">The ID of the study topic to get lesson plans from.</param>
        /// <returns>Returns a list of DTOs of lesson plans from the specified study topic.</returns>
        [HttpGet]
        [Route("api/lessonPlans/byTopic/{id}")]
        public IEnumerable<CurriculumLessonPlanDto> GetLessonPlansByTopic(int id)
        {
            return _context.CurriculumLessonPlans.Where(x => x.StudyTopicId == id).OrderBy(x => x.Title).ToList()
                .Select(Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>);
        }

        /// <summary>
        /// Adds a lesson plan to the database.
        /// </summary>
        /// <param name="plan">The lesson plan to add to the database</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/lessonPlans/create")]
        public IHttpActionResult CreateLessonPlan(CurriculumLessonPlan plan)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }
            
            var authorId = plan.AuthorId;

            var author = new StaffMember();

            if (authorId == 0)
            {
                var userId = User.Identity.GetUserId();
                author = _context.StaffMembers.SingleOrDefault(x => x.Person.UserId == userId);
                if (author == null)
                {
                    return Content(HttpStatusCode.BadRequest, "User does not have a personnel profile");
                }
            }

            if (authorId != 0)
            {
                author = _context.StaffMembers.SingleOrDefault(x => x.Id == authorId);
            }

            if (author == null)
            {
                return Content(HttpStatusCode.NotFound, "Staff member not found");
            }

            plan.AuthorId = author.Id;

            _context.CurriculumLessonPlans.Add(plan);
            _context.SaveChanges();

            return Ok("Lesson plan added");
        }

        /// <summary>
        /// Updates the lesson plan specified.
        /// </summary>
        /// <param name="plan">Lesson plan to update in the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/lessonPlans/update")]
        public IHttpActionResult UpdateLessonPlan(CurriculumLessonPlan plan)
        {
            var planInDb = _context.CurriculumLessonPlans.SingleOrDefault(x => x.Id == plan.Id);

            if (planInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Lesson plan not found");
            }

            planInDb.Title = plan.Title;
            planInDb.PlanContent = plan.PlanContent;
            planInDb.StudyTopicId = plan.StudyTopicId;
            planInDb.LearningObjectives = plan.LearningObjectives;
            planInDb.Homework = plan.Homework;

            _context.SaveChanges();

            return Ok("Lesson plan updated");
        }

        /// <summary>
        /// Deletes the specified lesson plan from the database.
        /// </summary>
        /// <param name="id">The ID of the lesson plan to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/lessonPlans/delete/{id}")]
        public IHttpActionResult DeleteLessonPlan(int id)
        {
            var plan = _context.CurriculumLessonPlans.SingleOrDefault(x => x.Id == id);

            if (plan == null)
            {
                return Content(HttpStatusCode.NotFound, "Lesson plan not found");
            }

            _context.CurriculumLessonPlans.Remove(plan);
            _context.SaveChanges();

            return Ok("Lesson plan deleted");
        }

        #endregion
    }
}
