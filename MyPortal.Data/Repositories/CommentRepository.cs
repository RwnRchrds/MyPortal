using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class CommentRepository : ReadWriteRepository<Comment>, ICommentRepository
    {
        public CommentRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Comment>> GetByCommentBank(int commentBankId)
        {
            return await Context.Comments.Where(x => x.CommentBankId == commentBankId).OrderBy(x => x.Value).ToListAsync();
        }
    }
}