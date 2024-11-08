using System;
using System.Threading.Tasks;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Services;

public class BaseServiceWithAccessControl : BaseService
{
    protected readonly IUserService UserService;
    protected readonly IPersonService PersonService;
    protected readonly IStudentService StudentService;
    
    public BaseServiceWithAccessControl(ISessionUser user, IUserService userService, IPersonService personService,
        IStudentService studentService) : base(user)
    {
        UserService = userService;
        PersonService = personService;
        StudentService = studentService;
    }

    protected async Task VerifyAccessToPerson(Guid personId)
    {
        if (!await AccessControlHelper.CanAccessPerson(User, UserService, PersonService, StudentService, personId))
        {
            throw new PermissionException("You do not have permission to access this person.");
        }
    }
}