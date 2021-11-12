using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
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
        [Route("id")]
        [ProducesResponseType(typeof(TaskModel), 200)]
        public async Task<IActionResult> GetById([FromQuery] Guid taskId)
        {
            try
            {
                var user = await UserService.GetUserByPrincipal(User);

                var task = await _taskService.GetById(taskId);

                if (await AuthoriseUpdate(task, user.Id.Value))
                {
                    return Ok(task);
                }

                return Forbid();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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
        public async Task<IActionResult> Create([FromForm] CreateTaskModel model)
        {
            try
            {
                if (TaskTypes.IsReserved(model.TypeId))
                {
                    return BadRequest("This task type cannot be created manually.");
                }

                var user = await UserService.GetUserByPrincipal(User);

                var canCreate = await AuthoriseCreate(model.AssignedToId, user.Id.Value);

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
        public async Task<IActionResult> Update([FromForm] UpdateTaskModel model)
        {
            try
            {
                var user = await UserService.GetUserByPrincipal(User);

                var canEdit = await AuthoriseUpdate(model, user.Id.Value);

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

                if (await AuthoriseUpdate(task, user.Id.Value))
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

        private async Task<bool> AuthoriseCreate(Guid personId, Guid userId)
        {
            var personInDb = await _personService.GetPersonWithTypes(personId);

            if (personInDb.PersonTypes.IsStudent)
            {
                if (User.IsType(UserTypes.Student))
                {
                    var student = await StudentService.GetByPersonId(personId);

                    return await AuthoriseStudent(student.Id.Value);
                }

                if (User.IsType(UserTypes.Staff))
                {
                    return true;
                }
            }

            if (personInDb.PersonTypes.IsStaff)
            {
                if (await UserHasPermission(PermissionValue.PeopleEditAllStaffTasks))
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

                    return await UserHasPermission(PermissionValue.PeopleEditManagedStaffTasks) &&
                           await _staffMemberService.IsLineManager(taskStaffMember.Id.Value, userStaffMember.Id.Value);
                }
            }

            return false;
        }

        private async Task<bool> AuthoriseUpdate(UpdateTaskModel model, Guid userId)
        {
            //if (TaskTypes.IsReserved(model.TypeId))
            //{
            //    return false;
            //}

            //if (await _taskService.IsTaskOwner(model.Id, userId))
            //{
            //    return true;
            //}

            //var personInDb = await _personService.GetPersonWithTypes(model.AssignedToId);

            //if (personInDb.PersonTypes.IsStudent)
            //{
            //    return User.IsType(UserTypes.Staff);
            //}

            //if (personInDb.PersonTypes.IsStaff)
            //{
            //    if (await UserHasPermission(Permissions.People.StaffTasks.EditAllStaffTasks))
            //    {
            //        return true;
            //    }

            //    var taskStaffMember = await Services.Staff.GetByPersonId(model.AssignedToId, false);

            //    var userStaffMember = await Services.Staff.GetByUserId(userId, false);

            //    if (userStaffMember != null && taskStaffMember != null &&
            //        await UserHasPermission(Permissions.People.StaffTasks.EditManagedStaffTasks) &&
            //        await Services.Staff.IsLineManager(taskStaffMember.Id, userStaffMember.Id))
            //    {
            //        return true;
            //    }
            //}

            //return false;

            return true;
        }

        private async Task<bool> AuthoriseUpdate(TaskModel model, Guid userId)
        {
            //var updateModel = new UpdateTaskModel
            //{
            //    AssignedToId = model.AssignedToId,
            //    Description = model.Description,
            //    TypeId = model.TypeId,
            //    Title = model.Title,
            //    DueDate = model.DueDate,
            //    Completed = model.Completed,
            //    Id = model.Id
            //};

            //return await AuthoriseUpdate(updateModel, userId);

            return true;
        }
    }
}