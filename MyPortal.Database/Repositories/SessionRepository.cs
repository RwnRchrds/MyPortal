using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SessionRepository : BaseReadWriteRepository<Session>, ISessionRepository
    {
        public SessionRepository(DbUserWithContext dbUser) : base(dbUser)
        {
            
        }

        public async Task Update(Session entity)
        {
            var session = await DbUser.Context.Sessions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (session == null)
            {
                throw new EntityNotFoundException("Session not found.");
            }

            session.StartDate = entity.StartDate;
            session.EndDate = entity.EndDate;
            session.ClassId = entity.ClassId;
            session.RoomId = entity.RoomId;
            session.TeacherId = entity.TeacherId;
        }
    }
}