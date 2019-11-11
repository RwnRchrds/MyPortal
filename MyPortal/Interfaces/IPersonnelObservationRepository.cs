using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IPersonnelObservationRepository : IRepository<PersonnelObservation>
    {
        Task<IEnumerable<PersonnelObservation>> GetByStaffMember(int staffId);
    }
}
