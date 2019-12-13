using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface ITrainingCertificateRepository : IReadWriteRepository<TrainingCertificate>
    {
        Task<TrainingCertificate> Get(int staffId, int courseId);

        Task<IEnumerable<TrainingCertificate>> GetByStaffMember(int staffId);
    }
}
