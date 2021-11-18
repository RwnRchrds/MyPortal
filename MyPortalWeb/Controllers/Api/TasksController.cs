using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Collection;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Person.Tasks;
using MyPortalWeb.Controllers.BaseControllers;
using MyPortalWeb.Models.Requests;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/tasks")]
    public class TasksController : StudentDataController
    {
        private ITaskService _taskService;
        private IPersonService _personService;
        private IStaffMemberService _staffMemberService;

        public TasksController(IStudentService studentService, IUserService userService, IRoleService roleService,
            ITaskService taskService, IPersonService personService, IStaffMemberService staffMemberService) : base(
            studentService, userService, roleService)
        {
            _taskService = taskService;
            _personService = personService;
            _staffMemberService = staffMemberService;
        }

        [HttpGet]
        [Route("id/{taskId}")]
        [ProducesResponseType(typeof(TaskModel), 200)]
        public async Task<IActionResult> GetById([FromRoute] Guid taskId)
        {
            try
            {
                var user = await UserService.GetUserByPrincipal(User);

                var task = await _taskService.GetById(taskId);

                if (await AuthorisePerson(task.AssignedToId, user.Id.Value, false))
                {
                    return Ok(task);
                }

                return Forbid();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("person/{personId}")]
        [ProducesResponseType(typeof(IEnumerable<TaskModel>), 200)]
        public async Task<IActionResult> GetByPerson([FromRoute] Guid personId, [FromQuery] TaskSearchOptions searchOptions)
        {
            try
            {
                var user = await UserService.GetUserByPrincipal(User);

                if (await AuthorisePerson(personId, user.Id.Value, false))
                {
                    var tasks = (await _taskService.GetByPerson(personId, searchOptions)).ToArray();

                    return Ok(tasks);
                }

                return Error(HttpStatusCode.Forbidden, "You do not have permission to access this resource.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("types")]
        [ProducesResponseType(typeof(IEnumerable<TaskTypeModel>), 200)]
        public async Task<IActionResult> GetTaskTypes([FromQuery] bool personal = false)
        {
            try
            {
                var taskTypes = await _taskService.GetTypes(personal);

                return Ok(taskTypes);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] CreateTaskModel model)
        {
            try
            {
                if (TaskTypes.IsReserved(model.TypeId))
                {
                    return Error(HttpStatusCode.BadRequest, "Tasks of this type cannot be created manually.");
                }

                var user = await UserService.GetUserByPrincipal(User);

                var canCreate = await AuthorisePerson(model.AssignedToId, user.Id.Value, true);

                if (canCreate)
                {
                    model.AssignedById = user.Id.Value;

                    await _taskService.Create(model);

                    return Ok();
                }

                return Forbid();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateTaskModel model)
        {
            try
            {
                var user = await UserService.GetUserByPrincipal(User);
                var task = await _taskService.GetById(model.Id);

                var canEdit = await AuthorisePerson(task.AssignedToId, user.Id.Value, true);

                if (canEdit)
                {
                    await _taskService.Update(model);

                    return Ok();
                }

                return Forbid();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("toggle")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ToggleCompleted([FromBody] TaskToggleRequestModel model)
        {
            try
            {
                var user = await UserService.GetUserByPrincipal(User);
                var task = await _taskService.GetById(model.TaskId);

                if (await AuthorisePerson(task.AssignedToId, user.Id.Value, true))
                {
                    await _taskService.SetCompleted(model.TaskId, model.Completed);

                    return Ok();
                }

                return Forbid();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromQuery] Guid taskId)
        {
            try
            {
                await _taskService.Delete(taskId);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        private async Task<bool> AuthorisePerson(Guid personId, Guid userId, bool edit)
        {
            var personInDb = await _personService.GetPersonWithTypes(personId);

            if (personInDb.PersonTypes.IsStudent)
            {
                if (User.IsType(UserTypes.Student))
                {
                    if (await UserHasPermission(PermissionValue.StudentViewStudentTasks))
                    {
                        var student = await StudentService.GetByPersonId(personId);

                        return await AuthoriseStudent(student.Id.Value);
                    }
                }

                else if (User.IsType(UserTypes.Staff))
                {
                    return await UserHasPermission(PermissionValue.StudentViewStudentTasks);
                }
            }

            else if (personInDb.PersonTypes.IsStaff)
            {
                if (edit && await UserHasPermission(PermissionValue.PeopleEditAllStaffTasks) ||
                    await UserHasPermission(PermissionValue.PeopleViewAllStaffTasks))
                {
                    return true;
                }

                var taskStaffMember = await _staffMemberService.GetByPersonId(personId, false);
                var userStaffMember = await _staffMemberService.GetByUserId(userId, false);

                if (userStaffMember != null && taskStaffMember != null)
                {
                    if (taskStaffMember.Id == userStaffMember.Id)
                    {
                        return true;
                    }

                    return (edit && await UserHasPermission(PermissionValue.PeopleEditManagedStaffTasks) ||
                            await UserHasPermission(PermissionValue.PeopleViewManagedStaffTasks)) &&
                           await _staffMemberService.IsLineManager(taskStaffMember.Id.Value, userStaffMember.Id.Value);
                }
            }

            return false;
        }
    }
}