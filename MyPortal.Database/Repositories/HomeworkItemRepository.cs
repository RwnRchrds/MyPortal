using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class HomeworkItemRepository : BaseReadWriteRepository<HomeworkItem>, IHomeworkItemRepository
    {
        public HomeworkItemRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Directories", "D", "DirectoryId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Directory), "D");

            return query;
        }

        protected override async Task<IEnumerable<HomeworkItem>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var homeworkItems = await Transaction.Connection.QueryAsync<HomeworkItem, Directory, HomeworkItem>(sql.Sql,
                (homework, directory) =>
                {
                    homework.Directory = directory;

                    return homework;
                }, sql.NamedBindings, Transaction);

            return homeworkItems;
        }

        public async Task Update(HomeworkItem entity)
        {
            var homeworkItem = await Context.Homework.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (homeworkItem == null)
            {
                throw new EntityNotFoundException("Homework item not found.");
            }

            homeworkItem.Title = entity.Title;
            homeworkItem.Description = entity.Description;
            homeworkItem.SubmitOnline = entity.SubmitOnline;
        }
    }
}