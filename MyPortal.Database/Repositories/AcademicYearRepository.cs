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
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class AcademicYearRepository : BaseReadWriteRepository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            
        }

        public async Task<AcademicYear> GetCurrent()
        {
            var sql = SelectAllColumns();

            var dateToday = DateTime.Today;

            sql.Where("AcademicYear.FirstDate", "<=", dateToday);
            sql.Where("AcademicYear.LastDate", ">=", dateToday);

            return (await ExecuteQuery(sql)).First();
        }

        public async Task<IEnumerable<AcademicYear>> GetAllToDate()
        {
            var sql = SelectAllColumns();

            var dateToday = DateTime.Today;

            sql.Where("Academic.FirstDate", "<=", dateToday);

            return await ExecuteQuery(sql);
        }
    }
}
