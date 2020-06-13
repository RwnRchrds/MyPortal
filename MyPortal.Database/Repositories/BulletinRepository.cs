using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class BulletinRepository : BaseReadWriteRepository<Bulletin>, IBulletinRepository
    {
        public BulletinRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
       
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(ApplicationUser), "User");

            query = JoinRelated(query);

            return query;
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("dbo.AspNetUsers as User", "User.Id", "Bulletin.AuthorId");

            return query;
        }

        protected override async Task<IEnumerable<Bulletin>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Bulletin, ApplicationUser, Bulletin>(sql.Sql, (bulletin, author) =>
            {
                bulletin.Author = author;

                return bulletin;
            }, sql.Bindings);
        }

        public async Task<IEnumerable<Bulletin>> GetApproved()
        {
            var query = SelectAllColumns();

            query.Where("Bulletin.Approved", "=", true);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<Bulletin>> GetStudent()
        {
            var query = SelectAllColumns();

            query.Where("Bulletin.Approved", "=", true);
            query.Where("Bulletin.ShowStudents", "=", true);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<Bulletin>> GetOwn(Guid authorId)
        {
            var query = SelectAllColumns();

            query.Where("Bulletin.AuthorId", "=", authorId);

            return await ExecuteQuery(query);
        }
    }
}