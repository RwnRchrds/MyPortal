using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using MyPortal.Dtos;
using MyPortal.Attributes;
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
        [Route("create", Name = "ApiPeopleCreateStudent")]
        public async Task<IHttpActionResult> CreateStudent([FromBody] Student student)
        {
            try
            {
                await PeopleProcesses.CreateStudent(student, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Student created");
        }

        [HttpDelete]
        [RequiresPermission("EditStudents")]
        [Route("delete/{studentId:int}", Name = "ApiPeopleDeleteStudent")]
        public IHttpActionResult DeleteStudent([FromUri] int studentId)
        {
            return PrepareResponse(PeopleProcesses.DeleteStudent(studentId, _context));
        }

        [Authorize]
        [RequiresPermission("ViewStudents")]
        [Route("get/byId/{studentId:int}", Name = "ApiPeopleGetStudentById")]
        public async Task<StudentDto> GetStudentById(int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleProcesses.GetStudentById(studentId, _context));
        }

        [RequiresPermission("ViewStudents")]
        [Route("get/all", Name = "ApiPeopleGetAllStudents")]
        public IEnumerable<StudentDto> GetStudents()
        {
            return PrepareResponseObject(PeopleProcesses.GetAllStudents(_context));
        }

        [HttpPost]
        [RequiresPermission("ViewStudents")]
        [Route("get/dataGrid/all", Name = "ApiPeopleGetAllStudentsDataGrid")]
        public IHttpActionResult GetAllStudentsDataGrid([FromBody] DataManagerRequest dm)
        {
            var students = PrepareResponseObject(PeopleProcesses.GetAllStudentsDataGrid(_context));

            return PrepareDataGridObject(students, dm);
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("get/byRegGroup/{regGroupId:int}", Name = "ApiPeopleGetStudentsByRegGroup")]
        public IEnumerable<StudentDto> GetStudentsByRegGroup([FromUri] int regGroupId)
        {
            return PrepareResponseObject(PeopleProcesses.GetStudentsByRegGroup(regGroupId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("get/byYearGroup/{yearGroupId:int}", Name = "ApiPeopleGetStudentsByYearGroup")]
        public IEnumerable<StudentDto> GetStudentsByYearGroup([FromUri] int yearGroupId)
        {
            return PrepareResponseObject(PeopleProcesses.GetStudentsByYearGroup(yearGroupId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("hasBasketItems/{studentId:int}")]
        public async Task<bool> StudentHasBasketItems([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleProcesses.StudentHasBasketItems(studentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("hasDocuments/{studentId:int}", Name = "ApiPeopleStudentHasDocuments")]
        public async Task<bool> StudentHasDocuments([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleProcesses.StudentHasDocuments(studentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("api/students/hasLogs/{studentId:int}", Name = "ApiPeopleStudentHasLogs")]
        public async Task<bool> StudentHasLogs([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleProcesses.StudentHasLogs(studentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("api/students/hasResults/{studentId:int}", Name = "ApiPeopleStudentHasResults")]
        public async Task<bool> StudentHasResults([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleProcesses.StudentHasResults(studentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("api/students/hasSales/{studentId:int}", Name = "ApiPeopleStudentHasSales")]
        public async Task<bool> StudentHasSales([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleProcesses.StudentHasSales(studentId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditStudents")]
        [Route("api/students/update", Name = "ApiPeopleUpdateStudent")]
        public async Task<IHttpActionResult> UpdateStudent([FromBody] Student student)
        {
            try
            {
                await PeopleProcesses.UpdateStudent(student, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Student updated");
        }
    }
}