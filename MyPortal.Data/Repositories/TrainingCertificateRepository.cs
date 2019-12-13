using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class TrainingCertificateRepository : ReadWriteRepository<TrainingCertificate>, ITrainingCertificateRepository
    {
        public TrainingCertificateRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<TrainingCertificate> Get(int staffId, int courseId)
        {
            return await Context.TrainingCertificates.SingleOrDefaultAsync(x =>
                x.StaffId == staffId && x.CourseId == courseId);
        }

        public async Task<IEnumerable<TrainingCertificate>> GetByStaffMember(int staffId)
        {
            return await Context.TrainingCertificates.Where(x => x.StaffId == staffId).ToListAsync();
        }
    }
}