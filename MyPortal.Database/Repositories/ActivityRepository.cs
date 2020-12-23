using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class ActivityRepository : BaseReadWriteRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(ApplicationDbContext context) : base(context, "Activity")
        {
        }
    }
}
