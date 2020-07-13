using System.Data;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class AchievementOutcomeRepository : BaseReadWriteRepository<AchievementOutcome>, IAchievementOutcomeRepository
    {
        public AchievementOutcomeRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            
        }
    }
}