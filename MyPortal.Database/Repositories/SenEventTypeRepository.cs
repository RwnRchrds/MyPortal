using System.Data.Common;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class SenEventTypeRepository : BaseReadRepository<SenEventType>, ISenEventTypeRepository
    {
        public SenEventTypeRepository(DbUser dbUser) : base(dbUser)
        {
        }
    }
}