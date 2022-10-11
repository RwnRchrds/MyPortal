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

        public async Task<IEnumerable<AttendanceMarkMetadata>> GetRegisterMarks(Guid studentGroupId,
            PossibleAttendancePeriod[] attendancePeriods)
        {
            if (attendancePeriods.Any())
            {
                var runAsDate = attendancePeriods.OrderBy(p => p.ActualStartTime).Select(p => p.ActualStartTime)
                    .FirstOrDefault();
            
                var query = GenerateEmptyQuery(typeof(AttendanceMark), "AM");

                JoinRelated(query);

                JoinEntity(query, "People", "P", "S.PersonId");

                query.Select("AM.Id as AttendanceMarkId", "AM.StudentId as StudentId", "AM.WeekId as WeekId",
                    "AM.PeriodId as PeriodId", "AM.CodeId as CodeId", "AM.CreatedById as CreatedById",
                    "AM.Comments as Comments", "AM.MinutesLate as MinutesLate");

                query.Select("CONCAT(P.LastName, ', ', P.FirstName) as StudentName");

                query.JoinStudentGroups("S", "SGM");

                query.Where(q =>
                {
                    for (int i = 0; i < attendancePeriods.Length; i++)
                    {
                        var period = attendancePeriods[i];

                        if (i == 0)
                        {
                            q.Where(sq =>
                                sq.Where($"AM.PeriodId", period.PeriodId).Where($"AM.WeekId",
                                    period.AttendanceWeekId));
                        }
                        else
                        {
                            q.OrWhere(sq =>
                                sq.Where($"AM.PeriodId", period.PeriodId).Where($"AM.WeekId",
                                    period.AttendanceWeekId));
                        }
                    }

                    return q;
                });

                query.WhereStudentGroup("SGM", studentGroupId, runAsDate);

                return await ExecuteQuery<AttendanceMarkMetadata>(query);
            }

            return Array.Empty<AttendanceMarkMetadata>();
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