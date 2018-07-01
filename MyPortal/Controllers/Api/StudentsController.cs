using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Misc;

namespace MyPortal.Controllers.Api
{
    public class StudentsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public StudentsController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public IEnumerable<StudentDto> GetStudents()
        {
            return _context.Students
                .Include(s => s.YearGroup1)
                .Include(s => s.RegGroup1)
                .ToList()
                .Select(Mapper.Map<Student, StudentDto>);
        }

        public StudentDto GetStudent(int id)
        {
            var student = _context.Students.SingleOrDefault(s => s.Id == id);

            if (student == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Student, StudentDto>(student);
        }

        [HttpPost]
        public StudentDto CreateStudent(StudentDto studentDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var student = Mapper.Map<StudentDto, Student>(studentDto);
            _context.Students
                .Add(student);

            _context.SaveChanges();

            studentDto.Id = student.Id;

            return studentDto;
        }

        [HttpPut]
        public IHttpActionResult UpdateStudent(int id, StudentDto studentDto)
        {
            if (studentDto == null)
                return Content(HttpStatusCode.BadRequest, "Invalid request data");

            var studentInDb = _context.Students.SingleOrDefault(s => s.Id == id);

            if (studentInDb == null)
                return Content(HttpStatusCode.NotFound, "Student not found");

            Mapper.Map(studentDto, studentInDb);
            studentInDb.FirstName = studentDto.FirstName;
            studentInDb.LastName = studentDto.LastName;
            studentInDb.FourMId = studentDto.FourMId;
            studentInDb.RegGroup = studentDto.RegGroup;
            studentInDb.YearGroup = studentDto.YearGroup;
            studentInDb.AccountBalance = studentDto.AccountBalance;

            _context.SaveChanges();

            return Ok("Student updated");
        }

        [HttpDelete]
        public IHttpActionResult DeleteStudent(int id)
        {
            var studentInDb = _context.Students.SingleOrDefault(s => s.Id == id);

            if (studentInDb == null)
                return Content(HttpStatusCode.NotFound, "Student not found");

            _context.Students.Remove(studentInDb);
            _context.SaveChanges();

            return Ok("Student deleted");
        }

        [HttpPost]
        [Route("api/students/credit")]
        public IHttpActionResult CreditAccount(BalanceAdjustment data)
        {
            if (data.Amount <= 0)
                return Content(HttpStatusCode.BadRequest, "Cannot credit negative amount");


            var studentInDb = _context.Students.SingleOrDefault(s => s.Id == data.Student);

            if (studentInDb == null)
                return Content(HttpStatusCode.NotFound, "Student not found");

            studentInDb.AccountBalance += data.Amount;

            _context.SaveChanges();

            return Ok("Account credited");
        }

        [HttpPost]
        [Route("api/students/debit")]
        public IHttpActionResult DebitAccount(BalanceAdjustment data)
        {
            if (data.Amount <= 0)
                return Content(HttpStatusCode.BadRequest, "Cannot debit negative amount");

            var studentInDb = _context.Students.SingleOrDefault(s => s.Id == data.Student);

            if (studentInDb == null)
                return Content(HttpStatusCode.NotFound, "Student not found");

            if (studentInDb.AccountBalance < data.Amount)
                return Content(HttpStatusCode.BadRequest, "Insufficient Funds");

            studentInDb.AccountBalance -= data.Amount;

            _context.SaveChanges();

            return Ok("Account debited");
        }
    }
}