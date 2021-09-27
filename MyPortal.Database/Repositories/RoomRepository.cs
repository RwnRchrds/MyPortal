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
    public class RoomRepository : BaseReadWriteRepository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Locations", "L", "LocationId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Location), "L");

            return query;
        }

        protected override async Task<IEnumerable<Room>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var rooms = await Transaction.Connection.QueryAsync<Room, Location, Room>(sql.Sql, (room, location) =>
            {
                room.Location = location;

                return room;
            }, sql.NamedBindings, Transaction);

            return rooms;
        }

        public async Task Update(Room entity)
        {
            var room = await Context.Rooms.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (room == null)
            {
                throw new EntityNotFoundException("Room not found.");
            }

            room.LocationId = entity.LocationId;
            room.Code = entity.Code;
            room.Name = entity.Name;
            room.MaxGroupSize = entity.MaxGroupSize;
            room.TelephoneNo = entity.TelephoneNo;
            room.ExcludeFromCover = entity.ExcludeFromCover;
            
        }
    }
}