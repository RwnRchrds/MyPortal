using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class MedicalPersonConditionRepository : Repository<MedicalPersonCondition>, IMedicalPersonConditionRepository
    {
        public MedicalPersonConditionRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}