using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DataGrid;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    public class StudentController : BaseApiController
    {
        private readonly IStudentService _studentService;
        
        public StudentController(IUserService userService, IAcademicYearService academicYearService, IStudentService studentService) : base(userService, academicYearService)
        {
            _studentService = studentService;
        }
        
        [HttpGet]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("Search")]
        public async Task<IActionResult> SearchStudents([FromQuery] StudentSearchOptions searchModel)
        {
            return await ProcessAsync(async () =>
            {
                IEnumerable<StudentListModel> students;

                using (new ProcessTimer("Fetch students"))
                {
                    students = (await _studentService.Get(searchModel)).Select(x => x.GetListModel());
                }

                return Ok(students);
            });
        }

        [HttpGet]
        [Route("GetById")]
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

            base.Dispose();
        }
    }
}