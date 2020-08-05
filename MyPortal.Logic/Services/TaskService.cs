using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Database.Search;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Person.Tasks;
using Task = System.Threading.Tasks.Task;
using TaskStatus = MyPortal.Database.Search.TaskStatus;

namespace MyPortal.Logic.Services
{
    public class TaskService : BaseService, ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskTypeRepository _taskTypeRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IStaffMemberRepository _staffMemberRepository;

        public TaskService(ApplicationDbContext context)
        {
            _taskRepository = new TaskRepository(context);
            _taskTypeRepository = new TaskTypeRepository(context);
            _personRepository = new PersonRepository(context);
            _staffMemberRepository = new StaffMemberRepository(context);
        }

        public async Task Create(params TaskModel[] tasks)
        {
            foreach (var task in tasks)
            {
                var taskToAdd = new Database.Models.Task
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

                _taskRepository.Create(taskToAdd);
            }

            await _taskRepository.SaveChanges();
        }

        public async Task<Lookup> GetTypes(bool personalOnly, bool activeOnly = true)
        {
            var taskTypes = (await _taskTypeRepository.GetAll()).AsQueryable();

            if (activeOnly)
            {
                taskTypes = taskTypes.Where(x => x.Active);
            }

            if (personalOnly)
            {
                taskTypes = taskTypes.Where(x => x.Personal);
            }

            return taskTypes.Where(t => !t.Reserved).ToLookup();
        }

        public async Task<bool> IsTaskOwner(Guid taskId, Guid userId)
        {
            var task = await _taskRepository.GetById(taskId);

            if (task == null)
            {
                throw new NotFoundException("Task not found.");
            }

            var person = await _personRepository.GetByUserId(userId);

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
            var task = await _taskRepository.GetById(taskId);

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
                var taskInDb = await _taskRepository.GetByIdWithTracking(task.Id);

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

            await _taskRepository.SaveChanges();
        }

        public async Task Delete(params Guid[] taskIds)
        {
            foreach (var taskId in taskIds)
            {
                var taskInDb = await _taskRepository.GetById(taskId);

                if (taskInDb.Type.Id == TaskTypes.Homework)
                {
                    throw new InvalidDataException("Please use the homework module to manage homework tasks.");
                }

                await _taskRepository.Delete(taskId);
            }

            await _taskRepository.SaveChanges();
        }

        public async Task<IEnumerable<TaskModel>> GetByPerson(Guid personId, TaskSearchOptions searchOptions = null)
        {
            if (searchOptions == null)
            {
                searchOptions = new TaskSearchOptions {Status = TaskStatus.Active};
            }

            var tasks = await _taskRepository.GetByAssignedTo(personId, searchOptions);

            return tasks.Select(BusinessMapper.Map<TaskModel>);
        }

        public override void Dispose()
        {
            _taskRepository.Dispose();
            _taskTypeRepository.Dispose();
            _personRepository.Dispose();
            _staffMemberRepository.Dispose();
        }
    }
}
