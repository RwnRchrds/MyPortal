using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class DiaryEventTypeRepository : BaseReadWriteRepository<DiaryEventType>, IDiaryEventTypeRepository
    {
        public DiaryEventTypeRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task<IEnumerable<DiaryEventType>> GetAll(bool includeReserved)
        {
            var query = GetDefaultQuery();

            if (!includeReserved)
            {
                query.Where($"{TblAlias}.Reserved", false);
            }

            return await ExecuteQuery(query);
        }

        public async Task Update(DiaryEventType entity)
        {
            var eventType = await DbUser.Context.DiaryEventTypes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (eventType == null)
            {
                throw new EntityNotFoundException("Event type not found.");
            }

            eventType.Description = entity.Description;
            eventType.Active = entity.Active;
            eventType.ColourCode = entity.ColourCode;
        }
    }
}