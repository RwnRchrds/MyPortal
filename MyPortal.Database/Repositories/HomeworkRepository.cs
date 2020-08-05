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
        public HomeworkRepository(ApplicationDbContext context) : base(context, "HomeworkItem")
        {
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Directory), "Directory");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Directories as Directory", "Directory.Id", "HomeworkItem.DirectoryId");
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