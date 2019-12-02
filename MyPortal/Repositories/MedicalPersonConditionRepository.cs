﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class MedicalPersonConditionRepository : ReadWriteRepository<MedicalPersonCondition>, IMedicalPersonConditionRepository
    {
        public MedicalPersonConditionRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<MedicalPersonCondition>> GetByPerson(int personId)
        {
            return await Context.MedicalPersonConditions.Where(x => x.PersonId == personId)
                .OrderBy(x => x.Condition.Description).ToListAsync();
        }
    }
}