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
using MyPortal.Logic.Models.Data.People;
using MyPortal.Logic.Models.Requests.Person.Tasks;
using MyPortalWeb.Controllers.BaseControllers;
using MyPortalWeb.Models;
using MyPortalWeb.Models.Requests;

namespace MyPortalWeb.Controllers.Api
{
    public class TasksController : PersonalDataController
    {
        private readonly ITaskService _taskService;
        private readonly IStaffMemberService _staffMemberService;

        public TasksController(IUserService userService, IPersonService personService, IStudentService studentService,
            ITaskService taskService, IStaffMemberService staffMemberService) 
            : base(userService, personService, studentService)
        {
            _taskService = taskService;
            _staffMemberService = staffMemberService;
        }

        [HttpGet]
        [Route("api/tasks/{taskId}")]
        [ProducesResponseType(typeof(TaskModel), 200)]
        public async Task<IActionResult> GetById([FromRoute] Guid taskId)
        {
            try
            {
                var task = await _taskService.GetTaskById(taskId);

                if (task.AssignedToId.HasValue)
                {
                    var accessResponse = await GetPermissionsForTasksPerson(task.AssignedToId.Value, false);

                    if (accessResponse.CanAccess || accessResponse.IsAssignee)
                    {
                        return Ok(task);
                    }
                }

                return Error(HttpStatusCode.Forbidden, PermissionMessage);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("api/people/{personId}/tasks")]
        [ProducesResponseType(typeof(IEnumerable<TaskModel>), 200)]
        public async Task<IActionResult> GetByPerson([FromRoute] Guid personId, [FromQuery] TaskSearchOptions searchOptions)
        {
            try
            {
                var accessResponse = await GetPermissionsForTasksPerson(personId, false);

                if (accessResponse.CanAccess || accessResponse.IsAssignee)
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
        [Route("api/tasks/types")]
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
        [Route("api/tasks")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] TaskRequestModel requestModel)
        {
            try
            {
                if (TaskTypes.IsReserved(requestModel.TypeId))
                {
                    return Error(HttpStatusCode.BadRequest, "Tasks of this type cannot be created manually.");
                }

                var userId = User.GetUserId();

                var accessResponse = await GetPermissionsForTasksPerson(requestModel.AssignedToId, true);

                if (accessResponse.CanAccess || accessResponse.IsAssignee)
                {
                    requestModel.AssignedById = userId;

                    await _taskService.CreateTask(requestModel);

                    return Ok();
                }

                return Error(HttpStatusCode.Forbidden, "You do not have permission to create tasks for this person.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("api/tasks/{taskId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromRoute] Guid taskId, [FromBody] TaskRequestModel requestModel)
        {
            try
            {
                if (await CanUpdateTask(taskId))
                {
                    await _taskService.UpdateTask(taskId, requestModel);

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
        [Route("api/tasks/{taskId}/complete")]
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
        [Route("api/tasks/{taskId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromRoute] Guid taskId)
        {
            try
            {
                if (await CanUpdateTask(taskId))
                {
                    await _taskService.DeleteTask(taskId);

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
            var userId = User.GetUserId();
            
            var task = await _taskService.GetTaskById(taskId);

            if (task.CreatedById != userId && task.AssignedToId.HasValue)
            {
                var accessResponse = await GetPermissionsForTasksPerson(task.AssignedToId.Value, true);

                if (accessResponse.CanAccess)
                {
                    return true;
                }

                if (accessResponse.IsAssignee)
                {
                    return task.CreatedById == userId || task.AllowEdit;
                }
            }

            return false;
        }

        private async Task<TaskAccessResponse> GetPermissionsForTasksPerson(Guid personId, bool edit)
        {
            var response = new TaskAccessResponse();
            
            var user = await GetLoggedInUser();
            
            if (user.PersonId != null && user.PersonId.Value == personId)
            {
                response.IsAssignee = true;
            }

            var person = await PersonService.GetPersonWithTypes(personId);

            if (person.PersonTypes.StaffId.HasValue)
            {
                var allStaffPermission =
                    edit ? PermissionValue.PeopleEditAllStaffTasks : PermissionValue.PeopleViewAllStaffTasks;
                var managedStaffPermission =
                    edit ? PermissionValue.PeopleEditManagedStaffTasks : PermissionValue.PeopleViewManagedStaffTasks;

                if (await User.HasPermission(UserService, PermissionRequirement.RequireAll,
                        allStaffPermission))
                {
                    response.CanAccess = true;
                }

                if (await User.HasPermission(UserService, PermissionRequirement.RequireAll,
                        managedStaffPermission))
                {
                    if (user.PersonId.HasValue && person.Person.Id.HasValue)
                    {
                        var staffMember = await _staffMemberService.GetByPersonId(person.Person.Id.Value);
                        var userPerson = await _staffMemberService.GetByPersonId(user.PersonId.Value);

                        if (staffMember is { Id: { } } && userPerson is {Id: { }})
                        {
                            if (await _staffMemberService.IsLineManager(staffMember.Id.Value, userPerson.Id.Value))
                            {
                                response.CanAccess = true;
                            }
                        }
                    }

                }
            }
            
            if (person.PersonTypes.ContactId.HasValue)
            {
                var contactPermission =
                    edit ? PermissionValue.PeopleEditContactTasks : PermissionValue.PeopleViewContactTasks;

                response.CanAccess =
                    await User.HasPermission(UserService, PermissionRequirement.RequireAll, contactPermission);
            }

            if (person.PersonTypes.StudentId.HasValue)
            {
                var studentPermission =
                    edit ? PermissionValue.StudentEditStudentTasks : PermissionValue.StudentViewStudentTasks;

                response.CanAccess =
                    await User.HasPermission(UserService, PermissionRequirement.RequireAll, studentPermission);
            }

            return response;
        }
    }
}