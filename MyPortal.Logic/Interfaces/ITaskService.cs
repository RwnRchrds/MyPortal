using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Interfaces
{
    public interface ITaskService : IService
    {
        Task Create(params TaskModel[] tasks);
        Task Update(params TaskModel[] tasks);

        Task Delete(params Guid[] taskIds);

        Task<IEnumerable<TaskModel>> GetByPerson(Guid personId, Guid filter);
    }
}
