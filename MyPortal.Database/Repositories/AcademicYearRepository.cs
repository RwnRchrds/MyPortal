﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class AcademicYearRepository : BaseReadWriteRepository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "AcademicYear")
        {
            
        }

        public async Task<AcademicYear> GetCurrent()
        {
            var sql = GenerateQuery();

            sql.LeftJoin("AcademicTerms AS AcademicTerm", "AcademicYear.Id", "AcademicTerm.AcademicYearId");

            var dateToday = DateTime.Today;

            sql.Where("AcademicTerm.StartDate", "<=", dateToday);
            sql.Where("AcademicTerm.EndDate", ">=", dateToday);

            sql.GroupByEntityColumns(typeof(AcademicYear), "AcademicYear");

            return await ExecuteQueryFirstOrDefault(sql);
        }

        public async Task<AcademicYear> GetLatest()
        {
            var sql = GenerateQuery();

            sql.LeftJoin("AcademicTerms AS AcademicTerm", "AcademicYear.Id", "AcademicTerm.AcademicYearId");
            sql.OrderByDesc("AcademicTerm.FirstDate");
            sql.GroupByEntityColumns(typeof(AcademicYear), "AcademicYear");
            sql.Limit(1);

            return await ExecuteQueryFirstOrDefault(sql);
        }

        public async Task<IEnumerable<AcademicYear>> GetAllToDate()
        {
            var sql = GenerateQuery();

            var dateToday = DateTime.Today;

            sql.Where("Academic.FirstDate", "<=", dateToday);

            return await ExecuteQuery(sql);
        }

        public async Task<bool> IsLocked(Guid academicYearId)
        {
            var query = new Query(TblName);

            query.Select("Locked");

            query.Where("AcademicYear.Id", academicYearId);

            return await ExecuteQueryFirstOrDefault<bool>(query);
        }
    }
}
