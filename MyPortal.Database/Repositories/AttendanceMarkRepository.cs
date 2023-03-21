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

        public async Task<IEnumerable<PossibleAttendanceMark>> GetPossibleMarksByStudentGroup(Guid studentGroupId,
            IEnumerable<AttendancePeriodInstance> attendancePeriods)
        {
            var orderedPeriods = attendancePeriods.OrderBy(p => p.ActualStartTime).ToArray();

            var dateFrom = orderedPeriods.Select(x => x.ActualStartTime).FirstOrDefault();

            var dateTo = orderedPeriods.Select(x => x.ActualStartTime).LastOrDefault();
            
            var query = new Query("Students as S");

            query.Select("SGM.StudentId as StudentId", "AW.Id as AttendanceWeekId", "AP.Id as PeriodId");
            query.SelectRaw("CASE WHEN AM.Id IS NOT NULL THEN 1 ELSE 0 END [Exists]");

            query.CrossJoin("AttendancePeriods as AP");
            query.LeftJoin("StudentGroupMemberships as SGM", "SGM.StudentId", "S.Id");
            query.LeftJoin("StudentGroups as SG", "SG.Id", "SGM.StudentGroupId");
            query.LeftJoin("CurriculumGroups as CG", "CG.StudentGroupId", "SG.Id");
            query.LeftJoin("Classes as C", "C.CurriculumGroupId", "CG.Id");
            query.LeftJoin("Sessions as SE", "SE.ClassId", "C.Id");
            query.LeftJoin("AttendanceWeekPatterns as AWP", "AWP.Id", "AP.WeekPatternId");
            query.LeftJoin("AttendanceWeeks as AW", "AW.WeekPatternId", "AWP.Id");
            query.LeftJoin("AttendanceMarks as AM",
                j => j.On("AM.StudentId", "S.Id").On("AM.WeekId", "AW.Id").On("AM.PeriodId", "AP.Id"));

            query.GroupBy("SGM.StudentId", "AW.Id", "AP.Id");
            query.GroupByRaw("CASE WHEN AM.Id IS NOT NULL THEN 1 ELSE 0 END");
            
            query.WhereStudentGroupMembershipValid("SGM", dateFrom, dateTo);
            query.Where(q =>
            {
                for (int i = 0; i < orderedPeriods.Length; i++)
                {
                    var period = orderedPeriods[i];

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

            query.Where("SG.Id", studentGroupId);

            return await ExecuteQuery<PossibleAttendanceMark>(query);
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

        public async Task<IEnumerable<AttendanceMarkDetailModel>> GetRegisterMarks(Guid studentGroupId,
            AttendancePeriodInstance[] attendancePeriods)
        {
            if (attendancePeriods.Any())
            {
                var orderedPeriods = attendancePeriods.OrderBy(p => p.ActualStartTime).ToArray();

                var dateFrom = orderedPeriods.Select(x => x.ActualStartTime).FirstOrDefault();

                var dateTo = orderedPeriods.Select(x => x.ActualStartTime).LastOrDefault();

                var query = GenerateEmptyQuery(typeof(AttendanceMark), "AM");

                JoinRelated(query);

                query.LeftJoin("People as P", "P.Id", $"S.PersonId");

                query.Select("AM.Id as AttendanceMarkId", "AM.StudentId as StudentId", "AM.WeekId as WeekId",
                    "AM.PeriodId as PeriodId", "AM.CodeId as CodeId", "AM.CreatedById as CreatedById",
                    "AM.Comments as Comments", "AM.MinutesLate as MinutesLate");

                query.Select("CONCAT(P.LastName, ', ', P.FirstName) as StudentName");

                query.JoinStudentGroupsByStudent("S", "SGM");

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

                query.Where("SGM.StudentGroupId", studentGroupId);
                query.WhereStudentGroupMembershipValid("SGM", dateFrom, dateTo);

                return await ExecuteQuery<AttendanceMarkDetailModel>(query);
            }

            return Array.Empty<AttendanceMarkDetailModel>();
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