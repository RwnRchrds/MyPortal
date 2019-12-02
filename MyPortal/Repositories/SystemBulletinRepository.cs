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
    public class SystemBulletinRepository : ReadWriteRepository<SystemBulletin>, ISystemBulletinRepository
    {
        public SystemBulletinRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<SystemBulletin>> GetApproved()
        {
            return await Context.SystemBulletins.Where(x => x.Approved).ToListAsync();
        }

        public async Task<IEnumerable<SystemBulletin>> GetStudent()
        {
            return await Context.SystemBulletins.Where(x => x.Approved && x.ShowStudents).ToListAsync();
        }

        public async Task<IEnumerable<SystemBulletin>> GetOwn(int authorId)
        {
            return await Context.SystemBulletins.Where(x => x.AuthorId == authorId).ToListAsync();
        }
    }
}