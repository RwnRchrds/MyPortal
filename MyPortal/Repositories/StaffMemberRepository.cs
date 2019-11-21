using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Extensions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class StaffMemberRepository : Repository<StaffMember>, IStaffMemberRepository
    {
        public StaffMemberRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<StaffMember> GetByIdWithRelated(int staffId, params Expression<Func<StaffMember, object>>[] includeProperties)
        {
            var query = Context.StaffMembers.AsQueryable();

            query = query.IncludeMultiple(includeProperties);

            return await query.SingleOrDefaultAsync(x => x.Id == staffId);
        }

        public new async Task<IEnumerable<StaffMember>> GetAll()
        {
            return await Context.StaffMembers.Include(x => x.Person).OrderBy(x => x.Person.LastName).ToListAsync();
        }

        public async Task<StaffMember> GetByUserId(string userId)
        {
            return await Context.StaffMembers.Include(x => x.Person).SingleOrDefaultAsync(x => x.Person.UserId == userId);
        }
    }
}