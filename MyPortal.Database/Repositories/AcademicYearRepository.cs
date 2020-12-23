using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class AcademicYearRepository : BaseReadWriteRepository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(ApplicationDbContext context) : base(context, "AcademicYear")
        {
            
        }

        public async Task<AcademicYear> GetCurrent()
        {
            var sql = GenerateQuery();

            var dateToday = DateTime.Today;

            sql.Where("AcademicYear.FirstDate", "<=", dateToday);
            sql.Where("AcademicYear.LastDate", ">=", dateToday);

            return (await ExecuteQuery(sql)).First();
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
