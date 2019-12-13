using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class AchievementRepository : ReadWriteRepository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<int> GetCountByStudent(int studentId, int academicYearId)
        {
            return await Context.Achievements.CountAsync(x => x.StudentId == studentId && x.AcademicYearId == academicYearId);
        }

        public async Task<int> GetPointsByStudent(int studentId, int academicYearId)
        {
            return await Context.Achievements
                       .Where(x => x.StudentId == studentId && x.AcademicYearId == academicYearId)
                       .SumAsync(x => (int?) x.Points) ?? 0;
        }

        public async Task<IEnumerable<Achievement>> GetByStudent(int studentId, int academicYearId)
        {
            return await Context.Achievements
                .Where(x => x.StudentId == studentId && x.AcademicYearId == academicYearId).ToListAsync();
        }

        public async Task<int> GetPointsToday()
        {
            return await Context.Achievements.Where(x => x.Date == DateTime.Today)
                       .SumAsync(x => (int?) x.Points) ?? 0;
        }
    }
}