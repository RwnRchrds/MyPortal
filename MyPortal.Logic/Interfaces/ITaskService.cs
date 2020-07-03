using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Search;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Requests.Person.Tasks;

namespace MyPortal.Logic.Interfaces
{
    public interface ITaskService : IService
    {
        Task Create(params TaskModel[] tasks);

        Task Update(params UpdateTaskModel[] tasks);

        Task Delete(params Guid[] taskIds);

        Task<bool> IsTaskOwner(Guid taskId, Guid userId);

        Task<Lookup> GetTypes(bool personal, bool activeOnly);

        Task<TaskModel> GetById(Guid taskId);

        Task<IEnumerable<TaskModel>> GetByPerson(Guid personId, TaskSearchOptions searchOptions = null);
    }
}
