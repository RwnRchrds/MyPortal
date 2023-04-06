using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class CurriculumBlockRepository : BaseReadWriteRepository<CurriculumBlock>, ICurriculumBlockRepository
    {
        public CurriculumBlockRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task<IEnumerable<CurriculumBlock>> GetByCurriculumBand(Guid bandId)
        {
            var query = GenerateQuery();

            query.LeftJoin("CurriculumBandBlock as CBB", "CBB.BlockId", $"{TblAlias}.Id");

            query.Where("CBB.BandId", bandId);

            return await ExecuteQuery(query);
        }

        public async Task<Guid?> GetAcademicYearId(Guid blockId)
        {
            var query = new Query(TblName);

            query.Select("Band.AcademicYearId");

            query.LeftJoin("CurriculumBandBlockAssignments as Assignment", "Assignment.BlockId", $"{TblAlias}.Id");
            query.LeftJoin("CurriculumBands as Band", "Band.Id", "Assignment.BandId");

            query.Where("Block.Id", blockId);

            var sql = Compiler.Compile(query);

            return (await Transaction.Connection.QueryAsync<Guid>(sql.Sql, sql.NamedBindings, Transaction)).FirstOrDefault();
        }

        public async Task<bool> CheckUniqueCode(Guid academicYearId, string code)
        {
            var query = GenerateQuery();

            query.LeftJoin("CurriculumBandBlockAssignment as Assignment", "Assignment.BlockId", $"{TblAlias}.Id");
            query.LeftJoin("CurriculumBand as Band", "Band.Id", "Assignment.BandId");

            query.Where("Band.AcademicYearId", academicYearId);
            query.Where("Block.Code", code);

            query.AsCount();

            var result = await ExecuteQueryIntResult(query);

            return result == null || result == 0;
        }

        public async Task Update(CurriculumBlock entity)
        {
            var block = await Context.CurriculumBlocks.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (block == null)
            {
                throw new EntityNotFoundException("Block not found.");
            }

            block.Code = entity.Code;
            block.Description = entity.Description;
        }
    }
}