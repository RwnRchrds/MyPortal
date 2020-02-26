using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class IncidentTypeRepository : BaseReadWriteRepository<IncidentType>, IIncidentTypeRepository
    {
        public IncidentTypeRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }

        protected override async Task<IEnumerable<IncidentType>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<IncidentType>(sql, param);
        }

        public async Task Update(IncidentType entity)
        {
            var typeInDb = await Context.IncidentTypes.FindAsync(entity.Id);

            typeInDb.Description = entity.Description;
            typeInDb.DefaultPoints = entity.DefaultPoints;
        }
    }
}