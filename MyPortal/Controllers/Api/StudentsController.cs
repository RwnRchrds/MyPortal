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

        /// <summary>
        /// Checks that the current student user's ID matches the requested student's ID.
        /// </summary>
        /// <param name="id">The ID of the student request.</param>
        /// <exception cref="HttpResponseException">Thrown if the student is not found or if the requested student does not match the current user.</exception>
        public void AuthenticateStudentRequest(int id)
        {
                var userId = User.Identity.GetUserId();
                var studentUser = _context.Students.SingleOrDefault(x => x.UserId == userId);
                var requestedStudent = _context.Students.SingleOrDefault(x => x.Id == id);

                if (studentUser == null || requestedStudent == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                if (studentUser.Id != requestedStudent.Id)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
        }

        /// <summary>
        /// Adds a document to the specified student student.
        /// </summary>
        /// <param name="data">The student document object to add</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Authorize(Roles = "Staff, SeniorStaff")]
        [Route("api/students/documents/add")]
        public IHttpActionResult AddDocument(StudentDocumentUpload data)
        {
            var student = _context.Students.SingleOrDefault(x => x.Id == data.Student);

            if (student == null)
            {
                return Content(HttpStatusCode.NotFound, "Student not found");
            }

            var document = data.Document;

            var currentUserId = User.Identity.GetUserId();

            var uploader = _context.Staff.Single(x => x.UserId == currentUserId);

            if (uploader == null)
            {
                return Content(HttpStatusCode.BadRequest, "Uploader not found");
            }

            document.UploaderId = uploader.Id;

            document.IsGeneral = false;

            document.Approved = true;

            document.Date = DateTime.Now;

            var isUriValid = Uri.TryCreate(document.Url, UriKind.Absolute, out var uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isUriValid)
            {
                return Content(HttpStatusCode.BadRequest, "The URL entered is not valid");
            }

            _context.Documents.Add(document);
            _context.SaveChanges();

            var studentDocument = new StudentDocument
            {
                DocumentId = document.Id,
                StudentId = data.Student
            };

            _context.StudentDocuments.Add(studentDocument);
            _context.SaveChanges();

            return Ok("Document added");
        }

        /// <summary>
        /// Adds a student.
        /// </summary>
        /// <param name="student">The student object to add.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException">Thrown when the model state is invalid.</exception>
        [HttpPost]
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IHttpActionResult CreateStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.Students
                .Add(student);

            _context.SaveChanges();
            return Ok("Student added");
        }

        /// <summary>
        /// Credits (increases) the student's account balance by the specified amount.
        /// </summary>
        /// <param name="data">The balance adjustment object to credit the account.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/students/credit")]
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IHttpActionResult CreditAccount(BalanceAdjustment data)
        {
            if (data.Amount <= 0)
            {
                return Content(HttpStatusCode.BadRequest, "Cannot credit negative amount");
            }

            var studentInDb = _context.Students.SingleOrDefault(s => s.Id == data.Student);

            if (studentInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Student not found");
            }

            studentInDb.AccountBalance += data.Amount;

            _context.SaveChanges();

            return Ok("Account credited");
        }

        /// <summary>
        /// Debits (decreases) the student's account balance by the specified amount.
        /// </summary>
        /// <param name="data">The balance adjustment object to debit the account.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/students/debit")]
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IHttpActionResult DebitAccount(BalanceAdjustment data)
        {
            if (data.Amount <= 0)
            {
                return Content(HttpStatusCode.BadRequest, "Cannot debit negative amount");
            }

            var studentInDb = _context.Students.SingleOrDefault(s => s.Id == data.Student);

            if (studentInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Student not found");
            }

            if (studentInDb.AccountBalance < data.Amount)
            {
                return Content(HttpStatusCode.BadRequest, "Insufficient Funds");
            }

            studentInDb.AccountBalance -= data.Amount;

            _context.SaveChanges();

            return Ok("Account debited");
        }

        /// <summary>
        /// Deletes the specified student.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IHttpActionResult DeleteStudent(int id)
        {
            var studentInDb = _context.Students.SingleOrDefault(s => s.Id == id);

            if (studentInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Student not found");
            }

            _context.Students.Remove(studentInDb);
            _context.SaveChanges();

            return Ok("Student deleted");
        }

        /// <summary>
        /// Gets the account balance for the specified student
        /// </summary>
        /// <param name="studentId">The ID of the student to query the balance for.</param>
        /// <returns>Returns the specified student's account balance.</returns>
        /// <exception cref="HttpResponseException">Thrown when the student is not found.</exception>
        [HttpGet]
        [Route("api/students/balance")]
        public decimal GetBalance(int studentId)
        {
            if (User.IsInRole("Student"))
            {
                AuthenticateStudentRequest(studentId);
            }
            
            var studentInDb = _context.Students.SingleOrDefault(x => x.Id == studentId);

            if (studentInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return studentInDb.AccountBalance;
        }

        /// <summary>
        /// Fetches the specified document.
        /// </summary>
        /// <param name="documentId">The ID of the document to fetch.</param>
        /// <returns>Returns a DTO of the specified document.</returns>
        /// <exception cref="HttpResponseException">Thrown when the document is not found.</exception>
        [HttpGet]
        [Route("api/students/documents/document/{documentId}")]
        public DocumentDto GetDocument(int documentId)
        {
            var document = _context.StudentDocuments
                .SingleOrDefault(x => x.Id == documentId);

            if (document == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (User.IsInRole("Student"))
            {
                AuthenticateStudentRequest(document.StudentId);
            }

            return Mapper.Map<Document, DocumentDto>(document.Document);
        }

        /// <summary>
        /// Gets a list of documents for the specified student.
        /// </summary>
        /// <param name="studentId">The ID of the student to fetch documents for.</param>
        /// <returns>Returns a list of DTOs of documents for the specified student.</returns>
        /// <exception cref="HttpResponseException">Thrown when the student is not found.</exception>
        [HttpGet]
        [Route("api/students/documents/fetch/{studentId}")]
        public IEnumerable<StudentDocumentDto> GetDocuments(int studentId)
        {
            if (User.IsInRole("Student"))
            {
                AuthenticateStudentRequest(studentId);
            }
            
            var student = _context.Students.SingleOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var documents = _context.StudentDocuments
                .Where(x => x.StudentId == studentId)
                .ToList()
                .Select(Mapper.Map<StudentDocument, StudentDocumentDto>);

            return documents;
        }

        /// <summary>
        /// Fetches the specified student.
        /// </summary>
        /// <param name="id">The ID of the student to fetch.</param>
        /// <returns>Returns a DTO of the specified student.</returns>
        /// <exception cref="HttpResponseException">Thrown when the student is not found.</exception>
        [Authorize]
        [Route("api/students/{id}")]
        public StudentDto GetStudent(int id)
        {
            if (User.IsInRole("Student"))
            {
                AuthenticateStudentRequest(id);
            }
            
            var student = _context.Students.SingleOrDefault(s => s.Id == id);

            if (student == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<Student, StudentDto>(student);
        }

        /// <summary>
        /// Gets a list of all students.
        /// </summary>
        /// <returns>Returns a list of DTOs of all students.</returns>
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IEnumerable<StudentDto> GetStudents()
        {
            return _context.Students
                .Include(s => s.YearGroup)
                .Include(s => s.RegGroup)
                .OrderBy(x => x.LastName)
                .ToList()
                .Select(Mapper.Map<Student, StudentDto>);
        }

        /// <summary>
        /// Gets a list of all students in the specified registration group.
        /// </summary>
        /// <param name="regGroupId">The ID of the registration group to fetch students from.</param>
        /// <returns>Returns a list of DTOs of students in the specified registration group.</returns>
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IEnumerable<StudentDto> GetStudentsByRegGroup(int regGroupId)
        {
            return _context.Students
                .Where(x => x.RegGroupId == regGroupId)
                .OrderBy(x => x.LastName)
                .ToList()
                .Select(Mapper.Map<Student, StudentDto>);
        }

        /// <summary>
        /// Gets a list of all students in the specified year group.
        /// </summary>
        /// <param name="yearGroupId">The ID of the year group to fetch students from.</param>
        /// <returns>Returns a list of DTOs of students in the specified year group.</returns>
        [HttpGet]
        [Authorize(Roles = "Staff, SeniorStaff")]
        [Route("api/students/yearGroup/{yearGroupId}")]
        public IEnumerable<StudentDto> GetStudentsFromYear(int yearGroupId)
        {
            return _context.Students
                .Where(x => x.YearGroupId == yearGroupId)
                .OrderBy(x => x.LastName)
                .ToList()
                .Select(Mapper.Map<Student, StudentDto>);
        }

        /// <summary>
        /// Deletes the specified document.
        /// </summary>
        /// <param name="documentId">The ID of the document to delete.</param>
        /// <returns>Returns a NegotiatedContentResult stating whether or not the action was successful.</returns>
        [HttpDelete]
        [Authorize(Roles = "Staff, SeniorStaff")]
        [Route("api/students/documents/remove/{documentId}")]
        public IHttpActionResult RemoveDocument(int documentId)
        {
            var studentDocument = _context.StudentDocuments.SingleOrDefault(x => x.Id == documentId);

            if (studentDocument == null)
            {
                return Content(HttpStatusCode.NotFound, "Document not found");
            }

            var attachedDocument = studentDocument.Document;

            if (attachedDocument == null)
            {
                return Content(HttpStatusCode.BadRequest, "No document attached");
            }

            _context.StudentDocuments.Remove(studentDocument);

            _context.Documents.Remove(attachedDocument);

            _context.SaveChanges();

            return Ok("Document deleted");
        }

        [HttpGet]
        [Route("api/students/hasBasketItems/{id}")]
        public bool StudentHasBasketItems(int id)
        {
            if (User.IsInRole("Student"))
            {
                AuthenticateStudentRequest(id);
            }
            
            var studentInDb = _context.Students.SingleOrDefault(x => x.Id == id);

            if (studentInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return studentInDb.BasketItems.Any();
        }

        [HttpGet]
        [Route("api/students/hasDocuments/{id}")]
        public bool StudentHasDocuments(int id)
        {
            if (User.IsInRole("Student"))
            {
                AuthenticateStudentRequest(id);
            }
            
            var studentInDb = _context.Students.SingleOrDefault(x => x.Id == id);

            if (studentInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return studentInDb.StudentDocuments.Any();
        }

        [HttpGet]
        [Route("api/students/hasLogs/{id}")]
        public bool StudentHasLogs(int id)
        {
            if (User.IsInRole("Student"))
            {
                AuthenticateStudentRequest(id);
            }
            
            var studentInDb = _context.Students.SingleOrDefault(x => x.Id == id);

            if (studentInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return studentInDb.Logs.Any();
        }

        [HttpGet]
        [Route("api/students/hasResults/{id}")]
        public bool StudentHasResults(int id)
        {
            if (User.IsInRole("Student"))
            {
                AuthenticateStudentRequest(id);
            }
            
            var studentInDb = _context.Students.SingleOrDefault(x => x.Id == id);

            if (studentInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return studentInDb.Results.Any();
        }

        [HttpGet]
        [Route("api/students/hasSales/{id}")]
        public bool StudentHasSales(int id)
        {
            if (User.IsInRole("Student"))
            {
                AuthenticateStudentRequest(id);
            }
            
            var studentInDb = _context.Students.SingleOrDefault(x => x.Id == id);

            if (studentInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return studentInDb.Sales.Any();
        }

        [HttpPost]
        [Authorize(Roles = "Staff, SeniorStaff")]
        [Route("api/students/documents/edit")]
        public IHttpActionResult UpdateDocument(Document data)
        {
            var documentInDb = _context.Documents.Single(x => x.Id == data.Id);

            if (documentInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Document not found");
            }

            var isUriValid = Uri.TryCreate(data.Url, UriKind.Absolute, out var uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isUriValid)
            {
                return Content(HttpStatusCode.BadRequest, "The URL entered is not valid");
            }

            documentInDb.Description = data.Description;
            documentInDb.Url = data.Url;
            documentInDb.IsGeneral = false;
            documentInDb.Approved = true;

            _context.SaveChanges();

            return Ok("Document updated");
        }

        [HttpPut]
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IHttpActionResult UpdateStudent(Student student)
        {
            if (student == null || !ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid request data");
            }

            var studentInDb = _context.Students.SingleOrDefault(s => s.Id == student.Id);

            if (studentInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Student not found");
            }

            Mapper.Map(student, studentInDb);
            studentInDb.FirstName = student.FirstName;
            studentInDb.LastName = student.LastName;
            studentInDb.Gender = student.Gender;
            studentInDb.RegGroupId = student.RegGroupId;
            studentInDb.YearGroupId = student.YearGroupId;
            studentInDb.AccountBalance = student.AccountBalance;

            _context.SaveChanges();

            return Ok("Student updated");
        }
    }
}