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

namespace MyPortal.Database.Repositories
{
    public class DirectoryRepository : BaseReadWriteRepository<Directory>, IDirectoryRepository
    {
        public DirectoryRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Directory), "Parent")}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Directory]", "[Parent].[Id]", "[Directory].[ParentId]", "Parent")}";
        }

        protected override async Task<IEnumerable<Directory>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Directory, Directory, Directory>(sql, (directory, parent) =>
                {
                    directory.Parent = parent;

                    return directory;
                }, param);
        }

        public async Task<IEnumerable<Directory>> GetSubdirectories(Guid directoryId, bool includeStaffOnly)
        {
            var sql = SelectAllColumns();

            SqlHelper.Where(ref sql, "[Parent].[Id] = @ParentId");

            if (!includeStaffOnly)
            {
                SqlHelper.Where(ref sql, "[Directory].[StaffOnly] = 0");
            }

            return await ExecuteQuery(sql, new {ParentId = directoryId});
        }
    }
}
