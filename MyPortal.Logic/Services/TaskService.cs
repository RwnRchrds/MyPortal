using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Person.Tasks;
using Task = System.Threading.Tasks.Task;
using TaskStatus = MyPortal.Database.Models.Search.TaskStatus;

namespace MyPortal.Logic.Services
{
    public class TaskService : BaseService, ITaskService
    {
        public TaskService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task Create(params TaskModel[] tasks)
        {
            foreach (var task in tasks)
            {
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

                UnitOfWork.Tasks.Create(taskToAdd);
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<TaskTypeModel>> GetTypes(bool personalOnly, bool activeOnly = true)
        {
            var taskTypes = await UnitOfWork.TaskTypes.GetAll(personalOnly, activeOnly, false);

            return taskTypes.Select(BusinessMapper.Map<TaskTypeModel>);
        }

        public async Task<bool> IsTaskOwner(Guid taskId, Guid userId)
        {
            var task = await UnitOfWork.Tasks.GetById(taskId);

            if (task == null)
            {
                throw new NotFoundException("Task not found.");
            }

            var person = await UnitOfWork.People.GetByUserId(userId);

            if (person != null && task.AssignedToId == person.Id)
            {
                return true;
            }

            if (task.AssignedById == userId)
            {
                return true;
            }

            return false;
        }

        public async Task<TaskModel> GetById(Guid taskId)
        {
            var task = await UnitOfWork.Tasks.GetById(taskId);

            if (task == null)
            {
                throw new NotFoundException("Task not found.");
            }

            return BusinessMapper.Map<TaskModel>(task);
        }

        public async Task Update(params UpdateTaskModel[] tasks)
        {
            foreach (var task in tasks)
            {
                var taskInDb = await UnitOfWork.Tasks.GetByIdForEditing(task.Id);

                if (taskInDb == null)
                {
                    throw new NotFoundException("Task not found.");
                }

                if (taskInDb.TypeId == TaskTypes.Homework)
                {
                    throw new InvalidDataException("Please use the homework module to manage homework tasks.");
                }

                taskInDb.Title = task.Title;
                taskInDb.Description = task.Description;
                taskInDb.DueDate = task.DueDate;
                taskInDb.TypeId = task.TypeId;

                if (task.Completed != null)
                {
                    taskInDb.Completed = task.Completed.Value;

                    if (task.Completed.Value)
                    {
                        taskInDb.CompletedDate = DateTime.Now;
                    }
                }
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task Delete(params Guid[] taskIds)
        {
            foreach (var taskId in taskIds)
            {
                var taskInDb = await UnitOfWork.Tasks.GetById(taskId);

                if (taskInDb.Type.Id == TaskTypes.Homework)
                {
                    throw new InvalidDataException("Please use the homework module to manage homework tasks.");
                }

                await UnitOfWork.Tasks.Delete(taskId);
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<TaskModel>> GetByPerson(Guid personId, TaskSearchOptions searchOptions = null)
        {
            if (searchOptions == null)
            {
                searchOptions = new TaskSearchOptions {Status = TaskStatus.Active};
            }

            var tasks = await UnitOfWork.Tasks.GetByAssignedTo(personId, searchOptions);

            return tasks.Select(BusinessMapper.Map<TaskModel>);
        }
    }
}
