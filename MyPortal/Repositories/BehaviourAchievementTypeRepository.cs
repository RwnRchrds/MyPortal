using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class BehaviourAchievementTypeRepository : ReadOnlyRepository<BehaviourAchievementType>, IBehaviourAchievementTypeRepository
    {
        public BehaviourAchievementTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}