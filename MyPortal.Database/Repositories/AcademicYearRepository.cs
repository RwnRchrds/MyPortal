using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class AcademicYearRepository : BaseReadWriteRepository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            
        }

        public async Task<AcademicYear> GetCurrent()
        {
            var sql = SelectAllColumns(true);

            SqlHelper.Where(ref sql, "[AcademicYear].[FirstDate] <= @DateToday");
            SqlHelper.Where(ref sql, "[AcademicYear].[LastDate] >= @DateToday");

            return (await ExecuteQuery(sql, new {DateToday = DateTime.Today})).First();
        }

        public async Task<IEnumerable<AcademicYear>> GetAllToDate()
        {
            var sql = SelectAllColumns(true);

            SqlHelper.Where(ref sql, "[AcademicYear].[FirstDate] <= @DateToday");

            return await ExecuteQuery(sql, new {DateToday = DateTime.Today});
        }

        protected override async Task<IEnumerable<AcademicYear>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<AcademicYear>(sql, param);
        }
    }
}
