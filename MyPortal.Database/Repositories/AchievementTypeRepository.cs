using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class AchievementTypeRepository : BaseReadWriteRepository<AchievementType>, IAchievementTypeRepository
    {
        public AchievementTypeRepository(ApplicationDbContext context) : base(context)
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
