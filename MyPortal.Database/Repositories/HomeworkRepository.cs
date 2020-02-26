using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class HomeworkRepository : BaseReadWriteRepository<Homework>, IHomeworkRepository
    {
        public HomeworkRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }

        protected override async Task<IEnumerable<Homework>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Homework>(sql, param);
        }

        public async Task Update(Homework entity)
        {
            var homeworkInDb = await Context.Homework.FindAsync(entity.Id);

            homeworkInDb.Title = entity.Title;
            homeworkInDb.Description = entity.Description;
            homeworkInDb.SubmitOnline = entity.SubmitOnline;
        }
    }
}