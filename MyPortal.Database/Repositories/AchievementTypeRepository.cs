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
    public class AchievementTypeRepository : BaseRepository, IAchievementTypeRepository
    {
        private readonly string TblName = @"[dbo].[AchievementType]";

        internal static readonly string AllColumns =
            EntityHelper.GetAllColumns(typeof(AchievementType), "AchievementType");

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

        public async Task<AchievementType> GetById(int id)
        {
            var sql = $"SELECT {AllColumns} FROM {TblName} WHERE [AchievementType].[Id] = @AchievementTypeId";

            return await Connection.QuerySingleOrDefaultAsync<AchievementType>(sql, new {AchievementTypeId = id});
        }

        public void Create(AchievementType entity)
        {
            Context.AchievementTypes.Add(entity);
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

        public async Task Delete(int id)
        {
            var achievementTypeInDb = await Context.AchievementTypes.FindAsync(id);

            if (achievementTypeInDb == null)
            {
                throw new Exception("Achievement type not found.");
            }

            Context.AchievementTypes.Remove(achievementTypeInDb);
        }

        public async Task<IEnumerable<AchievementType>> GetRecorded(int academicYearId)
        {
            var sql =
                $"SELECT {AllColumns} FROM {TblName} {JoinAchievement} GROUP BY {AllColumns} HAVING COUNT ([Achievement].[Id]) > 0";

            return await Connection.QueryAsync<AchievementType>(sql);
        }
    }
}
