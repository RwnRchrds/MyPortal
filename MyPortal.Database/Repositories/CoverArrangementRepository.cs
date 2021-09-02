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
    public class CoverArrangementRepository : BaseReadWriteRepository<CoverArrangement>, ICoverArrangementRepository
    {
        public CoverArrangementRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "AttendanceWeeks", "AW", "WeekId");
            JoinEntity(query, "Sessions", "S", "SessionId");
            JoinEntity(query, "StaffMembers", "T", "TeacherId");
            JoinEntity(query, "Rooms", "R", "RoomId");

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
                await Transaction.Connection
                    .QueryAsync<CoverArrangement, AttendanceWeek, Session, StaffMember, Room, CoverArrangement>(sql.Sql,
                        (arrangement, week, session, teacher, room) =>
                        {
                            arrangement.Week = week;
                            arrangement.Session = session;
                            arrangement.Teacher = teacher;
                            arrangement.Room = room;

                            return arrangement;
                        }, sql.NamedBindings, Transaction);

            return coverArrangements;
        }

        public async Task Update(CoverArrangement entity)
        {
            var coverArrangement = await Context.CoverArrangements.FirstOrDefaultAsync(x => x.Id == entity.Id);

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