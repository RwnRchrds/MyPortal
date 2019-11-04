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
    public class PersonnelTrainingCertificateRepository : Repository<PersonnelTrainingCertificate>, IPersonnelTrainingCertificateRepository
    {
        public PersonnelTrainingCertificateRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<PersonnelTrainingCertificate> GetCertificate(int staffId, int courseId)
        {
            return await Context.PersonnelTrainingCertificates.SingleOrDefaultAsync(x =>
                x.StaffId == staffId && x.CourseId == courseId);
        }

        public async Task<IEnumerable<PersonnelTrainingCertificate>> GetCertificatesByStaffMember(int staffId)
        {
            return await Context.PersonnelTrainingCertificates.Where(x => x.StaffId == staffId).ToListAsync();
        }
    }
}