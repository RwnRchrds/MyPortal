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
    public class ProfileCommentRepository : Repository<ProfileComment>, IProfileCommentRepository
    {
        public ProfileCommentRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ProfileComment>> GetCommentsByCommentBank(int commentBankId)
        {
            return await Context.ProfileComments.Where(x => x.CommentBankId == commentBankId).OrderBy(x => x.Value).ToListAsync();
        }
    }
}