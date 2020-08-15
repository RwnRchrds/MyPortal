using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Search;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Person.Tasks;

namespace MyPortal.Logic.Interfaces
{
    public interface ITaskService : IService
    {
        Task Create(params TaskModel[] tasks);

        Task Update(params UpdateTaskModel[] tasks);

        Task Delete(params Guid[] taskIds);

        Task<bool> IsTaskOwner(Guid taskId, Guid userId);

        Task<Lookup> GetTypes(bool personalOnly, bool activeOnly = true);

        Task<TaskModel> GetById(Guid taskId);

        Task<IEnumerable<TaskModel>> GetByPerson(Guid personId, TaskSearchOptions searchOptions = null);
    }
}
