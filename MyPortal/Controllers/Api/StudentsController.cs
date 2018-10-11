using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Misc;

namespace MyPortal.Controllers.Api
{
    [Authorize]
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

        [Authorize(Roles="Staff, SeniorStaff")]
        public IEnumerable<StudentDto> GetStudents()
        {
            return _context.Students
                .Include(s => s.YearGroup)
                .Include(s => s.RegGroup)
                .ToList()
                .Select(Mapper.Map<Student, StudentDto>);
        }

        [Authorize]
        public StudentDto GetStudent(int id)
        {
            var student = _context.Students.SingleOrDefault(s => s.Id == id);

            if (student == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Student, StudentDto>(student);
        }

        [HttpPost]
        [Authorize(Roles="Staff, SeniorStaff")]
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
        [Authorize(Roles="Staff, SeniorStaff")]
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
            studentInDb.RegGroupId = studentDto.RegGroupId;
            studentInDb.YearGroupId = studentDto.YearGroupId;
            studentInDb.AccountBalance = studentDto.AccountBalance;

            _context.SaveChanges();

            return Ok("Student updated");
        }

        [HttpDelete]
        [Authorize(Roles="Staff, SeniorStaff")]
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
        [Authorize(Roles="Staff, SeniorStaff")]
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
        [Authorize(Roles="Staff, SeniorStaff")]
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

        //GET ACCOUNT BALANCE
        [HttpGet]
        [Authorize]
        [Route("api/students/balance")]
        public decimal GetBalance(int student)
        {
            var studentInDb = _context.Students.SingleOrDefault(x => x.Id == student);

            if (studentInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return studentInDb.AccountBalance;
        }

        [HttpGet]
        [Route("api/students/documents/fetch/{studentId}")]
        public IEnumerable<StudentDocumentDto> GetDocuments(int studentId)
        {
            var student = _context.Students.SingleOrDefault(s => s.Id == studentId);

            if (student == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var documents = _context.StudentDocuments
                .Where(x => x.StudentId == studentId)
                .ToList()
                .Select(Mapper.Map<StudentDocument, StudentDocumentDto>);

            return documents;
        }

        [HttpGet]
        [Route("api/students/documents/document/{documentId}")]
        public DocumentDto GetDocument(int documentId)
        {      
            var document = _context.StudentDocuments
                .SingleOrDefault(x => x.Id == documentId);             
            
            if (document == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Document, DocumentDto>(document.Document);
        }

        [HttpPost]
        [Route("api/students/documents/add")]
        public IHttpActionResult AddDocument(StudentDocumentUpload data)
        {
            var student = _context.Students.SingleOrDefault(x => x.Id == data.Student);

            if (student == null)
                return Content(HttpStatusCode.NotFound, "Student not found");

            var document = data.Document;

            var currentUserId = User.Identity.GetUserId();

            var userProfile = _context.Staff.Single(x => x.UserId == currentUserId);

            document.UploaderId = userProfile.Id;

            document.IsGeneral = false;

            document.Approved = true;

            document.Date = DateTime.Now;

            var isUriValid = Uri.TryCreate(document.Url, UriKind.Absolute, out var uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isUriValid)
                return Content(HttpStatusCode.BadRequest, "The URL entered is not valid");

            _context.Documents.Add(document);
            _context.SaveChanges();

            var studentDocument = new StudentDocument()
            {
                DocumentId = document.Id,
                StudentId = data.Student
            };

            _context.StudentDocuments.Add(studentDocument);
            _context.SaveChanges();

            return Ok("Document added");
        }

        [HttpDelete]
        [Route("api/students/documents/remove/{documentId}")]
        public IHttpActionResult RemoveDocument(int documentId)
        {
            var studentDocument = _context.StudentDocuments.SingleOrDefault(x => x.Id == documentId);

            if (studentDocument == null)
                return Content(HttpStatusCode.NotFound, "Document not found");

            var attachedDocument = studentDocument.Document;

            if (attachedDocument == null)
                return Content(HttpStatusCode.BadRequest, "No document attached");

            _context.StudentDocuments.Remove(studentDocument);

            _context.Documents.Remove(attachedDocument);

            _context.SaveChanges();

            return Ok("Document deleted");
        }

        [HttpPost]
        [Route("api/students/documents/edit")]
        public IHttpActionResult UpdateDocument(DocumentDto data)
        {
            var documentInDb = _context.Documents.Single(x => x.Id == data.Id);

            if (documentInDb == null)
                return Content(HttpStatusCode.NotFound, "Document not found");

            var isUriValid = Uri.TryCreate(data.Url, UriKind.Absolute, out var uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isUriValid)
                return Content(HttpStatusCode.BadRequest, "The URL entered is not valid");

            documentInDb.Description = data.Description;
            documentInDb.Url = data.Url;
            documentInDb.IsGeneral = false;
            documentInDb.Approved = true;

            _context.SaveChanges();

            return Ok("Document updated");
        }

        [HttpGet]
        [Route("api/students/yearGroup/{yearGroupId}")]
        public IEnumerable<StudentDto> GetStudentsFromYear(int yearGroupId)
        {
            return _context.Students
                .Where(x => x.YearGroupId == yearGroupId)
                .OrderBy(x => x.LastName)
                .ToList()
                .Select(Mapper.Map<Student, StudentDto>);
        }
    }
}