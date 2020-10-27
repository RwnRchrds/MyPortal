using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Database.Models.Identity;
using MyPortal.Database.Search;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.List;

namespace MyPortalCore.Controllers.Api
{
    [Authorize]
    public class StudentController : BaseApiController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService, IUserService userService) : base(userService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("Search", Name = "ApiStudentSearch")]
        public async Task<IActionResult> SearchStudents([FromQuery] StudentSearchOptions searchModel)
        {
            return await ProcessAsync(async () =>
            {
                IEnumerable<StudentListModel> students;

                using (new ProcessTimer("Fetch students"))
                {
                    students = (await _studentService.Get(searchModel)).Select(x => x.GetDataGridModel());
                }

                return Ok(students);
            });
        }

        [HttpGet]
        [Route("GetById", Name = "ApiStudentGetById")]
        public async Task<IActionResult> GetById([FromQuery] Guid studentId)
        {
            return await ProcessAsync(async () =>
            {
                var student = await _studentService.GetById(studentId);

                return Ok(student);
            });
        }

        public override void Dispose()
        {
            _studentService.Dispose();
        }
    }
}