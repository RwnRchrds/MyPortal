using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class CurriculumBlockRepository : BaseReadWriteRepository<CurriculumBlock>, ICurriculumBlockRepository
    {
        public CurriculumBlockRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context, "Block")
        {
            
        }

        public async Task<IEnumerable<CurriculumBlock>> GetByCurriculumBand(Guid bandId)
        {
            var query = SelectAllColumns();

            query.LeftJoin("dbo.CurriculumBandBlock as BandBlock", "BandBlock.BlockId", "Block.Id");

            query.Where("BandBlock.BandId", bandId);

            return await ExecuteQuery(query);
        }

        public async Task<Guid?> GetAcademicYearId(Guid blockId)
        {
            var query = new Query(TblName);

            query.Select("Band.AcademicYearId");

            query.LeftJoin("dbo.CurriculumBandBlockAssignment as Assignment", "Assignment.BlockId", "Block.Id");
            query.LeftJoin("dbo.CurriculumBand as Band", "Band.Id", "Assignment.BandId");

            query.Where("Block.Id", blockId);

            var sql = Compiler.Compile(query);

            return (await Connection.QueryAsync<Guid>(sql.Sql, sql.NamedBindings)).FirstOrDefault();
        }

        public async Task<bool> CheckUniqueCode(Guid academicYearId, string code)
        {
            var query = SelectAllColumns();

            query.LeftJoin("dbo.CurriculumBandBlockAssignment as Assignment", "Assignment.BlockId", "Block.Id");
            query.LeftJoin("dbo.CurriculumBand as Band", "Band.Id", "Assignment.BandId");

            query.Where("Band.AcademicYearId", academicYearId);
            query.Where("Block.Code", code);

            query.AsCount();

            var result = await ExecuteQueryIntResult(query);

            return result == null || result == 0;
        }
    }
}