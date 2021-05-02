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

namespace MyPortal.Database.Repositories
{
    public class UserRepository : BaseReadWriteRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        public async Task<bool> UserExists(string username)
        {
            var query = GenerateEmptyQuery(typeof(User), "User");

            query.Where("User.UserName", username);

            query.AsCount();

            var result = await ExecuteQueryIntResult(query);

            return result > 0;
        }

        public async Task<User> GetByUsername(string username)
        {
            var query = GenerateQuery();

            query.Where("User.UserName", username);

            return await ExecuteQueryFirstOrDefault(query);
        }
    }
}