using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Models;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Requests.Person.Tasks;

namespace MyPortalCore.Controllers.Api
{
    [Authorize]
    public class TaskController : BaseApiController
    {
        private readonly ITaskService _taskService;
        private readonly IPersonService _personService;
        
        public TaskController(IApplicationUserService userService, ITaskService taskService, IPersonService personService) : base(userService)
        {
            _taskService = taskService;
            _personService = personService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromForm] CreateTaskModel model)
        {
            return await Process(async () =>
            {
                var user = await _userService.GetUserByPrincipal(User);

                var currentPerson = await _personService.GetByUserId(user.Id);

                var task = new TaskModel
                {
                    AssignedToId = model.AssignedToId,
                    AssignedById = currentPerson.Id,
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    CreatedDate = DateTime.Now,
                    Personal = model.AssignedToId == currentPerson.Id
                };

                await _taskService.Create(task);

                return Ok("Task created.");
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromForm] UpdateTaskModel model)
        {
            var user = await _userService.GetUserByPrincipal(User);

            var task = new TaskModel
            {
                Id = model.Id,
                DueDate = model.DueDate,
                Title = model.Title,
                Description = model.Description,
                Completed = model.Completed
            };

            await _taskService.Update(task);

            return Ok("Task updated.");
        }
    }
}