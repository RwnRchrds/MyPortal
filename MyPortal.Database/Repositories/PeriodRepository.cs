using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class PeriodRepository : BaseReadWriteRepository<Period>, IPeriodRepository
    {
        public PeriodRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
        }

        protected override async Task<IEnumerable<Period>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Period>(sql, param);
        }

        public async Task Update(Period entity)
        {
            var period = await Context.Periods.FindAsync(entity.Id);

            period.Name = entity.Name;
            period.Weekday = entity.Weekday;
            period.StartTime = entity.StartTime;
            period.EndTime = entity.EndTime;
            period.IsAm = entity.IsAm;
            period.IsPm = entity.IsPm;
        }
    }
}