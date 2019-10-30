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

        [HttpPost]
        [RequiresPermission("EditStudents")]
        [Route("api/students/update", Name = "ApiPeopleUpdateStudent")]
        public async Task<IHttpActionResult> UpdateStudent([FromBody] Student student)
        {
            try
            {
                await StudentService.UpdateStudent(student);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Student updated");
        }
    }
}