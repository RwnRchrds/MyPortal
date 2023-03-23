using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
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
        public TaskService(ISessionUser user) : base(user)
        {
        }

        public async Task CreateTask(TaskRequestModel task)
        {
            Validate(task);
            
            await using var unitOfWork = await User.GetConnection();
            
            var taskToAdd = new Database.Models.Entity.Task
            {
                Id = Guid.NewGuid(),
                Title = task.Title,
                Description = task.Description,
                AssignedToId = task.AssignedToId,
                CreatedById = task.AssignedById,
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

            if (task.CreatedById == userId)
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

        public async Task<TaskModel> GetTaskById(Guid taskId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var task = await unitOfWork.Tasks.GetById(taskId);

            if (task == null)
            {
                throw new NotFoundException("Task not found.");
            }

            return new TaskModel(task);
        }

        public async Task<TaskReminderModel> GetExistingReminder(Guid taskId, Guid userId)
        {
            await using var unitOfWork = await User.GetConnection();

            var reminders = await unitOfWork.TaskReminders.GetRemindersByUser(userId);

            var reminder = reminders.FirstOrDefault(r => r.TaskId == taskId);

            if (reminder != null)
            {
                return new TaskReminderModel(reminder);
            }

            return null;
        }

        public async Task CreateTaskReminder(TaskReminderRequestModel model)
        {
            await using var unitOfWork = await User.GetConnection();

            var reminders = await unitOfWork.TaskReminders.GetRemindersByUser(model.UserId);

            var existingReminder = reminders.FirstOrDefault(r => r.TaskId == model.TaskId);

            if (existingReminder != null)
            {
                throw new LogicException("A reminder already exists for this task.");
            }

            var newReminder = new TaskReminder
            {
                Id = Guid.NewGuid(),
                TaskId = model.TaskId,
                UserId = model.UserId,
                RemindTime = model.RemindTime
            };
            
            unitOfWork.TaskReminders.Create(newReminder);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateTaskReminder(Guid reminderId, TaskReminderRequestModel model)
        {
            await using var unitOfWork = await User.GetConnection();

            var existingReminder = await unitOfWork.TaskReminders.GetById(reminderId);

            if (existingReminder == null)
            {
                throw new NotFoundException("Task reminder not found.");
            }

            existingReminder.RemindTime = model.RemindTime;

            await unitOfWork.TaskReminders.Update(existingReminder);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTaskReminder(Guid reminderId)
        {
            await using var unitOfWork = await User.GetConnection();

            await unitOfWork.TaskReminders.Delete(reminderId);

            await unitOfWork.SaveChangesAsync();
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
