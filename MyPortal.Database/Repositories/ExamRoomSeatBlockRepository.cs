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
    public class ExamRoomSeatBlockRepository : BaseReadWriteRepository<ExamRoomSeatBlock>, IExamRoomSeatBlockRepository
    {
        public ExamRoomSeatBlockRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ExamRooms as ER", "ER.Id", $"{TblAlias}.ExamRoomId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamRoom), "ER");

            return query;
        }

        protected override async Task<IEnumerable<ExamRoomSeatBlock>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var seatBlocks =
                await DbUser.Transaction.Connection.QueryAsync<ExamRoomSeatBlock, ExamRoom, ExamRoomSeatBlock>(
                    sql.Sql,
                    (block, room) =>
                    {
                        block.ExamRoom = room;

                        return block;
                    }, sql.NamedBindings, DbUser.Transaction);

            return seatBlocks;
        }

        public async Task Update(ExamRoomSeatBlock entity)
        {
            var seatBlock = await DbUser.Context.ExamRoomSeatBlocks.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (seatBlock == null)
            {
                throw new EntityNotFoundException("Seat block not found.");
            }

            seatBlock.Comments = entity.Comments;
        }
    }
}