using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class MedicalConditionRepository : ReadRepository<MedicalCondition>, IMedicalConditionRepository
    {
        public MedicalConditionRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}