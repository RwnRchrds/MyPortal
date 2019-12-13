using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyPortal.Data.Extensions;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class StudentRepository : ReadWriteRepository<Student>, IStudentRepository
    {
        public StudentRepository(MyPortalDbContext context) : base(context)
        {

        }

        public new async Task<Student> GetById(int id)
        {
            return await Context.Students.Include(x => x.Person).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Student> GetByIdWithRelated(int studentId, params Expression<Func<Student, object>>[] includeProperties)
        {
            var query = Context.Students.AsQueryable();

            query = query.IncludeMultiple(includeProperties);

            return await query.SingleOrDefaultAsync(x => x.Id == studentId);
        }

        public async Task<Student> GetByUserId(string userId)
        {
            return await Context.Students.Include(x => x.Person).SingleOrDefaultAsync(x => x.Person.UserId == userId);
        }

        public new async Task<IEnumerable<Student>> GetAll()
        {
            return await Context.Students.Include(x => x.Person).ToListAsync();
        }

        public new async Task<IEnumerable<Student>> GetAll<TOrderBy>(Expression<Func<Student, TOrderBy>> orderBy)
        {
            return await Context.Students.Include(x => x.Person).OrderBy(orderBy).ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetOnRoll()
        {
            return await Context.Students.Include(x => x.Person).Where(x =>
                !x.Deleted && x.DateStarting <= DateTime.Today &&
                (x.DateLeaving == null || x.DateLeaving > DateTime.Today)).OrderBy(x => x.Person.LastName).ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetLeavers()
        {
            return await Context.Students.Include(x => x.Person).Where(x => !x.Deleted && x.DateLeaving <= DateTime.Today)
                .OrderBy(x => x.Person.LastName).ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetFuture()
        {
            return await Context.Students.Include(x => x.Person).Where(x => !x.Deleted && x.DateStarting > DateTime.Today)
                .OrderBy(x => x.Person.LastName).ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetByRegGroup(int regGroupId)
        {
            return await Context.Students.Include(x => x.Person).Where(x => x.RegGroupId == regGroupId).OrderBy(x => x.Person.LastName).ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetByYearGroup(int yearGroupId)
        {
            return await Context.Students.Include(x => x.Person).Where(x => x.YearGroupId == yearGroupId).Include(x => x.Person).OrderBy(x => x.Person.LastName)
                .ToListAsync();
        }
    }
}