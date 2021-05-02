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
    public class ObservationRepository : BaseReadWriteRepository<Observation>, IObservationRepository
    {
        public ObservationRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task Update(Observation entity)
        {
            var observation = await Context.Observations.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (observation == null)
            {
                throw new EntityNotFoundException("Observation not found.");
            }

            observation.ObserverId = entity.ObserverId;
            observation.OutcomeId = entity.OutcomeId;
            observation.Notes = entity.Notes;
        }
    }
}