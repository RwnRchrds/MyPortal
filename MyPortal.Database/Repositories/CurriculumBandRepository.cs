using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class CurriculumBandRepository : BaseReadWriteRepository<CurriculumBand>, ICurriculumBandRepository
    {
        public CurriculumBandRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("AcademicYears as AY", "AY.Id", $"{TblAlias}.AcademicYearId");
            query.LeftJoin("CurriculumYearGroup as CYG", "CYG.Id", $"{TblAlias}.CurriculumYearGroupId");
            query.LeftJoin("StudentGroup as SG", "SG.Id", $"{TblAlias}.StudentGroupId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AcademicYear), "AY");
            query.SelectAllColumns(typeof(CurriculumYearGroup), "CYG");
            query.SelectAllColumns(typeof(StudentGroup), "SG");

            return query;
        }

        protected override async Task<IEnumerable<CurriculumBand>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var bands = await Transaction.Connection
                .QueryAsync<CurriculumBand, AcademicYear, CurriculumYearGroup, StudentGroup, CurriculumBand>(sql.Sql,
                    (band, academicYear, yearGroup, studentGroup) =>
                    {
                        band.AcademicYear = academicYear;
                        band.CurriculumYearGroup = yearGroup;
                        band.StudentGroup = studentGroup;

                        return band;
                    }, sql.NamedBindings, Transaction);

            return bands;
        }
    }
}