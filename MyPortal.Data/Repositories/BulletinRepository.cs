using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;
namespace MyPortal.Data.Repositories

{
    public class BulletinRepository : ReadWriteRepository<Bulletin>, IBulletinRepository
    {
        public BulletinRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Bulletin>> GetApproved()
        {
            return await Context.Bulletins.Where(x => x.Approved).ToListAsync();
        }

        public async Task<IEnumerable<Bulletin>> GetStudent()
        {
            return await Context.Bulletins.Where(x => x.Approved && x.ShowStudents).ToListAsync();
        }

        public async Task<IEnumerable<Bulletin>> GetOwn(int authorId)
        {
            return await Context.Bulletins.Where(x => x.AuthorId == authorId).ToListAsync();
        }
    }
}