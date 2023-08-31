using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class CommentRepository : BaseReadWriteRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("CommentBanks as CB", "CB.Id", $"{TblAlias}.CommentBankId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(CommentBank), "CB");

            return query;
        }

        protected override async Task<IEnumerable<Comment>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var comments = await DbUser.Transaction.Connection.QueryAsync<Comment, CommentBankSection, Comment>(sql.Sql,
                (comment, section) =>
                {
                    comment.Section = section;

                    return comment;
                }, sql.NamedBindings, DbUser.Transaction);

            return comments;
        }

        public async Task Update(Comment entity)
        {
            var comment = await DbUser.Context.Comments.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (comment == null)
            {
                throw new EntityNotFoundException("Comment not found.");
            }

            comment.Value = entity.Value;
            comment.CommentBankSectionId = entity.CommentBankSectionId;
        }
    }
}