using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class HomeworkRepository : BaseReadWriteRepository<HomeworkItem>, IHomeworkRepository
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
            query.LeftJoin("Directory", "Directory.Id", "HomeworkItem.DirectoryId");
        }

        protected override async Task<IEnumerable<HomeworkItem>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<HomeworkItem, Directory, HomeworkItem>(sql.Sql, (homework, directory) =>
            {
                homework.Directory = directory;

                return homework;
            }, sql.NamedBindings);
        }
    }
}