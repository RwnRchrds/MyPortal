using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class HomeworkRepository : BaseReadWriteRepository<Homework>, IHomeworkRepository
    {
        public HomeworkRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Directory));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Directory", "Directory.Id", "Homework.DirectoryId");
        }

        protected override async Task<IEnumerable<Homework>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Homework, Directory, Homework>(sql.Sql, (homework, directory) =>
            {
                homework.Directory = directory;

                return homework;
            }, sql.Bindings);
        }
    }
}