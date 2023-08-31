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
    public class ExamComponentSittingRepository : BaseReadWriteRepository<ExamComponentSitting>,
        IExamComponentSittingRepository
    {
        public ExamComponentSittingRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ExamComponents as EC", "EC.Id", $"{TblAlias}.ComponentId");
            query.LeftJoin("ExamRooms as ER", "ER.Id", $"{TblAlias}.RoomId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamComponent), "EC");
            query.SelectAllColumns(typeof(ExamRoom), "ER");

            return query;
        }

        protected override async Task<IEnumerable<ExamComponentSitting>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var sittings = await DbUser.Transaction.Connection
                .QueryAsync<ExamComponentSitting, ExamComponent, ExamRoom, ExamComponentSitting>(sql.Sql,
                    (sitting, component, room) =>
                    {
                        sitting.Component = component;
                        sitting.Room = room;

                        return sitting;
                    }, sql.NamedBindings, DbUser.Transaction);

            return sittings;
        }

        public async Task Update(ExamComponentSitting entity)
        {
            var sitting = await DbUser.Context.ExamComponentSittings.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (sitting == null)
            {
                throw new EntityNotFoundException("Component sitting not found.");
            }

            sitting.ExamRoomId = entity.ExamRoomId;
            sitting.ActualStartTime = entity.ActualStartTime;
            sitting.ExtraTimePercent = entity.ExtraTimePercent;
        }
    }
}