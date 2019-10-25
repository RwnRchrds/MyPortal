using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class BehaviourIncidentRepository : Repository<BehaviourIncident>, IBehaviourIncidentRepository
    {
        public BehaviourIncidentRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}