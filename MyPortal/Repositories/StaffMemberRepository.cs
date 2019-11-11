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

        public new async Task<IEnumerable<StaffMember>> GetAllAsync()
        {
            return await Context.StaffMembers.Include(x => x.Person).OrderBy(x => x.Person.LastName).ToListAsync();
        }

        public async Task<StaffMember> GetByUserId(string userId)
        {
            return await Context.StaffMembers.Include(x => x.Person).SingleOrDefaultAsync(x => x.Person.UserId == userId);
        }
    }
}