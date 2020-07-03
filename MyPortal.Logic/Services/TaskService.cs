using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Search;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Requests.Person.Tasks;

namespace MyPortal.Logic.Services
{
    public class TaskService : BaseService, ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskTypeRepository _taskTypeRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IStaffMemberService _staffMemberService;

        public TaskService(ITaskRepository taskRepository, ITaskTypeRepository taskTypeRepository, IPersonRepository personRepository, IStaffMemberService staffMemberService) : base("Task")
        {
            _taskRepository = taskRepository;
            _taskTypeRepository = taskTypeRepository;
            _personRepository = personRepository;
            _staffMemberService = staffMemberService;
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

        public async Task<Lookup> GetTypes(bool personal, bool activeOnly)
        {
            var taskTypes = (await _taskTypeRepository.GetAll()).AsQueryable();

            if (activeOnly)
            {
                taskTypes = taskTypes.Where(x => x.Active);
            }

            return taskTypes.Where(t => t.Personal == personal && !t.Reserved).ToLookup();
        }

        public async Task<bool> IsTaskOwner(Guid taskId, Guid userId)
        {
            var task = await _taskRepository.GetById(taskId);

            if (task == null)
            {
                throw NotFound();
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
                throw NotFound();
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
                    throw NotFound();
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
                    throw BadRequest(
                        "To delete homework tasks, delete the homework submission.");
                }

                await _taskRepository.Delete(taskId);
            }

            await _taskRepository.SaveChanges();
        }

        public async Task<IEnumerable<TaskModel>> GetByPerson(Guid personId, TaskSearchOptions searchOptions = null)
        {
            var tasks = await _taskRepository.GetByAssignedTo(personId, searchOptions);

            return tasks.Select(BusinessMapper.Map<TaskModel>);
        }

        public override void Dispose()
        {
            _taskRepository.Dispose();
        }
    }
}
