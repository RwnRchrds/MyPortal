using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class StaffMemberRepository : Repository<StaffMember>, IStaffMemberRepository
    {
        public StaffMemberRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<StaffMember> GetByUserIdAsync(string userId)
        {
            return await Context.StaffMembers.SingleOrDefaultAsync(x => x.Person.UserId == userId);
        }
    }
}