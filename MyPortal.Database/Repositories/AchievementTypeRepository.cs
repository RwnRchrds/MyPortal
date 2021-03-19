using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class AchievementTypeRepository : BaseReadWriteRepository<AchievementType>, IAchievementTypeRepository
    {
        public AchievementTypeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task<IEnumerable<AchievementType>> GetRecorded(Guid academicYearId)
        {
            var query = GenerateQuery();

            query.LeftJoin("Achievements as Achievement", "Achievement.AchievementTypeId", "AchievementType.Id");

            query.GroupBy(EntityHelper.GetPropertyNames(typeof(AchievementType)));

            query.Having("COUNT([Achievement].[Id])", ">", 0);

            return await ExecuteQuery(query);
        }
    }
}
