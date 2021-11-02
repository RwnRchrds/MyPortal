using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortalWeb.Controllers.BaseControllers
{
    public abstract class StudentApiController : BaseApiController
    {
        protected async Task<bool> AuthoriseStudent(Guid requestedStudentId)
        {
            if (User.IsType(UserTypes.Student))
            {
                // Students can only access resources involving themselves
                var user = await Services.Users.GetUserByPrincipal(User);

                var student = await Services.Students.GetByUserId(user.Id.Value);

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

        protected StudentApiController(IAppServiceCollection services) : base(services)
        {
        }
    }
}
