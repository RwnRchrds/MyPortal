using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
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

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(Person), "StudentPerson");
            query.SelectAll(typeof(AttendanceWeek));
            query.SelectAll(typeof(AttendanceWeekPattern));
            query.SelectAll(typeof(Period));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Student", "Student.Id", "AttendanceMark.StudentId");
            query.LeftJoin("Person AS StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("AttendanceWeek", "AttendanceWeek.Id", "AttendanceMark.WeekId");
            query.LeftJoin("AttendanceWeekPattern", "AttendanceWeekPattern.Id", "AttendanceWeek.WeekPatternId");
            query.LeftJoin("AttendancePeriod AS Period", "Period.Id", "AttendanceMark.PeriodId");
        }

        protected override async Task<IEnumerable<AttendanceMark>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<AttendanceMark, Student, Person, AttendanceWeek, AttendanceWeekPattern, Period, AttendanceMark>(
                sql.Sql,
                (mark, student, person, week, pattern, period) =>
                {
                    mark.Student = student;
                    mark.Student.Person = person;
                    mark.Week = week;
                    mark.Week.WeekPattern = pattern;
                    mark.Period = period;

                    return mark;
                }, sql.NamedBindings);
        }

        public async Task<IEnumerable<AttendanceMark>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var query = SelectAllColumns();

            query.Where("Student.Id", "=", studentId);
            query.Where("AttendanceWeekPattern.AcademicYearId", "=", academicYearId);

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