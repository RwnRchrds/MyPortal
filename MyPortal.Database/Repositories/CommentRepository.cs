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
    public class CommentRepository : BaseReadWriteRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IDbConnection connection) : base(connection)
        {
        RelatedColumns = $@"{EntityHelper.GetAllColumns(typeof(CommentBank))}";

        JoinRelated =
            $@"{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[CommentBank]", "[CommentBank].[Id]", "[Comment].[CommentBankId]")}";
        }

        protected override async Task<IEnumerable<Comment>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Comment, CommentBank, Comment>(sql, (comment, bank) =>
                {
                    comment.CommentBank = bank;

                    return comment;
                });
        }

        public async Task Update(Comment entity)
        {
            var commentInDb = await Context.Comments.FindAsync(entity.Id);

            commentInDb.CommentBankId = entity.CommentBankId;
            commentInDb.Value = entity.Value;
        }
    }
}