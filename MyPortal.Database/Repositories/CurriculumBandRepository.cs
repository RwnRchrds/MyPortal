using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class CurriculumBandRepository : BaseReadWriteRepository<CurriculumBand>, ICurriculumBandRepository
    {
        public CurriculumBandRepository(DbUserWithContext dbUser) : base(dbUser)
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

            var bands = await DbUser.Transaction.Connection
                .QueryAsync<CurriculumBand, AcademicYear, CurriculumYearGroup, StudentGroup, CurriculumBand>(sql.Sql,
                    (band, academicYear, yearGroup, studentGroup) =>
                    {
                        band.AcademicYear = academicYear;
                        band.CurriculumYearGroup = yearGroup;
                        band.StudentGroup = studentGroup;

                        return band;
                    }, sql.NamedBindings, DbUser.Transaction);

            return bands;
        }

        public async Task<IEnumerable<CurriculumBand>> GetCurriculumBandsByYearGroup(Guid yearGroupId)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.CurriculumYearGroupId", yearGroupId);

            return await ExecuteQuery(query);
        }

        public async Task Update(CurriculumBand entity)
        {
            var band = await DbUser.Context.CurriculumBands.FirstOrDefaultAsync(b => b.Id == entity.Id);

            if (band == null)
            {
                throw new EntityNotFoundException("Curriculum band not found.");
            }

            band.AcademicYearId = entity.AcademicYearId;
            band.CurriculumYearGroupId = entity.CurriculumYearGroupId;
            band.StudentGroupId = entity.StudentGroupId;
        }
    }
}