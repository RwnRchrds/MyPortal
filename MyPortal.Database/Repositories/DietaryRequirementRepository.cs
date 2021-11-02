using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class DietaryRequirementRepository : BaseReadWriteRepository<DietaryRequirement>, IDietaryRequirementRepository
    {
        public DietaryRequirementRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(DietaryRequirement entity)
        {
            var dietaryRequirement = await Context.DietaryRequirements.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (dietaryRequirement == null)
            {
                throw new EntityNotFoundException("Dietary requirement not found.");
            }
            
            dietaryRequirement.Description = entity.Description;
            dietaryRequirement.Active = entity.Active;
        }
    }
}