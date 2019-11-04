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
        Task<IEnumerable<SystemBulletin>> GetApprovedBulletins();

        Task<IEnumerable<SystemBulletin>> GetApprovedStudentBulletins();

        Task<IEnumerable<SystemBulletin>> GetOwnBulletins(int authorId);
    }
}
