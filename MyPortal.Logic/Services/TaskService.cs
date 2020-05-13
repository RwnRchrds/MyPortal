using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Services
{
    public class TaskService : BaseService, ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository) : base("Task")
        {
            _taskRepository = taskRepository;
        }
        
        public Lookup GetSearchFilters()
        {
            var searchTypes = new Dictionary<string, Guid>();
            
            searchTypes.Add("All", Guid.Empty);
            searchTypes.Add("Active", SearchFilters.Tasks.Active);
            searchTypes.Add("Overdue", SearchFilters.Tasks.Overdue);
            searchTypes.Add("Completed", SearchFilters.Tasks.Completed);

            return new Lookup(searchTypes);
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
                    Personal = task.Personal,
                    Completed = false
                };

                _taskRepository.Create(taskToAdd);
            }

            await _taskRepository.SaveChanges();
        }

        public async Task Update(params TaskModel[] tasks)
        {
            foreach (var task in tasks)
            {
                var taskInDb = await _taskRepository.GetByIdWithTracking(task.Id);

                if (taskInDb == null)
                {
                    NotFound();
                }

                taskInDb.Title = task.Title;
                taskInDb.Description = task.Description;
                taskInDb.DueDate = task.DueDate;
                taskInDb.Completed = task.Completed;
            }

            await _taskRepository.SaveChanges();
        }

        public async Task Delete(params Guid[] taskIds)
        {
            foreach (var taskId in taskIds)
            {
                await _taskRepository.Delete(taskId);
            }

            await _taskRepository.SaveChanges();
        }

        public async Task<IEnumerable<TaskModel>> GetByPerson(Guid personId, Guid filter)
        {
            IEnumerable<Database.Models.Task> tasks;

            if (filter == SearchFilters.Tasks.Active)
            {
                tasks = await _taskRepository.GetActiveByAssignedTo(personId);
            }
            else if (filter == SearchFilters.Tasks.Completed)
            {
                tasks = await _taskRepository.GetCompletedByAssignedTo(personId);
            }
            else if (filter == SearchFilters.Tasks.Overdue)
            {
                tasks = await _taskRepository.GetOverdueByAssignedTo(personId);
            }
            else
            {
                tasks = await _taskRepository.GetByAssignedTo(personId);
            }

            return tasks.Select(_businessMapper.Map<TaskModel>);
        }

        public override void Dispose()
        {
            _taskRepository.Dispose();
        }
    }
}
