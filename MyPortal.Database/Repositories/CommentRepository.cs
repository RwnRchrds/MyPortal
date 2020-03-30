using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class CommentRepository : BaseReadWriteRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
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
    }
}