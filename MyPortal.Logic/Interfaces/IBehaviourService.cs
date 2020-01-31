using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Logic.Interfaces
{
    public interface IBehaviourService
    {
        Task AddIncidentToDetention(int incidentId, int detentionId);
        Task RemoveIncidentFromDetention(int incidentId, int detentionId);
    }
}
