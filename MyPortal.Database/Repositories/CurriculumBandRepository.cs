using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class CurriculumBandRepository : BaseReadWriteRepository<CurriculumBand>, ICurriculumBandRepository
    {
        public CurriculumBandRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
        }

        protected override async Task<IEnumerable<CurriculumBand>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<CurriculumBand>(sql, param);
        }

        public async Task Update(CurriculumBand entity)
        {
            var band = await Context.CurriculumBands.FindAsync(entity.Id);

            band.Name = entity.Name;
            band.Description = entity.Description;
        }
    }
}