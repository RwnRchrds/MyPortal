using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class TaskTypeRepository : BaseReadWriteRepository<TaskType>, ITaskTypeRepository
    {
        public TaskTypeRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
