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
    public class BehaviourAchievementRepository : ReadWriteRepository<BehaviourAchievement>, IBehaviourAchievementRepository
    {
        public BehaviourAchievementRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<int> GetCountByStudent(int studentId, int academicYearId)
        {
            return await Context.BehaviourAchievements.CountAsync(x => x.StudentId == studentId && x.AcademicYearId == academicYearId);
        }

        public async Task<int> GetPointsByStudent(int studentId, int academicYearId)
        {
            return await Context.BehaviourAchievements
                       .Where(x => x.StudentId == studentId && x.AcademicYearId == academicYearId)
                       .SumAsync(x => (int?) x.Points) ?? 0;
        }

        public async Task<IEnumerable<BehaviourAchievement>> GetByStudent(int studentId, int academicYearId)
        {
            return await Context.BehaviourAchievements
                .Where(x => x.StudentId == studentId && x.AcademicYearId == academicYearId).ToListAsync();
        }

        public async Task<int> GetPointsToday()
        {
            return await Context.BehaviourAchievements.Where(x => x.Date == DateTime.Today)
                       .SumAsync(x => (int?) x.Points) ?? 0;
        }
    }
}