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
    public class RoomClosureRepository : BaseReadWriteRepository<RoomClosure>, IRoomClosureRepository
    {
        public RoomClosureRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Rooms as R", "R.Id", $"{TblAlias}.RoomId");
            query.LeftJoin("RoomClosureReasons as RCR", "RCR.Id", $"{TblAlias}.ReasonId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Room), "R");
            query.SelectAllColumns(typeof(RoomClosureReason), "RCR");

            return query;
        }

        protected override async Task<IEnumerable<RoomClosure>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var closures = await DbUser.Transaction.Connection.QueryAsync<RoomClosure, Room, RoomClosureReason, RoomClosure>(
                sql.Sql,
                (closure, room, reason) =>
                {
                    closure.Room = room;
                    closure.Reason = reason;

                    return closure;
                }, sql.NamedBindings, DbUser.Transaction);

            return closures;
        }

        public async Task Update(RoomClosure entity)
        {
            var closure = await DbUser.Context.RoomClosures.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (closure == null)
            {
                throw new EntityNotFoundException("Room closure not found.");
            }

            closure.ReasonId = entity.ReasonId;
            closure.StartDate = entity.StartDate;
            closure.EndDate = entity.EndDate;
            closure.Notes = entity.Notes;
        }
    }
}