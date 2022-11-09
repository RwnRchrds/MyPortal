using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Response.People;

namespace MyPortalWeb.Controllers.BaseControllers
{
    public abstract class PersonalDataController : BaseApiController
    {
        protected IStudentService StudentService;
        protected IPersonService PersonService;
        
        protected async Task<bool> CanAccessPerson(Guid requestedPersonId)
        {
            var person = await PersonService.GetPersonWithTypes(requestedPersonId);

            if (person == null)
            {
                return false;
            }
            
            if (User.IsType(UserTypes.Student))
            {
                // Students can only access resources involving themselves
                var userId = User.GetUserId();

                var student = await StudentService.GetStudentByUserId(userId);

                if (student.PersonId == requestedPersonId)
                {
                    return true;
                }
            }
            else if (User.IsType(UserTypes.Staff))
            {
                if (person.PersonTypes.StudentId.HasValue)
                {
                    // Staff members can access resources for all students if they have ViewStudentDetails permission
                    return await UserHasPermission(PermissionValue.StudentViewStudentDetails);
                }
                if (person.PersonTypes.StaffId.HasValue)
                {
                    // Staff members can access other basic staff information if they have the ViewBasicDetails permission
                    // Non basic details (e.g employment details) should require further permission checks
                    return await UserHasPermission(PermissionValue.PeopleViewStaffBasicDetails);
                }
                if (person.PersonTypes.ContactId.HasValue)
                {
                    // Staff members can access all contacts if they have ViewContactDetails permission
                    return await UserHasPermission(PermissionValue.PeopleViewContactDetails);
                }
            }
            else if (User.IsType(UserTypes.Parent))
            {
                // Parents can only access resources involving students that they have parental responsibility for
                var userId = User.GetUserId();
                var userPerson = await PersonService.GetPersonWithTypesByUser(userId);
                if (userPerson.PersonTypes.ContactId.HasValue)
                {
                    var students =
                        await StudentService.GetStudentsByContact(userPerson.PersonTypes.ContactId.Value, true);

                    return students.Any(s =>
                    {
                        if (s.Person.Id.HasValue && person.Person.Id.HasValue)
                        {
                            return s.Person.Id.Value == person.Person.Id.Value;
                        }

                        return false;
                    });
                }
            }

            return false;
        }

        protected PersonalDataController(IStudentService studentService, IPersonService personService,
            IUserService userService, IRoleService roleService) : base(userService, roleService)
        {
            StudentService = studentService;
            PersonService = personService;
        }
    }
}
