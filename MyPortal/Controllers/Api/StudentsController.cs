using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Models.Database;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/people/students")]
    public class StudentsController : MyPortalApiController
    {
        private readonly StudentService _service;

        public StudentsController()
        {
            _service = new StudentService(UnitOfWork);
        }
        
        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("get/byId/{studentId:int}", Name = "ApiPeopleGetStudentById")]
        public async Task<StudentDto> GetStudentById([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            try
            {
                var student = await _service.GetStudentById(studentId);

                return Mapper.Map<Student, StudentDto>(student);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("get/byRegGroup/{regGroupId:int}", Name = "ApiPeopleGetStudentsByRegGroup")]
        public async Task<IEnumerable<StudentDto>> GetStudentsByRegGroup([FromUri] int regGroupId)
        {
            try
            {
                var students = await _service.GetStudentsByRegGroup(regGroupId);

                return students.Select(Mapper.Map<Student, StudentDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("get/byYearGroup/{yearGroupId:int}", Name = "ApiPeopleGetStudentsByYearGroup")]
        public async Task<IEnumerable<StudentDto>> GetStudentsByYearGroup([FromUri] int yearGroupId)
        {
            try
            {
                var students = await _service.GetStudentsByYearGroup(yearGroupId);

                return students.Select(Mapper.Map<Student, StudentDto>);
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
                await _service.UpdateStudent(student);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Student updated");
        }
    }
}