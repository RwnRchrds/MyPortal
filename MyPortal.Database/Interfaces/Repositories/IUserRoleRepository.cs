using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetByUser(Guid userId);
        Task DeleteAllByRole(Guid roleId);
        Task DeleteAllByUser(Guid userId);
    }
}
