using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.LiteDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;
using MyPortal.Processes;

namespace MyPortal.Controllers.Api
{
    [Authorize(Roles = ("Staff"))]
    public class CurriculumClassesController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public CurriculumClassesController()
        {
            _context = new MyPortalDbContext();
        }

        public CurriculumClassesController(MyPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/curriculum/classes/byTeacher/{teacherId}/{dateString}")]
        public IEnumerable<CurriculumClassPeriodDto> GetClassesByTeacher(int teacherId, int dateString)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(User);
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
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(User);

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
            currClass.AcademicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(User);

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
                .OrderBy(x => x.Student.LastName)
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
                    enrolment.Student.LastName + ", " + enrolment.Student.FirstName + " is already enrolled in " +
                    enrolment.CurriculumClass.Name);
            }

            bool canEnroll;

            try
            {
                canEnroll = CurriculumProcesses.StudentCanEnroll(enrolment.StudentId, enrolment.ClassId);
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

            return Ok(enrolment.Student.LastName + ", " + enrolment.Student.FirstName + " has been unenrolled from " + enrolment.CurriculumClass.Name);
        }
    }
}
