using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.People;

using MyPortal.Logic.Models.Requests.Person.Tasks;
using Task = System.Threading.Tasks.Task;
using TaskStatus = MyPortal.Database.Models.Search.TaskStatus;

namespace MyPortal.Logic.Services
{
    public class TaskService : BaseUserService, ITaskService
    {
        public TaskService(ICurrentUser user) : base(user)
        {
        }

        public async Task CreateTask(TaskRequestModel task)
        {
            Validate(task);
            
            await using var unitOfWork = await User.GetConnection();
            
            var taskToAdd = new Database.Models.Entity.Task
            {
                Title = task.Title,
                Description = task.Description,
                AssignedToId = task.AssignedToId,
                AssignedById = task.AssignedById,
                CreatedDate = DateTime.Now,
                DueDate = task.DueDate,
                TypeId = task.TypeId,
                Completed = false
            };

            unitOfWork.Tasks.Create(taskToAdd);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskTypeModel>> GetTypes(bool personalOnly, bool activeOnly = true)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var taskTypes = await unitOfWork.TaskTypes.GetAll(personalOnly, activeOnly, false);

            return taskTypes.Select(t => new TaskTypeModel(t));
        }

        public async Task<bool> IsTaskOwner(Guid taskId, Guid userId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var task = await unitOfWork.Tasks.GetById(taskId);

            if (task == null)
            {
                throw new NotFoundException("Task not found.");
            }

            if (task.AssignedById == userId)
            {
                return true;
            }

            var person = await unitOfWork.People.GetByUserId(userId);

            if (person != null && task.AssignedToId == person.Id)
            {
                return task.AllowEdit;
            }

            return false;
        }

        public async Task<TaskModel> GetById(Guid taskId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var task = await unitOfWork.Tasks.GetById(taskId);

            if (task == null)
            {
                throw new NotFoundException("Task not found.");
            }

            return new TaskModel(task);
        }

        public async Task UpdateTask(Guid taskId, TaskRequestModel task)
        {
            await using var unitOfWork = await User.GetConnection();
            
            Validate(task);
            
            ValidationHelper.ValidateModel(task);

            var taskInDb = await unitOfWork.Tasks.GetById(taskId);

            if (taskInDb == null)
            {
                throw new NotFoundException("Task not found.");
            }

            if (taskInDb.System)
            {
                throw new LogicException("Tasks of this type cannot be updated manually.");
            }

            taskInDb.Title = task.Title;
            taskInDb.Description = task.Description;
            taskInDb.DueDate = task.DueDate;
            taskInDb.TypeId = task.TypeId;

            await unitOfWork.Tasks.Update(taskInDb);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTask(Guid taskId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            await unitOfWork.Tasks.Delete(taskId);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task SetCompleted(Guid taskId, bool completed)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var taskInDb = await unitOfWork.Tasks.GetById(taskId);

            if (taskInDb.System)
            {
                throw new LogicException("Tasks of this type cannot be updated manually.");
            }

            taskInDb.Completed = completed;
            taskInDb.CompletedDate = DateTime.Now;

            await unitOfWork.Tasks.Update(taskInDb);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetByPerson(Guid personId, TaskSearchOptions searchOptions = null)
        {
            if (searchOptions == null)
            {
                searchOptions = new TaskSearchOptions { Status = TaskStatus.Active };
            }
            
            await using var unitOfWork = await User.GetConnection();

            var tasks = await unitOfWork.Tasks.GetByAssignedTo(personId, searchOptions);

            return tasks.Select(t => new TaskModel(t));
        }
    }
}
