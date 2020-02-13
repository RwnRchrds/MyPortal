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
    public class AttendanceMarkRepository : BaseReadWriteRepository<AttendanceMark>, IAttendanceMarkRepository
    {
        private readonly string RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(Person), "StudentPerson")}
{EntityHelper.GetAllColumns(typeof(AttendanceWeek))},
{EntityHelper.GetAllColumns(typeof(Period))}";

        private readonly string JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[AttendanceMark].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[StudentPerson].[Id]", "[Student].[PersonId]", "StudentPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AttendanceWeek]","[AttendanceWeek].[Id]", "[AttendanceMark].[WeekId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Period]", "[Period].[Id]", "[AttendanceMark].[PeriodId]")}";
        
        public AttendanceMarkRepository(IDbConnection connection) : base(connection)
        {
        }

        protected override async Task<IEnumerable<AttendanceMark>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<AttendanceMark, Student, Person, AttendanceWeek, Period, AttendanceMark>(
                sql,
                (mark, student, person, week, period) =>
                {
                    mark.Student = student;
                    mark.Student.Person = person;
                    mark.Week = week;
                    mark.Period = period;

                    return mark;
                }, param);
        }

        public async Task<IEnumerable<AttendanceMark>> GetAll()
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";

            return await ExecuteQuery(sql);
        }

        public async Task<AttendanceMark> GetById(Guid id)
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";
            
            SqlHelper.Where(ref sql, "[AttendanceMark].[Id] = @MarkId");

            return (await ExecuteQuery(sql, new {MarkId = id})).Single();
        }

        public async Task Update(AttendanceMark entity)
        {
            var markInDb = await Context.AttendanceMarks.FindAsync(entity.Id);

            markInDb.Mark = entity.Mark;
        }
    }
}