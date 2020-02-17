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
        private readonly string RelatedColumns = $@"{EntityHelper.GetAllColumns(typeof(CommentBank))}";

        private readonly string JoinRelated =
            $@"{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[CommentBank]", "[CommentBank].[Id]", "[Comment].[CommentBankId]")}";
        
        public CommentRepository(IDbConnection connection) : base(connection)
        {
        }

        protected override async Task<IEnumerable<Comment>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Comment, CommentBank, Comment>(sql, (comment, bank) =>
                {
                    comment.CommentBank = bank;

                    return comment;
                });
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";

            return await ExecuteQuery(sql);
        }

        public async Task<Comment> GetById(Guid id)
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";
            
            SqlHelper.Where(ref sql, "[Comment].[Id] = @CommentId");

            return (await ExecuteQuery(sql, new {CommentId = id})).Single();
        }

        public async Task Update(Comment entity)
        {
            var commentInDb = await Context.Comments.FindAsync(entity.Id);

            commentInDb.CommentBankId = entity.CommentBankId;
            commentInDb.Value = entity.Value;
        }
    }
}