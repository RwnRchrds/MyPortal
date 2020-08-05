using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class BulletinRepository : BaseReadWriteRepository<Bulletin>, IBulletinRepository
    {
        public BulletinRepository(ApplicationDbContext context) : base(context, "Bulletin")
        {
       
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ApplicationUser), "User");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("AspNetUsers as User", "User.Id", "Bulletin.AuthorId");
        }

        protected override async Task<IEnumerable<Bulletin>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Bulletin, ApplicationUser, Bulletin>(sql.Sql, (bulletin, author) =>
            {
                bulletin.Author = author;

                return bulletin;
            }, sql.NamedBindings);
        }

        public async Task<IEnumerable<Bulletin>> GetApproved()
        {
            var query = GenerateQuery();

            query.Where("Bulletin.Approved", "=", true);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<Bulletin>> GetStudent()
        {
            var query = GenerateQuery();

            query.Where("Bulletin.Approved", "=", true);
            query.Where("Bulletin.ShowStudents", "=", true);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<Bulletin>> GetOwn(Guid authorId)
        {
            var query = GenerateQuery();

            query.Where("Bulletin.AuthorId", "=", authorId);

            return await ExecuteQuery(query);
        }
    }
}