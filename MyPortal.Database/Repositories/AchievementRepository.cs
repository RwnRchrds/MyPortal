using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class AchievementRepository : BaseRepository, IAchievementRepository
    {
        private readonly string TblName = @"[dbo].[Achievement] AS [Achievement]";
        
        internal static readonly string AllColumns = EntityHelper.GetAllColumns(typeof(Achievement), "Achievement");

        private readonly string JoinAcademicYear =
            @"LEFT JOIN [dbo].[AcademicYear] AS [AcademicYear] ON [AcademicYear].[Id] = [Achievement].[AcademicYearId]";

        private readonly string JoinAchievementType =
            @"LEFT JOIN [dbo].[AchievementType] AS [AchievementType] ON [AchievementType].[Id] = [Achievement].[AchievementTypeId]";

        private readonly string JoinStudent =
            @"LEFT JOIN [dbo].[Student] AS [Student] ON [Student].[Id] = [Achievement].[StudentId]";

        public AchievementRepository(IDbConnection connection) : base(connection)
        {
            
        }

        public async Task<IEnumerable<Achievement>> GetAll()
        {
            var sql = $"SELECT {AllColumns},{AcademicYearRepository.AllColumns},{AchievementTypeRepository.AllColumns},{StudentRepository.AllColumns} FROM {TblName} {JoinAcademicYear} {JoinAchievementType} {JoinStudent}";

            return await Connection.QueryAsync<Achievement>(sql);
        }

        public Task<Achievement> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Achievement entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Achievement entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountByStudent(int studentId, int academicYearId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetPointsByStudent(int studentId, int academicYearId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Achievement>> GetByStudent(int studentId, int academicYearId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetPointsToday()
        {
            throw new NotImplementedException();
        }
    }
}
