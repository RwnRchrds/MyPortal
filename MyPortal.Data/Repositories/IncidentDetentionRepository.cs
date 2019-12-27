using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class IncidentDetentionRepository : ReadWriteRepository<IncidentDetention>, IIncidentDetentionRepository
    {
        public IncidentDetentionRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<IncidentDetention>> GetNotAttended()
        {
            return await Context.IncidentDetentions
                .Where(x => x.Detention.Event.EndTime < DateTime.Now && !x.AttendanceStatus.Attended).ToListAsync();
        }

        public async Task<IEnumerable<IncidentDetention>> GetByStudent(int studentId)
        {
            return await Context.IncidentDetentions.Where(x => x.Incident.StudentId == studentId).ToListAsync();
        }
    }
}
