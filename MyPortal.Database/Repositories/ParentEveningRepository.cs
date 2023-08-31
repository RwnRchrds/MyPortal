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
    public class ParentEveningRepository : BaseReadWriteRepository<ParentEvening>, IParentEveningRepository
    {
        public ParentEveningRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("DiaryEvents as DE", "DE.Id", $"{TblAlias}.EventId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(DiaryEvent), "DE");

            return query;
        }

        protected override async Task<IEnumerable<ParentEvening>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var parentEvenings = await DbUser.Transaction.Connection.QueryAsync<ParentEvening, DiaryEvent, ParentEvening>(
                sql.Sql,
                (evening, diaryEvent) =>
                {
                    evening.Event = diaryEvent;

                    return evening;
                }, sql.NamedBindings, DbUser.Transaction);

            return parentEvenings;
        }

        public async Task Update(ParentEvening entity)
        {
            var evening = await DbUser.Context.ParentEvenings.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (evening == null)
            {
                throw new EntityNotFoundException("Parent evening not found.");
            }

            evening.BookingOpened = entity.BookingOpened;
            evening.BookingClosed = entity.BookingClosed;
        }
    }
}