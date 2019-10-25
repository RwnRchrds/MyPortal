using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Models.Database;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/people/students")]
    public class StudentsController : MyPortalApiController
    {
        

        

        [Authorize]
        [RequiresPermission("ViewStudents")]
        [Route("get/byId/{studentId:int}", Name = "ApiPeopleGetStudentById")]
        public async Task<StudentDto> GetStudentById(int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            return await StudentService.GetStudentById(studentId, _context);
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("get/byRegGroup/{regGroupId:int}", Name = "ApiPeopleGetStudentsByRegGroup")]
        public async Task<IEnumerable<StudentDto>> GetStudentsByRegGroup([FromUri] int regGroupId)
        {
            return await StudentService.GetStudentsByRegGroup(regGroupId, _context);
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("get/byYearGroup/{yearGroupId:int}", Name = "ApiPeopleGetStudentsByYearGroup")]
        public async Task<IEnumerable<StudentDto>> GetStudentsByYearGroup([FromUri] int yearGroupId)
        {
            try
            {
                return await StudentService.GetStudentsByYearGroup(yearGroupId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("hasBasketItems/{studentId:int}")]
        public async Task<bool> StudentHasBasketItems([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            try
            {
                return await StudentService.StudentHasBasketItems(studentId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("hasDocuments/{studentId:int}", Name = "ApiPeopleStudentHasDocuments")]
        public async Task<bool> StudentHasDocuments([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleService.StudentHasDocuments(studentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("api/students/hasLogs/{studentId:int}", Name = "ApiPeopleStudentHasLogs")]
        public async Task<bool> StudentHasLogs([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleService.StudentHasLogs(studentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("api/students/hasResults/{studentId:int}", Name = "ApiPeopleStudentHasResults")]
        public async Task<bool> StudentHasResults([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleService.StudentHasResults(studentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("api/students/hasSales/{studentId:int}", Name = "ApiPeopleStudentHasSales")]
        public async Task<bool> StudentHasSales([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(PeopleService.StudentHasSales(studentId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditStudents")]
        [Route("api/students/update", Name = "ApiPeopleUpdateStudent")]
        public async Task<IHttpActionResult> UpdateStudent([FromBody] Student student)
        {
            try
            {
                await PeopleService.UpdateStudent(student, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Student updated");
        }
    }
}