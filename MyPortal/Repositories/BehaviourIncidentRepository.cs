using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class BehaviourIncidentRepository : Repository<BehaviourIncident>, IBehaviourIncidentRepository
    {
        public BehaviourIncidentRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<int> GetBehaviourIncidentCountByStudent(int studentId, int academicYearId)
        {
            return await Context.BehaviourIncidents.CountAsync(x =>
                x.AcademicYearId == academicYearId && x.StudentId == studentId);
        }

        public async Task<int> GetBehaviourIncidentPointsCountByStudent(int studentId, int academicYearId)
        {
            return await Context.BehaviourIncidents
                       .Where(x => x.AcademicYearId == academicYearId && x.StudentId == studentId)
                       .SumAsync(x => (int?) x.Points) ?? 0;
        }

        public async Task<int> GetBehaviourIncidentPointsToday()
        {
            return await Context.BehaviourIncidents.Where(x => x.Date == DateTime.Today)
                       .SumAsync(x => (int?) x.Points) ?? 0;
        }

        public async Task<IEnumerable<BehaviourIncident>> GetBehaviourIncidentsByStudent(int studentId, int academicYearId)
        {
            return await Context.BehaviourIncidents
                .Where(x => x.AcademicYearId == academicYearId && x.StudentId == studentId).ToListAsync();
        }
    }
}