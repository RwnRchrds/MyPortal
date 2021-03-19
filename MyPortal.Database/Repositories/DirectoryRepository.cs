using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class DirectoryRepository : BaseReadWriteRepository<Directory>, IDirectoryRepository
    {
        public DirectoryRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "Directory")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Directory), "Parent");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Directories as Parent", "Parent.Id", "Directory.ParentId");
        }

        protected override async Task<IEnumerable<Directory>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<Directory, Directory, Directory>(sql.Sql, (directory, parent) =>
                {
                    directory.Parent = parent;

                    return directory;
                }, sql.NamedBindings, Transaction);
        }

        public async Task<IEnumerable<Directory>> GetSubdirectories(Guid directoryId, bool includeStaffOnly)
        {
            var query = GenerateQuery();

            query.Where("Directory.ParentId", "=", directoryId);

            if (!includeStaffOnly)
            {
                query.Where("Directory.StaffOnly", false);
            }

            return await ExecuteQuery(query);
        }
    }
}
