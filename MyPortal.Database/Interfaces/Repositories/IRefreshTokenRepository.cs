using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IRefreshTokenRepository : IReadWriteRepository<RefreshToken>
    {
        Task<IEnumerable<RefreshToken>> GetByUser(Guid userId);

        Task DeleteExpired(Guid userId);
    }
}
