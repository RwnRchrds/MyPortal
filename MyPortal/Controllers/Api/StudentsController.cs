using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Dtos.DataGrid;
using MyPortal.Models.Database;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/students")]
    public class StudentsController : MyPortalApiController
    {
        private readonly StudentService _service;

        public StudentsController()
        {
            _service = new StudentService(UnitOfWork);
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
        
        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("get/byId/{studentId:int}", Name = "ApiGetStudentById")]
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

        [HttpPost]
        [RequiresPermission("ViewStudents")]
        [Route("get/all/dataGrid", Name = "ApiGetAllStudentsDataGrid")]
        public async Task<IHttpActionResult> GetAllStudentsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var students = await _service.GetAllStudents();

                var list = students.Select(Mapper.Map<Student, GridStudentDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewStudents")]
        [Route("get/byRegGroup/{regGroupId:int}", Name = "ApiGetStudentsByRegGroup")]
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
        [Route("get/byYearGroup/{yearGroupId:int}", Name = "ApiGetStudentsByYearGroup")]
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
        [Route("update", Name = "ApiUpdateStudent")]
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