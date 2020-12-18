using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
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

        protected async Task<bool> AuthoriseStudent(Guid requestedStudentId)
        {
            if (User.IsType(UserTypes.Student))
            {
                // Students can only access resources involving themselves
                var user = await UserService.GetUserByPrincipal(User);

                var student = await StudentService.GetByUserId(user.Id);

                if (student.Id == requestedStudentId)
                {
                    return true;
                }
            }
            else if (User.IsType(UserTypes.Staff))
            {
                // All staff members are "authorised" for student requests - combine with permission requirements for enhanced access control
                return true;
            }
            else if (User.IsType(UserTypes.Parent))
            {
                // TODO - Add this functionality in when it becomes available
                return false;
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
