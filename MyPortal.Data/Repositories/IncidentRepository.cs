using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class IncidentRepository : ReadWriteRepository<Incident>, IIncidentRepository
    {
        public IncidentRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<int> GetCountByStudent(int studentId, int academicYearId)
        {
            return await Context.Incidents.CountAsync(x =>
                x.AcademicYearId == academicYearId && x.StudentId == studentId);
        }

        public async Task<int> GetPointsByStudent(int studentId, int academicYearId)
        {
            return await Context.Incidents
                       .Where(x => x.AcademicYearId == academicYearId && x.StudentId == studentId)
                       .SumAsync(x => (int?) x.Points) ?? 0;
        }

        public async Task<int> GetPointsToday()
        {
            return await Context.Incidents.Where(x => x.Date == DateTime.Today)
                       .SumAsync(x => (int?) x.Points) ?? 0;
        }

        public async Task<IEnumerable<Incident>> GetByStudent(int studentId, int academicYearId)
        {
            return await Context.Incidents
                .Where(x => x.AcademicYearId == academicYearId && x.StudentId == studentId).ToListAsync();
        }
    }
}