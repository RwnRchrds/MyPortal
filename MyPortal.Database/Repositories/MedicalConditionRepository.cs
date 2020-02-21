using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class MedicalConditionRepository : BaseReadWriteRepository<MedicalCondition>, IMedicalConditionRepository
    {
        public MedicalConditionRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
        }

        protected override async Task<IEnumerable<MedicalCondition>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<MedicalCondition>(sql, param);
        }

        public async Task Update(MedicalCondition entity)
        {
            var mc = await Context.Conditions.FindAsync(entity.Id);

            mc.Description = entity.Description;
        }
    }
}