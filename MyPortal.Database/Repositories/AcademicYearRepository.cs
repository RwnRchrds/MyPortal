﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AcademicYearRepository : BaseReadWriteRepository<AcademicYear>, IAcademicYearRepository
    {
        private readonly string TblName = @"[dbo].[AcademicYear] AS [AcademicYear]";

        internal static readonly string AllColumns = EntityHelper.GetAllColumns(typeof(AcademicYear), "AcademicYear");

        public AcademicYearRepository(IDbConnection connection) : base(connection)
        {
            
        }

        public Task<IEnumerable<AcademicYear>> GetAll()
        {
            var sql = $"SELECT {AllColumns} FROM {TblName}";

            return Connection.QueryAsync<AcademicYear>(sql);
        }

        public async Task<AcademicYear> GetById(Guid id)
        {
            var sql = $"SELECT {AllColumns} FROM {TblName} WHERE [AcademicYear].[Id] = @AcademicYearId";

            return await Connection.QuerySingleOrDefaultAsync<AcademicYear>(sql, new {AcademicYearId = id});
        }

        public async Task Update(AcademicYear entity)
        {
            var academicYearInDb = await Context.AcademicYears.FindAsync(entity.Id);

            if (academicYearInDb == null)
            {
                throw new Exception("Academic year not found.");
            }

            academicYearInDb.Name = entity.Name;
        }

        public async Task<AcademicYear> GetCurrent()
        {
            var sql =
                $"SELECT {AllColumns} FROM {TblName} WHERE [AcademicYear].[FirstDate] <= DateToday AND [AcademicYear].[LastDate] >= DateToday";

            return await Connection.QueryFirstOrDefaultAsync<AcademicYear>(sql, new {DateToday = DateTime.Today});
        }

        protected override async Task<IEnumerable<AcademicYear>> ExecuteQuery(string sql, object param = null)
        {
            throw new NotImplementedException();
        }
    }
}