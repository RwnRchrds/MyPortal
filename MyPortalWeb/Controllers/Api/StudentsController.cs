using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyPortal.Database.Constants;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.Entity;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/students")]
    public class StudentsController : StudentApiController
    {
        public StudentsController(IUserService userService, IAcademicYearService academicYearService,
            IRolePermissionsCache rolePermissionsCache, IStudentService studentService) : base(userService,
            academicYearService, rolePermissionsCache, studentService)
        {
        }

        [HttpGet]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("search")]
        [Produces(typeof(IEnumerable<StudentDataGridModel>))]
        public async Task<IActionResult> SearchStudents([FromQuery] StudentSearchOptions searchModel)
        {
            return await ProcessAsync(async () =>
            {
                IEnumerable<StudentDataGridModel> students;

                using (new ProcessTimer("Fetch students"))
                {
                    students = (await StudentService.Get(searchModel)).Select(x => x.GetDataGridModel());
                }

                return Ok(students);
            }, Permissions.Student.StudentDetails.ViewStudentDetails);
        }

        [HttpGet]
        [Route("id")]
        [Produces(typeof(IEnumerable<StudentModel>))]
        public async Task<IActionResult> GetById([FromQuery] Guid studentId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthenticateStudent(studentId))
                {
                    var student = await StudentService.GetById(studentId);

                    return Ok(student);
                }

                return Forbid();
            }, Permissions.Student.StudentDetails.ViewStudentDetails);
        }
    }
}