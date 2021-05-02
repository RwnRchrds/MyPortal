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
