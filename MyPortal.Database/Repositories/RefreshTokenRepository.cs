using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class RefreshTokenRepository : BaseReadWriteRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Users", "U", "UserId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(User), "U");

            return query;
        }

        protected override async Task<IEnumerable<RefreshToken>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var tokens = await Transaction.Connection.QueryAsync<RefreshToken, User, RefreshToken>(sql.Sql,
                (token, user) =>
                {
                    token.User = user;

                    return token;
                }, sql.NamedBindings, Transaction);

            return tokens;
        }

        public async Task<IEnumerable<RefreshToken>> GetByUser(Guid userId)
        {
            var query = GenerateQuery();

            query.Where("RefreshToken.UserId", userId);

            return await ExecuteQuery(query);
        }

        public async Task DeleteExpired(Guid userId)
        {
            var query = GenerateEmptyQuery(typeof(RefreshToken));

            query.Where("UserId", userId);
            query.Where("ExpirationDate", "<", DateTime.Now);

            query.AsDelete();

            await ExecuteNonQuery(query);
        }
    }
}
