using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;

namespace MyPortalWeb.Controllers.BaseControllers
{
    public abstract class StudentApiController : BaseApiController
    {
        protected readonly IStudentService StudentService;

        public StudentApiController(IUserService userService, IAcademicYearService academicYearService, IStudentService studentService) : base(userService, academicYearService)
        {
            StudentService = studentService;
        }

        protected async Task<bool> AuthenticateStudent(Guid studentId)
        {
            if (User.IsType(1))
            {
                var user = await UserService.GetUserByPrincipal(User);

                var student = await StudentService.GetByUserId(user.Id);

                if (student.Id == studentId)
                {
                    return true;
                }
            }
            else if (User.IsType(0))
            {
                return true;
            }

            return false;
        }

        public override void Dispose()
        {
            StudentService.Dispose();

            base.Dispose();
        }
    }
}
