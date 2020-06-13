using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AttendanceMarkRepository : BaseReadWriteRepository<AttendanceMark>, IAttendanceMarkRepository
    {
        public AttendanceMarkRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
       
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(Person), "StudentPerson");
            query.SelectAll(typeof(AttendanceWeek));
            query.SelectAll(typeof(Period));

            query = JoinRelated(query);

            return query;
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Student", "Student.Id", "AttendanceMark.StudentId");
            query.LeftJoin("dbo.Person AS StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("dbo.AttendanceWeek", "AttendanceWeek.Id", "AttendanceMark.WeekId");
            query.LeftJoin("dbo.AttendancePeriod AS Period", "Period.Id", "AttendanceMark.PeriodId");

            return query;
        }

        protected override async Task<IEnumerable<AttendanceMark>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<AttendanceMark, Student, Person, AttendanceWeek, Period, AttendanceMark>(
                sql.Sql,
                (mark, student, person, week, period) =>
                {
                    mark.Student = student;
                    mark.Student.Person = person;
                    mark.Week = week;
                    mark.Period = period;

                    return mark;
                }, sql.Bindings);
        }

        public async Task<IEnumerable<AttendanceMark>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var query = SelectAllColumns();

            query.Where("Student.Id", "=", studentId);
            query.Where("AttendanceWeek.AcademicYearId", "=", academicYearId);

            return await ExecuteQuery(query);
        }

        public async Task<AttendanceMark> Get(Guid studentId, Guid attendanceWeekId, Guid periodId)
        {
            var query = SelectAllColumns();

            query.Where("Student.Id", "=", studentId);

            query.Where("AttendanceWeek.Id", "=", attendanceWeekId);

            query.Where("Period.Id", "=", periodId);

            return (await ExecuteQuery(query)).SingleOrDefault();
        }

        public async Task Update(AttendanceMark mark)
        {
            var columns = new List<string> {"Mark", "MinutesLate", "Comments"};

            var values = new List<object> {mark.Mark, mark.MinutesLate, mark.Comments};

            var query = new Query(TblName).Where("AttendanceMark.Id", "=", mark.Id).AsUpdate(columns, values);

            await ExecuteNonQuery(query);
        }
    }
}