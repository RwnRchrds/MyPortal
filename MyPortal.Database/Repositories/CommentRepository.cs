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
    public class CommentRepository : BaseReadWriteRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "Comment")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(CommentBank), "CommentBank");
            
            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("CommentBanks as CommentBank", "CommentBank.Id", "Comment.CommentBankId");
        }

        protected override async Task<IEnumerable<Comment>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<Comment, CommentBank, Comment>(sql.Sql, (comment, bank) =>
                {
                    comment.CommentBank = bank;

                    return comment;
                }, sql.NamedBindings, Transaction);
        }

        public async Task Update(Comment entity)
        {
            var comment = await Context.Comments.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (comment == null)
            {
                throw new EntityNotFoundException("Comment not found.");
            }

            comment.Value = entity.Value;
            comment.CommentBankId = entity.CommentBankId;
        }
    }
}