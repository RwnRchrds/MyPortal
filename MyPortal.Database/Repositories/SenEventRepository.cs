using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SenEventRepository : BaseReadWriteRepository<SenEvent>, ISenEventRepository
    {
        public SenEventRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(SenEvent entity)
        {
            var senEvent = await DbUser.Context.SenEvents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (senEvent == null)
            {
                throw new EntityNotFoundException("SEN event not found.");
            }

            senEvent.Date = entity.Date;
            senEvent.Note = entity.Note;
            senEvent.EventTypeId = entity.EventTypeId;
        }
    }
}