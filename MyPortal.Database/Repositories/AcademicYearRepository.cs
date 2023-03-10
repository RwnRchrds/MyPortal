using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AcademicYearRepository : BaseReadWriteRepository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task<AcademicYear> GetCurrent()
        {
            var sql = GenerateQuery();

            sql.LeftJoin("AcademicTerms AS AT", $"{TblAlias}.Id", "AT.AcademicYearId");

            var dateToday = DateTime.Today;

            sql.Where("AT.StartDate", "<=", dateToday);
            sql.Where("AT.EndDate", ">=", dateToday);

            sql.GroupByEntityColumns(typeof(AcademicYear), "AcademicYear");

            return await ExecuteQueryFirstOrDefault(sql);
        }

        public async Task<AcademicYear> GetLatest()
        {
            var sql = GenerateQuery();

            sql.LeftJoin("AcademicTerms AS AT", $"{TblAlias}.Id", "AT.AcademicYearId");
            sql.OrderByDesc("AT.FirstDate");
            sql.GroupByEntityColumns(typeof(AcademicYear), TblAlias);
            sql.Limit(1);

            return await ExecuteQueryFirstOrDefault(sql);
        }

        public async Task<IEnumerable<AcademicYear>> GetAllToDate()
        {
            var sql = GenerateQuery();

            sql.LeftJoin("AcademicTerms AS AT", $"{TblAlias}.Id", "AT.AcademicYearId");

            var dateToday = DateTime.Today;

            sql.Where("AT.StartDate", "<=", dateToday);

            return await ExecuteQuery(sql);
        }

        public async Task<bool> IsLocked(Guid academicYearId)
        {
            var query = new Query(TblName);

            query.Select("Locked");

            query.Where($"{TblAlias}.Id", academicYearId);

            return await ExecuteQueryFirstOrDefault<bool>(query);
        }

        public async Task Update(AcademicYear entity)
        {
            var academicYear = await Context.AcademicYears.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (academicYear == null)
            {
                throw new EntityNotFoundException("Academic year not found.");
            }

            academicYear.Name = entity.Name;
            academicYear.Locked = entity.Locked;
        }
    }
}
