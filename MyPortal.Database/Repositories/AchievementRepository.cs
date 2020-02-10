using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AchievementRepository : BaseReadWriteRepository<Achievement>, IAchievementRepository
    {
        private readonly string RelatedColumns = "";

        private readonly string JoinRelated = "";

        public AchievementRepository(IDbConnection connection) : base(connection)
        {
            
        }

        public async Task<IEnumerable<Achievement>> GetAll()
        {
            var sql =
                $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";

            return await ExecuteQuery(sql);
        }

        public async Task<Achievement> GetById(Guid id)
        {
            var sql = $"SELECT {AllColumns} FROM {TblName} WHERE [Achievement].[Id] = @AchievementId";

            return (await ExecuteQuery(sql, new {AchievementId = id})).First();
        }

        public async Task Update(Achievement entity)
        {
            var achievementInDb = await Context.Achievements.FindAsync(entity.Id);

            achievementInDb.AchievementTypeId = entity.AchievementTypeId;
            achievementInDb.Points = entity.Points;
            achievementInDb.LocationId = entity.LocationId;
            achievementInDb.Comments = entity.Comments;
        }

        public async Task<int> GetCountByStudent(int studentId, int academicYearId)
        {
            var sql = $"SELECT COUNT([Id]) FROM {TblName} WHERE [StudentId] = @StudentId AND [AcademicYearId] = @AcademicYearId";

            return (await ExecuteIntQuery(sql, new {StudentId = studentId, AcademicYearId = academicYearId})).First();
        }

        public async Task<int> GetPointsByStudent(int studentId, int academicYearId)
        {
            var sql = $"SELECT SUM([Points]) FROM {TblName} WHERE [StudentId] = @StudentId AND [AcademicYearId] = @AcademicYearId";

            return (await ExecuteIntQuery(sql, new {StudentId = studentId, AcademicYearId = academicYearId})).First();
        }

        public Task<IEnumerable<Achievement>> GetByStudent(int studentId, int academicYearId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetPointsToday()
        {
            throw new NotImplementedException();
        }

        protected override async Task<IEnumerable<Achievement>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection
                .QueryAsync<Achievement, AcademicYear, AchievementType, Student, Location, ApplicationUser, Achievement
                >(sql, (
                    (achievement, acadYear, type, student, location, recordedBy) =>
                    {
                        achievement.AcademicYear = acadYear;
                        achievement.Type = type;
                        achievement.Student = student;
                        achievement.Location = location;
                        achievement.RecordedBy = recordedBy;

                        return achievement;
                    }), param);
        }
    }
}
