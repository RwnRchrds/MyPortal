using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;

namespace MyPortalCore.Controllers.Api
{
    public abstract class StudentApiController : BaseApiController
    {
        public StudentApiController(IUserService userService, IAcademicYearService academicYearService) : base(userService, academicYearService)
        {

        }

        protected async Task<bool> AuthenticateStudentResource(IStudentService studentService, Guid studentId)
        {
            if (User.IsType(UserTypes.Student))
            {
                var user = await _userService.GetUserByPrincipal(User);

                var student = await studentService.GetByUserId(user.Id);

                if (student.Id == studentId)
                {
                    return true;
                }
            }
            else if (User.IsType(UserTypes.Staff))
            {
                return true;
            }

            return false;
        }
    }
}
