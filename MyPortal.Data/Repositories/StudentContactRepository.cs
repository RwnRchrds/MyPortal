using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class StudentContactRepository : ReadWriteRepository<StudentContact>, IStudentContactRepository
    {
        public StudentContactRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<StudentContact>> GetByStudent(int studentId)
        {
            return await Context.StudentContacts.Where(x => x.StudentId == studentId).ToListAsync();
        }

        public async Task<IEnumerable<StudentContact>> GetByContact(int contactId)
        {
            return await Context.StudentContacts.Where(x => x.ContactId == contactId).ToListAsync();
        }

        public async Task<IEnumerable<StudentContact>> GetByContactParentalResponsibility(int contactId)
        {
            return await Context.StudentContacts.Where(x => x.ContactId == contactId && x.ParentalResponsibility)
                .ToListAsync();
        }
    }
}