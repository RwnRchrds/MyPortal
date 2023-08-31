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
    public class ExamRoomRepository : BaseReadWriteRepository<ExamRoom>, IExamRoomRepository
    {
        public ExamRoomRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Rooms as R", "R.Id", $"{TblAlias}.RoomId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Room), "R");

            return query;
        }

        protected override async Task<IEnumerable<ExamRoom>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var examRooms = await DbUser.Transaction.Connection.QueryAsync<ExamRoom, Room, ExamRoom>(sql.Sql,
                (examRoom, room) =>
                {
                    examRoom.Room = room;

                    return examRoom;
                }, sql.NamedBindings, DbUser.Transaction);

            return examRooms;
        }

        public async Task Update(ExamRoom entity)
        {
            var examRoom = await DbUser.Context.ExamRooms.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (examRoom == null)
            {
                throw new EntityNotFoundException("Exam room not found.");
            }

            examRoom.Columns = entity.Columns;
            examRoom.Rows = entity.Rows;
        }
    }
}