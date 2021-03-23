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
    public class TasksController : StudentApiController
    {
        public TasksController(IAppServiceCollection services) : base(services)
        {
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(typeof(TaskModel), 200)]
        public async Task<IActionResult> GetById([FromQuery] Guid taskId)
        {
            return await ProcessAsync(async () =>
            {
                var user = await Services.Users.GetUserByPrincipal(User);

                var task = await Services.Tasks.GetById(taskId);

                if (await AuthoriseUpdate(task, user.Id))
                {
                    return Ok(task);
                }

                return Forbid();
            });
        }

        [HttpGet]
        [Route("types")]
        [ProducesResponseType(typeof(IEnumerable<TaskTypeModel>), 200)]
        public async Task<IActionResult> GetTaskTypes([FromQuery] bool personal = false)
        {
            return await ProcessAsync(async () =>
            {
                var taskTypes = await Services.Tasks.GetTypes(personal);

                return Ok(taskTypes);
            });
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromForm] CreateTaskModel model)
        {
            return await ProcessAsync(async () =>
            {
                if (TaskTypes.IsReserved(model.TypeId))
                {
                    return BadRequest("This task type cannot be created manually.");
                }

                var user = await Services.Users.GetUserByPrincipal(User);

                var canCreate = await AuthoriseCreate(model.AssignedToId, user.Id);

                if (canCreate)
                {
                    var task = new TaskModel
                    {
                        AssignedToId = model.AssignedToId,
                        AssignedById = user.Id,
                        Title = model.Title,
                        Description = model.Description,
                        DueDate = model.DueDate,
                        CreatedDate = DateTime.Now,
                        TypeId = model.TypeId,
                        Completed = false
                    };

                    await Services.Tasks.Create(task);

                    return Ok();
                }

                return Forbid();
            });
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromForm] UpdateTaskModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await Services.Users.GetUserByPrincipal(User);

                var canEdit = await AuthoriseUpdate(model, user.Id);

                if (canEdit)
                {
                    await Services.Tasks.Update(model);

                    return Ok();
                }

                return Forbid();
            });
        }

        [HttpPut]
        [Route("toggle")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ToggleCompleted([FromBody] TaskToggleRequestModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await Services.Users.GetUserByPrincipal(User);
                var task = await Services.Tasks.GetById(model.TaskId);

                if (await AuthoriseUpdate(task, user.Id))
                {
                    await Services.Tasks.SetCompleted(model.TaskId, model.Completed);

                    return Ok();
                }

                return Forbid();
            });
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromQuery] Guid taskId)
        {
            return await ProcessAsync(async () =>
            {
                var user = await Services.Users.GetUserByPrincipal(User);

                await Services.Tasks.Delete(taskId);

                return Ok();
            });
        }

        private async Task<bool> AuthoriseCreate(Guid personId, Guid userId)
        {
            var personInDb = await Services.People.GetPersonWithTypes(personId);

            if (personInDb.PersonTypes.IsStudent)
            {
                if (User.IsType(UserTypes.Student))
                {
                    var student = await Services.Students.GetByPersonId(personId);

                    return await AuthoriseStudent(student.Id);
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

                var taskStaffMember = await Services.Staff.GetByPersonId(personId, false);
                var userStaffMember = await Services.Staff.GetByUserId(userId, false);

                if (userStaffMember != null && taskStaffMember != null)
                {
                    if (taskStaffMember.Id == userStaffMember.Id)
                    {
                        return true;
                    }

                    return await UserHasPermission(PermissionValue.PeopleEditManagedStaffTasks) &&
                           await Services.Staff.IsLineManager(taskStaffMember.Id, userStaffMember.Id);
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

            //if (await Services.Tasks.IsTaskOwner(model.Id, userId))
            //{
            //    return true;
            //}

            //var personInDb = await Services.People.GetPersonWithTypes(model.AssignedToId);

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