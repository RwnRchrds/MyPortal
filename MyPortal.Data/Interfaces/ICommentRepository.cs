using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface ICommentRepository : IReadWriteRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetByCommentBank(int commentBankId);
    }
}
