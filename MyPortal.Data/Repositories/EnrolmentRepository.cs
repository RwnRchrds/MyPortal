using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class EnrolmentRepository : ReadWriteRepository<Enrolment>, IEnrolmentRepository
    {
        public EnrolmentRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Enrolment>> GetByClass(int classId)
        {
            return await Context.Enrolments.Where(x => x.ClassId == classId).OrderBy(x => x.Student.Person.LastName).ToListAsync();
        }

        public async Task<IEnumerable<Enrolment>> GetByStudent(int studentId)
        {
            return await Context.Enrolments.Where(x => x.StudentId == studentId).OrderBy(x => x.Class.Name).ToListAsync();
        }
    }
}