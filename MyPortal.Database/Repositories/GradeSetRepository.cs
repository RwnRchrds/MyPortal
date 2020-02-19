using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class GradeSetRepository : BaseReadWriteRepository<GradeSet>, IGradeSetRepository
    {
        public GradeSetRepository(IDbConnection connection) : base(connection)
        {
        }

        protected override async Task<IEnumerable<GradeSet>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<GradeSet>(sql, param);
        }

        public async Task Update(GradeSet entity)
        {
            var gradeSetInDb = await Context.GradeSets.FindAsync(entity.Id);

            gradeSetInDb.Name = entity.Name;
            gradeSetInDb.Active = entity.Active;
        }
    }
}