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
        public AttendanceMarkRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(Person), "StudentPerson")}
{EntityHelper.GetAllColumns(typeof(AttendanceWeek))},
{EntityHelper.GetAllColumns(typeof(Period))}";

        JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[AttendanceMark].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[StudentPerson].[Id]", "[Student].[PersonId]", "StudentPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AttendanceWeek]","[AttendanceWeek].[Id]", "[AttendanceMark].[WeekId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Period]", "[Period].[Id]", "[AttendanceMark].[PeriodId]")}";
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

        public async Task Update(AttendanceMark entity)
        {
            var markInDb = await Context.AttendanceMarks.FindAsync(entity.Id);

            markInDb.Mark = entity.Mark;
        }
    }
}