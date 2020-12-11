using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortalWeb.Controllers.BaseControllers
{
    public abstract class StudentApiController : BaseApiController
    {
        protected readonly IStudentService StudentService;

        protected StudentApiController(IUserService userService, IAcademicYearService academicYearService,
            IRolePermissionsCache rolePermissionsCache, IStudentService studentService) : base(userService,
            academicYearService, rolePermissionsCache)
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
