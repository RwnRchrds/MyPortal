using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class TaskTypeRepository : BaseReadWriteRepository<TaskType>, ITaskTypeRepository
    {
        public TaskTypeRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {

        }
    }
}
