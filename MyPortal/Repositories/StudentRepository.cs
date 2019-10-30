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
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<Student> GetByUserId(string userId)
        {
            return await Context.Students.SingleOrDefaultAsync(x => x.Person.UserId == userId);
        }

        public async Task<IEnumerable<Student>> GetOnRoll()
        {
            return await Context.Students.Where(x =>
                !x.Deleted && x.DateStarting <= DateTime.Today &&
                (x.DateLeaving == null || x.DateLeaving > DateTime.Today)).OrderBy(x => x.Person.LastName).ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetLeavers()
        {
            return await Context.Students.Where(x => !x.Deleted && x.DateLeaving <= DateTime.Today)
                .OrderBy(x => x.Person.LastName).ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetFuture()
        {
            return await Context.Students.Where(x => !x.Deleted && x.DateStarting > DateTime.Today)
                .OrderBy(x => x.Person.LastName).ToListAsync();
        }
    }
}