using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class TaskTypeRepository : BaseReadWriteRepository<TaskType>, ITaskTypeRepository
    {
        public TaskTypeRepository(ApplicationDbContext context) : base(context, "TaskType")
        {

        }

        public async Task<IEnumerable<TaskType>> GetAll(bool personal, bool active, bool includeReserved)
        {
            var query = GenerateQuery();

            query.Where("TaskType.Personal", personal);
            query.Where("Personal.Active", active);
            query.Where("Personal.Reserved", includeReserved);

            return await ExecuteQuery(query);
        }
    }
}
