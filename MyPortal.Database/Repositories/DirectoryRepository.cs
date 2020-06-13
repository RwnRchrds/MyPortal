using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class DirectoryRepository : BaseReadWriteRepository<Directory>, IDirectoryRepository
    {
        public DirectoryRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
           
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Directory), "Parent");

            query = JoinRelated(query);

            return query;
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Directory as Parent", "Parent.Id", "Directory.ParentId");

            return query;
        }

        protected override async Task<IEnumerable<Directory>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Directory, Directory, Directory>(sql.Sql, (directory, parent) =>
                {
                    directory.Parent = parent;

                    return directory;
                }, sql.Bindings);
        }

        public async Task<IEnumerable<Directory>> GetSubdirectories(Guid directoryId, bool includeStaffOnly)
        {
            var query = SelectAllColumns();

            query.Where("Directory.ParentId", "=", directoryId);

            if (!includeStaffOnly)
            {
                query.Where("Directory.StaffOnly", false);
            }

            return await ExecuteQuery(query);
        }
    }
}
