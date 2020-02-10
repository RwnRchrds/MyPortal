using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AchievementTypeRepository : BaseReadWriteRepository<AchievementType>, IAchievementTypeRepository
    {


        private readonly string JoinAchievement =
            @"LEFT JOIN [dbo].[Achievement] AS [Achievement] ON [Achievement].[AchievementTypeId] = [AchievementType].[Id]";

        public AchievementTypeRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<AchievementType>> GetAll()
        {
            var sql = $"SELECT {AllColumns} FROM {TblName}";

            return await Connection.QueryAsync<AchievementType>(sql);
        }

        public async Task<AchievementType> GetById(Guid id)
        {
            var sql = $"SELECT {AllColumns} FROM {TblName} WHERE [AchievementType].[Id] = @AchievementTypeId";

            return await Connection.QuerySingleOrDefaultAsync<AchievementType>(sql, new {AchievementTypeId = id});
        }

        public async Task Update(AchievementType entity)
        {
            var achievementTypeInDb = await Context.AchievementTypes.FindAsync(entity.Id);

            if (achievementTypeInDb == null)
            {
                throw new Exception("Achievement type not found.");
            }

            achievementTypeInDb.Description = entity.Description;
            achievementTypeInDb.DefaultPoints = entity.DefaultPoints;
        }

        public async Task<IEnumerable<AchievementType>> GetRecorded(Guid academicYearId)
        {
            var sql =
                $"SELECT {AllColumns} FROM {TblName} {JoinAchievement} GROUP BY {AllColumns} HAVING COUNT ([Achievement].[Id]) > 0";

            return await Connection.QueryAsync<AchievementType>(sql);
        }

        protected override async Task<IEnumerable<AchievementType>> ExecuteQuery(string sql, object param = null)
        {
            throw new NotImplementedException();
        }
    }
}
