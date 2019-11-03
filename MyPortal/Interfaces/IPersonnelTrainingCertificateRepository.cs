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
        Task<PersonnelTrainingCertificate> GetCertificate(int staffId, int courseId);

        Task<IEnumerable<PersonnelTrainingCertificate>> GetCertificatesByStaffMember(int staffId);
    }
}
