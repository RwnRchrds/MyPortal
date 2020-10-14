using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Person.Tasks;

namespace MyPortalWeb.Controllers.Api
{
    public class TaskController : StudentApiController
    {
        private readonly ITaskService _taskService;
        private readonly IPersonService _personService;
        private readonly IStaffMemberService _staffMemberService;

        public TaskController(IUserService userService, IAcademicYearService academicYearService,
            IStudentService studentService, ITaskService taskService, IPersonService personService,
            IStaffMemberService staffMemberService) : base(userService, academicYearService, studentService)
        {
            _taskService = taskService;
            _personService = personService;
            _staffMemberService = staffMemberService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetById([FromQuery] Guid taskId)
        {
            return await ProcessAsync(async () =>
            {
                var user = await UserService.GetUserByPrincipal(User);

                var task = await _taskService.GetById(taskId);

                if (await AuthoriseUpdate(task, user.Id))
                {
                    return Ok(task);
                }

                return Forbid();
            });
        }

        [HttpGet]
        [Route("GetTypes")]
        public async Task<IActionResult> GetTaskTypes([FromQuery] bool personal = false)
        {
            return await ProcessAsync(async () =>
            {
                var taskTypes = await _taskService.GetTypes(personal);

                return Ok(taskTypes);
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTaskModel model)
        {
            return await ProcessAsync(async () =>
            {
                if (TaskTypes.IsReserved(model.TypeId))
                {
                    return BadRequest("This task type cannot be created manually.");
                }

                var user = await UserService.GetUserByPrincipal(User);

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

                    await _taskService.Create(task);

                    return Ok("Task created successfully.");
                }

                return Forbid();
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateTaskModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await UserService.GetUserByPrincipal(User);

                var canEdit = await AuthoriseUpdate(model, user.Id);

                if (canEdit)
                {
                    await _taskService.Update(model);

                    return Ok("Task updated successfully.");
                }

                return Forbid();
            });
        }

        [HttpPut]
        [Route("Toggle")]
        public async Task<IActionResult> ToggleCompleted([FromQuery] Guid taskId)
        {
            return await ProcessAsync(async () =>
            {
                var user = await UserService.GetUserByPrincipal(User);
                var task = await _taskService.GetById(taskId);

                if (await AuthoriseUpdate(task, user.Id))
                {
                    var model = new UpdateTaskModel
                    {
                        Id = task.Id,
                        Description = task.Description,
                        TypeId = task.TypeId,
                        Title = task.Title,
                        Completed = !task.Completed,
                        AssignedToId = task.AssignedToId,
                        DueDate = task.DueDate
                    };

                    await _taskService.Update(model);

                    return Ok("Task toggled successfully.");
                }

                return Forbid();
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid taskId)
        {
            return await ProcessAsync(async () =>
            {
                var user = await UserService.GetUserByPrincipal(User);

                await _taskService.Delete(taskId);

                return Ok("Task deleted successfully.");
            });
        }

        private async Task<bool> AuthoriseCreate(Guid personId, Guid userId)
        {
            var taskPersonTypes = await _personService.GetPersonTypes(personId);

            if (taskPersonTypes.Student)
            {
                if (User.IsType(UserTypes.Student))
                {
                    var student = await StudentService.GetByPersonId(personId);

                    return await AuthenticateStudent(student.Id);
                }

                if (User.IsType(UserTypes.Staff))
                {
                    return true;
                }
            }

            else if (taskPersonTypes.Employee)
            {
                if (User.HasPermission(Permissions.EditStaffPerformancePermissions))
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

                    return await _staffMemberService.IsLineManager(taskStaffMember.Id, userStaffMember.Id);
                }
            }

            return false;
        }

        private async Task<bool> AuthoriseUpdate(UpdateTaskModel model, Guid userId)
        {
            if (TaskTypes.IsReserved(model.TypeId))
            {
                return false;
            }
            
            if (await _taskService.IsTaskOwner(model.Id, userId))
            {
                return true;
            }

            var taskPersonTypes = await _personService.GetPersonTypes(model.AssignedToId);

            if (taskPersonTypes.Student)
            {
                return User.IsType(UserTypes.Staff);
            }

            if (taskPersonTypes.Employee)
            {
                if (User.HasPermission(Permissions.EditStaffPerformancePermissions))
                {
                    return true;
                }

                var taskStaffMember = await _staffMemberService.GetByPersonId(model.AssignedToId, false);

                var userStaffMember = await _staffMemberService.GetByUserId(userId, false);

                if (userStaffMember != null && taskStaffMember != null && await _staffMemberService.IsLineManager(taskStaffMember.Id, userStaffMember.Id))
                {
                    return true;
                }
            }

            return false;
        }

        private async Task<bool> AuthoriseUpdate(TaskModel model, Guid userId)
        {
            var updateModel = new UpdateTaskModel
            {
                AssignedToId = model.AssignedToId,
                Description = model.Description,
                TypeId = model.TypeId,
                Title = model.Title,
                DueDate = model.DueDate,
                Completed = model.Completed,
                Id = model.Id
            };

            return await AuthoriseUpdate(updateModel, userId);
        }

        public override void Dispose()
        {
            _taskService.Dispose();
        }
    }
}