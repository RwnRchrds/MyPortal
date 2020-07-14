using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class CurriculumBandRepository : BaseReadWriteRepository<CurriculumBand>, ICurriculumBandRepository
    {
        public CurriculumBandRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(AcademicYear));
            query.SelectAll(typeof(CurriculumYearGroup), "CYG");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("AcademicYear", "AcademicYear.Id", "CurriclumBand.AcademicYearId");
            query.LeftJoin("CurriculumYearGroup as CYG", "CYG.Id", "CurriculumBand.CurriclumYearGroupId");
        }

        protected override async Task<IEnumerable<CurriculumBand>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<CurriculumBand, AcademicYear, CurriculumYearGroup, CurriculumBand>(sql.Sql,
                (band, academicYear, yearGroup) =>
                {
                    band.AcademicYear = academicYear;
                    band.CurriculumYearGroup = yearGroup;

                    return band;
                }, sql.NamedBindings);
        }

        public async Task<bool> CheckUniqueCode(Guid academicYearId, string code)
        {
            var query = SelectAllColumns();

            query.Where("CurriculumBand.AcademicYearId", academicYearId);
            query.Where("CurriculumBand.Code", code);

            query.AsCount();

            var result = await ExecuteQueryIntResult(query);

            return result == null || result == 0;
        }
    }
}