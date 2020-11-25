using System.Data;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Repositories
{
    public class CurriculumGroupMembershipRepository : BaseReadWriteRepository<CurriculumGroupMembership>, ICurriculumGroupMembershipRepository
    {
        public CurriculumGroupMembershipRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}