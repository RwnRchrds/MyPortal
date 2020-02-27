using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SubjectRepository : BaseReadWriteRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
        }

        protected override async Task<IEnumerable<Subject>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Subject>(sql, param);
        }

        public async Task Update(Subject entity)
        {
            var subject = await Context.Subjects.FindAsync(entity.Id);

            subject.Name = entity.Name;
            subject.Code = entity.Code;
            subject.Deleted = entity.Deleted;
        }
    }
}