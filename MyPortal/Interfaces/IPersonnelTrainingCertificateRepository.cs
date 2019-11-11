using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IPersonnelTrainingCertificateRepository : IRepository<PersonnelTrainingCertificate>
    {
        Task<PersonnelTrainingCertificate> Get(int staffId, int courseId);

        Task<IEnumerable<PersonnelTrainingCertificate>> GetByStaffMember(int staffId);
    }
}
