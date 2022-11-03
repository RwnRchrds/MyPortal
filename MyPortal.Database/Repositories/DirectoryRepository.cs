using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class DirectoryRepository : BaseReadWriteRepository<Directory>, IDirectoryRepository
    {
        public DirectoryRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
           
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Directories as P", "P", "ParentId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Directory), "P");

            return query;
        }

        protected override async Task<IEnumerable<Directory>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var directories = await Transaction.Connection.QueryAsync<Directory, Directory, Directory>(sql.Sql,
                (directory, parent) =>
                {
                    directory.Parent = parent;

                    return directory;
                }, sql.NamedBindings, Transaction);

            return directories;
        }

        public async Task<IEnumerable<Directory>> GetSubdirectories(Guid directoryId, bool includeRestricted)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.ParentId", "=", directoryId);

            if (!includeRestricted)
            {
                query.Where($"{TblAlias}.Restricted", false);
            }

            return await ExecuteQuery(query);
        }

        public async Task Update(Directory entity)
        {
            var directory = await Context.Directories.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (directory == null)
            {
                throw new EntityNotFoundException("Directory not found.");
            }

            directory.Name = entity.Name;
            directory.Private = entity.Private;
        }
    }
}
