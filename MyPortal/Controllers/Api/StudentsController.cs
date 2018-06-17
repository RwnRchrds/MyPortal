using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

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
        public void UpdateStudent(int id, StudentDto studentDto)
        {
            if (studentDto == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var studentInDb = _context.Students.SingleOrDefault(s => s.Id == id);

            if (studentInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var c = Mapper.Map(studentDto, studentInDb);
            studentInDb.FirstName = studentDto.FirstName;
            studentInDb.LastName = studentDto.LastName;
            studentInDb.FourMId = studentDto.FourMId;
            studentInDb.RegGroup = studentDto.RegGroup;
            studentInDb.YearGroup = studentDto.YearGroup;
            studentInDb.AccountBalance = studentDto.AccountBalance;

            _context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteStudent(int id)
        {
            var studentInDb = _context.Students.SingleOrDefault(s => s.Id == id);

            if (studentInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Students.Remove(studentInDb);
            _context.SaveChanges();
        }

        [HttpPost]
        [Route("api/students/credit")]
        public void CreditAccount(BalanceAdjustment data)
        {
            if (data.Amount <= 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);



            var studentInDb = _context.Students.SingleOrDefault(s => s.Id == data.Student);

            if (studentInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            studentInDb.AccountBalance += data.Amount;

            _context.SaveChanges();
        }

        [HttpPost]
        [Route("api/students/debit")]
        public void DebitAccount(BalanceAdjustment data)
        {
            if (data.Amount <= 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var studentInDb = _context.Students.SingleOrDefault(s => s.Id == data.Student);

            if (studentInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            if (studentInDb.AccountBalance < data.Amount)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            studentInDb.AccountBalance -= data.Amount;

            _context.SaveChanges();
        }
    }
}