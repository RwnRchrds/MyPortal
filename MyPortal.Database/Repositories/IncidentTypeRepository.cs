using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class IncidentTypeRepository : BaseReadWriteRepository<IncidentType>, IIncidentTypeRepository
    {
        public IncidentTypeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(IncidentType entity)
        {
            var incidentType = await Context.IncidentTypes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (incidentType == null)
            {
                throw new EntityNotFoundException("Incident type not found.");
            }

            incidentType.Description = entity.Description;
            incidentType.Active = entity.Active;
            incidentType.DefaultPoints = entity.DefaultPoints;
        }
    }
}