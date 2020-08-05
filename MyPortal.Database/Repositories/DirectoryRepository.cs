using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class DirectoryRepository : BaseReadWriteRepository<Directory>, IDirectoryRepository
    {
        public DirectoryRepository(ApplicationDbContext context) : base(context, "Directory")
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

            return await Connection.QueryAsync<Directory, Directory, Directory>(sql.Sql, (directory, parent) =>
                {
                    directory.Parent = parent;

                    return directory;
                }, sql.NamedBindings);
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
