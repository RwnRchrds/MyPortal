using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IMedicalPersonConditionRepository : IRepository<MedicalPersonCondition>
    {
        Task<IEnumerable<MedicalPersonCondition>> GetMedicalConditionsByPerson(int personId);
    }
}
