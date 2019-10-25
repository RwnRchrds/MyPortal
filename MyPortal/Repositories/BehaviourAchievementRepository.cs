using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class BehaviourAchievementRepository : Repository<BehaviourAchievement>, IBehaviourAchievementRepository
    {
        public BehaviourAchievementRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}