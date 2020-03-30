using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class MedicalConditionRepository : BaseReadWriteRepository<MedicalCondition>, IMedicalConditionRepository
    {
        public MedicalConditionRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }

        protected override async Task<IEnumerable<MedicalCondition>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<MedicalCondition>(sql, param);
        }
    }
}