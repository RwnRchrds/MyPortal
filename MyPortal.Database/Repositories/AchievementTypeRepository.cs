using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AchievementTypeRepository : BaseReadWriteRepository<AchievementType>, IAchievementTypeRepository
    {
        public AchievementTypeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task<IEnumerable<AchievementType>> GetTypesWithRecordedAchievementsByYear(Guid academicYearId)
        {
            var query = GenerateQuery();

            query.LeftJoin("Achievements as A", "A.AchievementTypeId", $"{TblAlias}.Id");

            query.GroupBy(EntityHelper.GetPropertyNames(typeof(AchievementType)));

            query.Having($"COUNT({TblAlias}.Id)", ">", 0);

            return await ExecuteQuery(query);
        }

        public async Task Update(AchievementType entity)
        {
            var achievementType = await Context.AchievementTypes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (achievementType == null)
            {
                throw new EntityNotFoundException("Achievement type not found.");
            }
            
            achievementType.Description = entity.Description;
            achievementType.DefaultPoints = entity.DefaultPoints;
            achievementType.Active = entity.Active;
        }
    }
}
