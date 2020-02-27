﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class CommentBankRepository : BaseReadWriteRepository<CommentBank>, ICommentBankRepository
    {
        public CommentBankRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }

        protected override async Task<IEnumerable<CommentBank>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<CommentBank>(sql, param);
        }

        public async Task Update(CommentBank entity)
        {
            var commentBankInDb = await Context.CommentBanks.FindAsync(entity.Id);

            commentBankInDb.Name = entity.Name;
            commentBankInDb.Active = entity.Active;
        }
    }
}