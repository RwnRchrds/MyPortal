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
    public class DiaryEventTypeRepository : BaseReadWriteRepository<DiaryEventType>, IDiaryEventTypeRepository
    {
        public DiaryEventTypeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task<IEnumerable<DiaryEventType>> GetAll(bool includeReserved)
        {
            var query = GenerateQuery();

            if (!includeReserved)
            {
                query.Where($"{TblAlias}.Reserved", false);
            }

            return await ExecuteQuery(query);
        }

        public async Task Update(DiaryEventType entity)
        {
            var eventType = await Context.DiaryEventTypes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (eventType == null)
            {
                throw new EntityNotFoundException("Event type not found.");
            }

            if (eventType.System)
            {
                throw new SystemEntityException("System entities cannot be modified.");
            }

            eventType.Description = entity.Description;
            eventType.Active = entity.Active;
            eventType.ColourCode = entity.ColourCode;
        }
    }
}