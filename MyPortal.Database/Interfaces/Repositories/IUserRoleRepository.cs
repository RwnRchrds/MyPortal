using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IUserRoleRepository : IDisposable
    {
        Task<IEnumerable<UserRole>> GetByUser(Guid userId);
    }
}
