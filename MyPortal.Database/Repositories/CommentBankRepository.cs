using System;
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
        public CommentBankRepository(IDbConnection connection) : base(connection)
        {
        }

        protected override async Task<IEnumerable<CommentBank>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<CommentBank>(sql, param);
        }

        public async Task<IEnumerable<CommentBank>> GetAll()
        {
            var sql = $"SELECT {AllColumns} FROM {TblName}";

            return await ExecuteQuery(sql);
        }

        public async Task<CommentBank> GetById(Guid id)
        {
            var sql = $"SELECT {AllColumns} FROM {TblName}";
            
            SqlHelper.Where(ref sql, "[CommentBank].[Id] = @CommentBankId");

            return (await ExecuteQuery(sql, new {CommentBankId = id})).Single();
        }

        public async Task Update(CommentBank entity)
        {
            var commentBankInDb = await Context.CommentBanks.FindAsync(entity.Id);

            commentBankInDb.Name = entity.Name;
            commentBankInDb.Active = entity.Active;
        }
    }
}