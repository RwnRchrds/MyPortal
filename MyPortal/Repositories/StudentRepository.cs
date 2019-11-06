using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Ajax.Utilities;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<Student> GetByUserIdAsync(string userId)
        {
            return await Context.Students.SingleOrDefaultAsync(x => x.Person.UserId == userId);
        }

        public new async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await Context.Students.Include(x => x.Person).OrderBy(x => x.Person.LastName).ToListAsync();
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

        public async Task<IEnumerable<Student>> GetStudentsByRegGroup(int regGroupId)
        {
            return await Context.Students.Where(x => x.RegGroupId == regGroupId).OrderBy(x => x.Person.LastName).ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsByYearGroup(int yearGroupId)
        {
            return await Context.Students.Where(x => x.YearGroupId == yearGroupId).OrderBy(x => x.Person.LastName)
                .ToListAsync();
        }
    }
}