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
{EntityHelper.GetAllColumns(typeof(Person), "StudentPerson")},
{EntityHelper.GetAllColumns(typeof(AttendanceWeek))},
{EntityHelper.GetAllColumns(typeof(Period))}";

        JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[AttendanceMark].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[StudentPerson].[Id]", "[Student].[PersonId]", "StudentPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AttendanceWeek]","[AttendanceWeek].[Id]", "[AttendanceMark].[WeekId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AttendancePeriod]", "[Period].[Id]", "[AttendanceMark].[PeriodId]", "Period")}";
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

        public async Task<IEnumerable<AttendanceMark>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var sql = SelectAllColumns();

            SqlHelper.Where(ref sql, "[Student].[Id] = @StudentId");

            SqlHelper.Where(ref sql, "[AttendanceWeek].[AcademicYearId] = @AcademicYearId");

            return await ExecuteQuery(sql, new {StudentId = studentId, AcademicYearId = academicYearId});
        }

        public async Task<AttendanceMark> Get(Guid studentId, Guid attendanceWeekId, Guid periodId)
        {
            var sql = SelectAllColumns();

            SqlHelper.Where(ref sql, "[Student].[Id] = @StudentId");

            SqlHelper.Where(ref sql, "[AttendanceWeek].[Id] = @AttendanceWeekId");

            SqlHelper.Where(ref sql, "[Period].[Id] = @PeriodId");

            return (await ExecuteQuery(sql,
                    new {StudentId = studentId, AttendanceWeekId = attendanceWeekId, PeriodId = periodId}))
                .SingleOrDefault();
        }

        public async Task Update(AttendanceMark mark)
        {
            var sql =
                $"UPDATE [dbo].[AttendanceMark] SET [Mark] = @Mark, [MinutesLate] = @MinutesLate, [Comments] = @Comments WHERE [Id] = @Id";

            await ExecuteNonQuery(sql,
                new {Id = mark.Id, Mark = mark.Mark, MinutesLate = mark.MinutesLate, Comments = mark.Comments});
        }
    }
}