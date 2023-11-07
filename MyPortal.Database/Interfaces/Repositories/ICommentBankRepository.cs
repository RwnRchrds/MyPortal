using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ICommentBankRepository : IReadWriteRepository<CommentBank>, IUpdateRepository<CommentBank>
    {
    }
}