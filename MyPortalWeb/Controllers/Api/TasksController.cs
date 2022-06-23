using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Person.Tasks;
using MyPortalWeb.Controllers.BaseControllers;
using MyPortalWeb.Models;
using MyPortalWeb.Models.Requests;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/tasks")]
    public class TasksController : PersonalDataController
    {
        private ITaskService _taskService;
        private IStaffMemberService _staffMemberService;

        public TasksController(IStudentService studentService, IUserService userService, IRoleService roleService,
            ITaskService taskService, IPersonService personService, IStaffMemberService staffMemberService) : base(
            studentService, personService, userService, roleService)
        {
            _taskService = taskService;
            _staffMemberService = staffMemberService;
        }

        [HttpGet]
        [Route("{taskId}")]
        [ProducesResponseType(typeof(TaskModel), 200)]
        public async Task<IActionResult> GetById([FromRoute] Guid taskId)
        {
            try
            {
                var task = await _taskService.GetById(taskId);

                var accessResponse = await GetPermissionsForTasksPerson(task.AssignedToId, false);

                if (accessResponse.CanAccess || accessResponse.IsOwner)
                {
                    return Ok(task);
                }

                return Error(HttpStatusCode.Forbidden, PermissionMessage);
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
                var accessResponse = await GetPermissionsForTasksPerson(personId, false);

                if (accessResponse.CanAccess || accessResponse.IsOwner)
                {
                    var tasks = (await _taskService.GetByPerson(personId, searchOptions)).ToArray();

                    return Ok(tasks);
                }

                return Error(HttpStatusCode.Forbidden, PermissionMessage);
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
        public async Task<IActionResult> Create([FromBody] CreateTaskRequestModel requestModel)
        {
            try
            {
                if (TaskTypes.IsReserved(requestModel.TypeId))
                {
                    return Error(HttpStatusCode.BadRequest, "Tasks of this type cannot be created manually.");
                }

                var user = await GetLoggedInUser();

                var accessResponse = await GetPermissionsForTasksPerson(requestModel.AssignedToId, true);

                if (accessResponse.CanAccess || accessResponse.IsOwner)
                {
                    requestModel.AssignedById = user.Id.Value;

                    await _taskService.Create(requestModel);

                    return Ok();
                }

                return Error(HttpStatusCode.Forbidden, "You do not have permission to create tasks for this person.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("update")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateTaskRequestModel requestModel)
        {
            try
            {
                if (await CanUpdateTask(requestModel.Id))
                {
                    await _taskService.Update(requestModel);

                    return Ok();   
                }

                return Error(HttpStatusCode.Forbidden, "You do not have permission to access this task");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("toggle")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ToggleCompleted([FromBody] TaskToggleRequestModel model)
        {
            try
            {
                if (await CanUpdateTask(model.TaskId))
                {
                    await _taskService.SetCompleted(model.TaskId, model.Completed);

                    return Ok();
                }

                return Error(HttpStatusCode.Forbidden, "You do not have permission to edit this task.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("delete/{taskId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromRoute] Guid taskId)
        {
            try
            {
                if (await CanUpdateTask(taskId))
                {
                    await _taskService.Delete(taskId);

                    return Ok();
                }

                return Error(HttpStatusCode.Forbidden, "You do not have permission to delete this task.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        private async Task<bool> CanUpdateTask(Guid taskId)
        {
            var user = await GetLoggedInUser();
            var task = await _taskService.GetById(taskId);

            var accessResponse = await GetPermissionsForTasksPerson(task.AssignedToId, true);

            if (accessResponse.CanAccess)
            {
                return true;
            }

            if (accessResponse.IsOwner)
            {
                return task.AssignedById == user.Id.Value || task.AllowEdit;
            }

            return false;
        }

        private async Task<TaskAccessResponse> GetPermissionsForTasksPerson(Guid personId, bool edit)
        {
            var response = new TaskAccessResponse();
            
            var user = await GetLoggedInUser();
            
            if (user.PersonId.Value == personId)
            {
                response.IsOwner = true;
            }

            var person = await PersonService.GetPersonWithTypes(personId);

            if (person.PersonTypes.IsStaff)
            {
                var allStaffPermission =
                    edit ? PermissionValue.PeopleEditAllStaffTasks : PermissionValue.PeopleViewAllStaffTasks;
                var managedStaffPermission =
                    edit ? PermissionValue.PeopleEditManagedStaffTasks : PermissionValue.PeopleViewManagedStaffTasks;
                
                if (await User.HasPermission(RoleService, PermissionRequirement.RequireAll,
                        allStaffPermission))
                {
                    response.CanAccess = true;
                }

                if (await User.HasPermission(RoleService, PermissionRequirement.RequireAll,
                        managedStaffPermission))
                {
                    if (user.PersonId.HasValue)
                    {
                        var staffMember = await _staffMemberService.GetByPersonId(person.Person.Id.Value);
                        var userPerson = await _staffMemberService.GetByPersonId(user.PersonId.Value);

                        if (staffMember != null && userPerson != null)
                        {
                            if (await _staffMemberService.IsLineManager(staffMember.Id.Value, userPerson.Id.Value))
                            {
                                response.CanAccess = true;
                            }
                        }
                    }

                }
            }
            
            if (person.PersonTypes.IsContact)
            {
                var contactPermission =
                    edit ? PermissionValue.PeopleEditContactTasks : PermissionValue.PeopleViewContactTasks;

                response.CanAccess =
                    await User.HasPermission(RoleService, PermissionRequirement.RequireAll, contactPermission);
            }

            if (person.PersonTypes.IsStudent)
            {
                var studentPermission =
                    edit ? PermissionValue.StudentEditStudentTasks : PermissionValue.StudentViewStudentTasks;

                response.CanAccess =
                    await User.HasPermission(RoleService, PermissionRequirement.RequireAll, studentPermission);
            }

            return response;
        }
    }
}