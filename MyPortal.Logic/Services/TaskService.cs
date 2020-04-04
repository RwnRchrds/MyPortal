using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Services
{
    public class TaskService : BaseService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
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

                taskInDb.Title = task.Title;
                taskInDb.Description = task.Description;
                taskInDb.AssignedToId = task.AssignedToId;
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

        public async Task<IEnumerable<TaskModel>> GetByPerson(Guid personId)
        {
            var tasks = await _taskRepository.GetByPerson(personId);

            return tasks.Select(_businessMapper.Map<TaskModel>);
        }
    }
}
