using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IAssessmentResultSetRepository : IRepository<AssessmentResultSet>
    {
        Task<IEnumerable<AssessmentResultSet>> GetResultSetsByStudent(int studentId);
        Task<AssessmentResultSet> GetCurrent();
    }
}
