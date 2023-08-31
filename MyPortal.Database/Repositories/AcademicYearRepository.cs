using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AcademicYearRepository : BaseReadWriteRepository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(DbUserWithContext dbUser) : base(dbUser)
        {
            
        }

        public async Task<AcademicYear> GetCurrentAcademicYear()
        {
            var sql = GetDefaultQuery();

            sql.LeftJoin("AcademicTerms AS AT", $"{TblAlias}.Id", "AT.AcademicYearId");

            var dateToday = DateTime.Today;

            sql.Where("AT.StartDate", "<=", dateToday);
            sql.Where("AT.EndDate", ">=", dateToday);

            sql.GroupByEntityColumns(typeof(AcademicYear), TblAlias);

            return await ExecuteQueryFirstOrDefault(sql);
        }

        public async Task<AcademicYear> GetLatestAcademicYear()
        {
            var sql = GetDefaultQuery();

            sql.LeftJoin("AcademicTerms AS AT", $"{TblAlias}.Id", "AT.AcademicYearId");
            sql.OrderByDesc("AT.FirstDate");
            sql.GroupByEntityColumns(typeof(AcademicYear), TblAlias);
            sql.Limit(1);

            return await ExecuteQueryFirstOrDefault(sql);
        }

        public async Task<AcademicYear> GetAcademicYearByWeek(Guid attendanceWeekId)
        {
            var sql = GetDefaultQuery();

            sql.LeftJoin("AcademicTerms as AT", $"{TblAlias}.Id", "AT.AcademicYearId");
            sql.LeftJoin("AcademicWeeks as AW", $"AT.Id", "AW.AcademicTermId");

            sql.Where("AW.Id", attendanceWeekId);

            return await ExecuteQueryFirstOrDefault<AcademicYear>(sql);
        }

        public async Task<IEnumerable<AcademicYear>> GetAllAcademicYears()
        {
            var sql = GetDefaultQuery();

            sql.LeftJoin("AcademicTerms AS AT", $"{TblAlias}.Id", "AT.AcademicYearId");

            var dateToday = DateTime.Today;

            sql.Where("AT.StartDate", "<=", dateToday);

            return await ExecuteQuery(sql);
        }

        public async Task<bool> IsYearLocked(Guid academicYearId)
        {
            var query = GetEmptyQuery();

            query.Select("Locked");

            query.Where($"{TblAlias}.Id", academicYearId);

            return await ExecuteQueryFirstOrDefault<bool>(query);
        }

        public async Task Update(AcademicYear entity)
        {
            var academicYear = await DbUser.Context.AcademicYears.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (academicYear == null)
            {
                throw new EntityNotFoundException("Academic year not found.");
            }

            academicYear.Name = entity.Name;
            academicYear.Locked = entity.Locked;
        }
    }
}
