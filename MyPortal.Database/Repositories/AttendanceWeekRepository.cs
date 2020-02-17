using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AttendanceWeekRepository : BaseReadWriteRepository<AttendanceWeek>, IAttendanceWeekRepository
    {
        private readonly string RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(AcademicYear))}";

        private readonly string JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AcademicYear]", "[AcademicYear].[Id]", "[AttendanceWeek].[AcademicYearId]")}";
        
        public AttendanceWeekRepository(IDbConnection connection) : base(connection)
        {
        }

        protected override async Task<IEnumerable<AttendanceWeek>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<AttendanceWeek, AcademicYear, AttendanceWeek>(sql, (week, year) =>
            {
                week.AcademicYear = year;

                return week;
            }, param);
        }

        public async Task<IEnumerable<AttendanceWeek>> GetAll()
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";

            return await ExecuteQuery(sql);
        }

        public async Task<AttendanceWeek> GetById(Guid id)
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";
            
            SqlHelper.Where(ref sql, "[AttendanceWeek].[Id] = @AttendanceWeekId");

            return (await ExecuteQuery(sql, new {AttendanceWeekId = id})).Single();
        }

        public async Task Update(AttendanceWeek entity)
        {
            var weekInDb = await Context.AttendanceWeeks.FindAsync(entity.Id);

            weekInDb.IsHoliday = entity.IsHoliday;
            weekInDb.IsNonTimetable = entity.IsNonTimetable;
        }
    }
}