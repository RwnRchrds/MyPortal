using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Data.People;

using MyPortal.Logic.Models.Requests.Person.Tasks;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ITaskService : IService
    {
        Task CreateTask(TaskRequestModel task);

        Task UpdateTask(Guid taskId, TaskRequestModel task);

        Task DeleteTask(Guid taskId);

        Task<bool> IsTaskOwner(Guid taskId, Guid userId);

        Task<IEnumerable<TaskTypeModel>> GetTypes(bool personalOnly, bool activeOnly = true);

        Task<TaskModel> GetTaskById(Guid taskId);

        Task<TaskReminderModel> GetExistingReminder(Guid taskId, Guid userId);

        Task CreateTaskReminder(TaskReminderRequestModel model);

        Task UpdateTaskReminder(Guid reminderId, TaskReminderRequestModel model);

        Task DeleteTaskReminder(Guid reminderId);

        Task<IEnumerable<TaskModel>> GetByPerson(Guid personId, TaskSearchOptions searchOptions = null);
        Task SetCompleted(Guid taskId, bool completed);
    }
}
