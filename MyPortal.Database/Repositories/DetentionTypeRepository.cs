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
    public class DetentionTypeRepository : BaseReadWriteRepository<DetentionType>, IDetentionTypeRepository
    {
        public DetentionTypeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        public async Task Update(DetentionType entity)
        {
            var detentionType = await Context.DetentionTypes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (detentionType == null)
            {
                throw new EntityNotFoundException("Detention type not found.");
            }

            detentionType.StartTime = entity.StartTime;
            detentionType.EndTime = entity.EndTime;
            detentionType.Description = entity.Description;
            detentionType.Active = entity.Active;
        }
    }
}