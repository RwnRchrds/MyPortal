using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Attendance;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AttendanceMarkRepository : BaseReadWriteRepository<AttendanceMark>, IAttendanceMarkRepository
    {
        public AttendanceMarkRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
       
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("AttendanceCodes as AC", "AC.Id", $"{TblAlias}.CodeId");
            query.LeftJoin("AttendancePeriods as AP", "AP.Id", $"{TblAlias}.PeriodId");
            query.LeftJoin("AttendanceWeeks as AW", "AW.Id", $"{TblAlias}.WeekId");
            query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");
            query.LeftJoin("Users as U", "U.Id", $"{TblAlias}.CreatedById");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AttendanceCode), "AC");
            query.SelectAllColumns(typeof(AttendancePeriod), "AP");
            query.SelectAllColumns(typeof(AttendanceWeek), "AW");
            query.SelectAllColumns(typeof(Student), "S");
            query.SelectAllColumns(typeof(User), "U");

            return query;
        }

        protected override async Task<IEnumerable<AttendanceMark>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var marks = await Transaction.Connection
                .QueryAsync<AttendanceMark, AttendanceCode, AttendancePeriod, AttendanceWeek, Student, User, AttendanceMark>(
                    sql.Sql,
                    (mark, code, period, week, student, user) =>
                    {
                        mark.Week = week;
                        mark.AttendancePeriod = period;
                        mark.AttendanceCode = code;
                        mark.Student = student;
                        mark.CreatedBy = user;

                        return mark;
                    }, sql.NamedBindings, Transaction);

            return marks;
        }

        public async Task<IEnumerable<AttendanceMark>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var query = GenerateQuery();

            query.LeftJoin("AttendanceWeeks as AW", "AW.Id", $"{TblAlias}.WeekId");
            query.LeftJoin("AcademicTerms AS AT", "AT.Id", "AW.AcademicTermId");
            query.LeftJoin("AcademicYears AS AY", "AY.Id", "AT.AcademicYearId");

            query.Where($"{TblAlias}.StudentId", "=", studentId);
            query.Where("AY.Id", "=", academicYearId);

            return await ExecuteQuery(query);
        }

        public async Task<AttendanceMark> GetMark(Guid studentId, Guid attendanceWeekId, Guid periodId)
        {
            var query = GenerateQuery();
            
            query.LeftJoin("AttendanceWeeks as AW", "AW.Id", $"{TblAlias}.WeekId");

            query.Where($"{TblAlias}.StudentId", "=", studentId);
            query.Where("AW.Id", "=", attendanceWeekId);
            query.Where($"{TblAlias}.PeriodId", "=", periodId);

            return (await ExecuteQuery(query)).SingleOrDefault();
        }

        public async Task<IEnumerable<PossibleAttendanceMark>> GetRegisterMarks(Guid studentGroupId, DateTime startDate, DateTime endDate)
        {
            var query = GenerateEmptyQuery(typeof(Student), "S");

            query.Select("M.Id", "S.Id AS StudentId", "PAP.AttendanceWeekId AS WeekId", "PAP.PeriodId AS PeriodId",
                "M.CodeId", "M.Comments", "M.MinutesLate");

            query.LeftJoin($"{TblName} AS M", "M.StudentId", "S.Id");
            query.CrossJoin("AttendancePeriods_PossibleAttendancePeriods AS PAP");

            query.Where("PAP.StartTime", ">=", startDate);
            query.Where("PAP.EndTime", "<", endDate);

            query.JoinStudentGroups("S", "SGM");

            query.WhereStudentGroup("SGM", studentGroupId, startDate);

            return await ExecuteQuery<PossibleAttendanceMark>(query);
        }

        public async Task Update(AttendanceMark mark)
        {
            var attendanceMark = await Context.AttendanceMarks.FirstOrDefaultAsync(x => x.Id == mark.Id);

            if (attendanceMark == null)
            {
                throw new EntityNotFoundException("Attendance mark not found.");
            }

            attendanceMark.CodeId = mark.CodeId;
            attendanceMark.Comments = mark.Comments;
            attendanceMark.MinutesLate = mark.MinutesLate;
        }
    }
}