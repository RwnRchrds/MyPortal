using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class CommentBankRepository : ReadWriteRepository<CommentBank>, ICommentBankRepository
    {
        public CommentBankRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}