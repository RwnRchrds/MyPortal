using System.Data;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class CurriculumGroupMembershipRepository : BaseReadWriteRepository<CurriculumGroupMembership>
    {
        public CurriculumGroupMembershipRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            
        }
    }
}