using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class TaskTypeRepository : BaseReadWriteRepository<TaskType>, ITaskTypeRepository
    {
        public TaskTypeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        public async Task<IEnumerable<TaskType>> GetAll(bool personalOnly, bool activeOnly, bool includeSystem)
        {
            var query = GenerateQuery();

            if (personalOnly)
            {
                query.Where($"{TblAlias}.Personal", true);   
            }

            if (activeOnly)
            {
                query.Where($"{TblAlias}.Active", true);
            }
            
            query.Where($"{TblAlias}.System", includeSystem);

            return await ExecuteQuery(query);
        }

        public async Task Update(TaskType entity)
        {
            var taskType = await Context.TaskTypes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (taskType == null)
            {
                throw new EntityNotFoundException("Task type not found.");
            }

            if (taskType.System)
            {
                throw ExceptionHelper.UpdateSystemEntityException;
            }

            taskType.Description = entity.Description;
            taskType.ColourCode = entity.ColourCode;
            taskType.Personal = entity.Personal;
            taskType.Active = entity.Active;
        }
    }
}
