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
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class BulletinRepository : BaseReadWriteRepository<Bulletin>, IBulletinRepository
    {
        public BulletinRepository(IDbConnection connection) : base(connection)
        {
        RelatedColumns = $@"
{EntityHelper.GetUserColumns("User")}";

        JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[Bulletin].[AuthorId]", "User")}";
        }

        protected override async Task<IEnumerable<Bulletin>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Bulletin, ApplicationUser, Bulletin>(sql, (bulletin, author) =>
            {
                bulletin.Author = author;

                return bulletin;
            }, param);
        }

        public async Task Update(Bulletin entity)
        {
            var bulletinInDb = await Context.Bulletins.FindAsync(entity.Id);

            bulletinInDb.Title = entity.Title;
            bulletinInDb.Detail = entity.Detail;
            bulletinInDb.ShowStudents = entity.ShowStudents;
            bulletinInDb.Approved = entity.Approved;
            bulletinInDb.ExpireDate = entity.ExpireDate;
        }

        public async Task<IEnumerable<Bulletin>> GetApproved()
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";
            
            SqlHelper.Where(ref sql, "[Bulletin].[Approved] = 1");

            return await ExecuteQuery(sql);
        }

        public async Task<IEnumerable<Bulletin>> GetStudent()
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";
            
            SqlHelper.Where(ref sql, "[Bulletin].[Approved] = 1");
            SqlHelper.Where(ref sql, "[Bulletin].[ShowStudents] = 1");

            return await ExecuteQuery(sql);
        }

        public async Task<IEnumerable<Bulletin>> GetOwn(Guid authorId)
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";
            
            SqlHelper.Where(ref sql, "[Bulletin].[AuthorId] = @AuthorId");

            return await ExecuteQuery(sql, new {AuthorId = authorId});
        }
    }
}