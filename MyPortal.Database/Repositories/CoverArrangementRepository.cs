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
    public class CoverArrangementRepository : BaseReadWriteRepository<CoverArrangement>, ICoverArrangementRepository
    {
        public CoverArrangementRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("AttendanceWeeks as AW", "AW.Id", $"{TblAlias}.WeekId");
            query.LeftJoin("Sessions as S", "S.Id", $"{TblAlias}.SessionId");
            query.LeftJoin("StaffMembers as T", "T.Id", $"{TblAlias}.TeacherId");
            query.LeftJoin("Rooms as R", "R.Id", $"{TblAlias}.RoomId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AttendanceWeek), "AW");
            query.SelectAllColumns(typeof(Session), "S");
            query.SelectAllColumns(typeof(StaffMember), "T");
            query.SelectAllColumns(typeof(Room), "R");

            return query;
        }

        protected override async Task<IEnumerable<CoverArrangement>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var coverArrangements =
                await DbUser.Transaction.Connection
                    .QueryAsync<CoverArrangement, AttendanceWeek, Session, StaffMember, Room, CoverArrangement>(sql.Sql,
                        (arrangement, week, session, teacher, room) =>
                        {
                            arrangement.Week = week;
                            arrangement.Session = session;
                            arrangement.Teacher = teacher;
                            arrangement.Room = room;

                            return arrangement;
                        }, sql.NamedBindings, DbUser.Transaction);

            return coverArrangements;
        }

        public async Task Update(CoverArrangement entity)
        {
            var coverArrangement = await DbUser.Context.CoverArrangements.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (coverArrangement == null)
            {
                throw new EntityNotFoundException("Cover arrangement not found.");
            }

            coverArrangement.TeacherId = entity.TeacherId;
            coverArrangement.RoomId = entity.RoomId;
            coverArrangement.Comments = entity.Comments;
        }
    }
}