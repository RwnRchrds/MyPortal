using System.Collections.Generic;
using System.Web.Http;
using MyPortal.Dtos;
using MyPortal.Models.Attributes;
using MyPortal.Models.Database;
using MyPortal.Processes;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/people/students")]
    public class StudentsController : MyPortalApiController
    {
        [HttpPost]
        [RequiresPermission("EditStudents")]
        [Route("create")]
        public IHttpActionResult CreateStudent([FromBody] Student student)
        {
            return PrepareResponse(PeopleProcesses.CreateStudent(student, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditStudents")]
        [Route("delete/{studentId:int}")]
        public IHttpActionResult DeleteStudent([FromUri] int studentId)
        {
            return PrepareResponse(PeopleProcesses.DeleteStudent(studentId, _context));
        }

        [Authorize]
        [RequiresPermission("ViewStudents")]
        [Route("get/byId/{studentId:int}")]
        public StudentDto GetStudent(int studentId)
        {
            AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleProcesses.GetStudentById(studentId, _context));
        }

        [RequiresPermission("ViewStudents")]
        [Route("get/all")]
        public IEnumerable<StudentDto> GetStudents()
        {
            return PrepareResponseObject(PeopleProcesses.GetAllStudents(_context));
        }

        [HttpPost]
        [RequiresPermission("ViewStudents")]
        [Route("get/dataGrid/all")]
        public IHttpActionResult GetStudentsForDataGrid([FromBody] DataManagerRequest dm)
        {
            var students = PrepareResponseObject(PeopleProcesses.GetAllStudents_DataGrid(_context));

            return PrepareDataGridObject(students, dm);
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("get/byRegGroup/{regGroupId:int}")]
        public IEnumerable<StudentDto> GetStudentsByRegGroup([FromUri] int regGroupId)
        {
            return PrepareResponseObject(PeopleProcesses.GetStudentsByRegGroup(regGroupId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("get/byYearGroup/{yearGroupId:int}")]
        public IEnumerable<StudentDto> GetStudentsFromYear([FromUri] int yearGroupId)
        {
            return PrepareResponseObject(PeopleProcesses.GetStudentsByYearGroup(yearGroupId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("hasBasketItems/{studentId:int}")]
        public bool StudentHasBasketItems([FromUri] int studentId)
        {
            AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleProcesses.StudentHasBasketItems(studentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("hasDocuments/{studentId:int}")]
        public bool StudentHasDocuments([FromUri] int studentId)
        {
            AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleProcesses.StudentHasDocuments(studentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("api/students/hasLogs/{studentId:int}")]
        public bool StudentHasLogs([FromUri] int studentId)
        {
            AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleProcesses.StudentHasLogs(studentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("api/students/hasResults/{studentId:int}")]
        public bool StudentHasResults([FromUri] int studentId)
        {
            AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleProcesses.StudentHasResults(studentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("api/students/hasSales/{studentId:int}")]
        public bool StudentHasSales([FromUri] int studentId)
        {
            AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleProcesses.StudentHasSales(studentId, _context));
        }

        [HttpPut]
        [RequiresPermission("EditStudents")]
        public IHttpActionResult UpdateStudent([FromBody] Student student)
        {
            return PrepareResponse(PeopleProcesses.UpdateStudent(student, _context));
        }
    }
}