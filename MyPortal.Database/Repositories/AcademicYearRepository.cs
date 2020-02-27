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
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AcademicYearRepository : BaseReadWriteRepository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            
        }

        public async Task Update(AcademicYear entity)
        {
            var academicYearInDb = await GetByIdWithTracking(entity.Id);

            if (academicYearInDb == null)
            {
                throw new Exception("Academic year not found.");
            }

            academicYearInDb.Name = entity.Name;
        }

        public async Task<AcademicYear> GetCurrent()
        {
            var sql = SelectAllColumns();

            SqlHelper.Where(ref sql, "[AcademicYear].[FirstDate] <= @DateToday");
            SqlHelper.Where(ref sql, "[AcademicYear].[LastDate] >= @DateToday");

            return (await ExecuteQuery(sql, new {DateToday = DateTime.Today})).First();
        }

        protected override async Task<IEnumerable<AcademicYear>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<AcademicYear>(sql, param);
        }
    }
}
