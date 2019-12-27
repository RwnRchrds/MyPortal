using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IIncidentDetentionRepository : IReadWriteRepository<IncidentDetention>
    {
        Task<IEnumerable<IncidentDetention>> GetNotAttended();

        Task<IEnumerable<IncidentDetention>> GetByStudent(int studentId);
    }
}
