using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class PersonConditionRepository : BaseReadWriteRepository<PersonCondition>, IPersonConditionRepository
    {
        public PersonConditionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
           
        }

        public async Task Update(PersonCondition entity)
        {
            var personCondition = await Context.PersonConditions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (personCondition == null)
            {
                throw new EntityNotFoundException("Person condition not found.");
            }

            personCondition.MedicationTaken = entity.MedicationTaken;
            personCondition.Medication = entity.Medication;
        }
    }
}