using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class RoomClosureRepository : BaseReadWriteRepository<RoomClosure>, IRoomClosureRepository
    {
        public RoomClosureRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Rooms", "R", "RoomId");
            JoinEntity(query, "RoomClosureReasons", "RCR", "ReasonId");

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

            var closures = await Transaction.Connection.QueryAsync<RoomClosure, Room, RoomClosureReason, RoomClosure>(
                sql.Sql,
                (closure, room, reason) =>
                {
                    closure.Room = room;
                    closure.Reason = reason;

                    return closure;
                }, sql.NamedBindings, Transaction);

            return closures;
        }

        public async Task Update(RoomClosure entity)
        {
            var closure = await Context.RoomClosures.FirstOrDefaultAsync(x => x.Id == entity.Id);

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