using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface ISystemBulletinRepository : IRepository<SystemBulletin>
    {
        Task<IEnumerable<SystemBulletin>> GetApproved();

        Task<IEnumerable<SystemBulletin>> GetStudent();

        Task<IEnumerable<SystemBulletin>> GetOwn(int authorId);
    }
}
