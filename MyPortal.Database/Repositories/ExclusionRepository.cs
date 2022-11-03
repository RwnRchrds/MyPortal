using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExclusionRepository : BaseReadWriteRepository<Exclusion>, IExclusionRepository
    {
        public ExclusionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");
            query.LeftJoin("ExclusionTypes as ET", "ET.Id", $"{TblAlias}.ExclusionTypeId");
            query.LeftJoin("ExclusionReasons as ER", "ER.Id", $"{TblAlias}.ExclusionReasonId");
            query.LeftJoin("ExclusionAppealResults as EAR", "EAR.Id", $"{TblAlias}.AppealResultId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "S");
            query.SelectAllColumns(typeof(ExclusionType), "ET");
            query.SelectAllColumns(typeof(ExclusionReason), "ER");
            query.SelectAllColumns(typeof(ExclusionAppealResult), "EAR");

            return query;
        }

        protected override async Task<IEnumerable<Exclusion>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var exclusions = await Transaction.Connection
                .QueryAsync<Exclusion, Student, ExclusionType, ExclusionReason, ExclusionAppealResult, Exclusion>(
                    sql.Sql,
                    (exclusion, student, type, reason, appealResult) =>
                    {
                        exclusion.Student = student;
                        exclusion.ExclusionType = type;
                        exclusion.ExclusionReason = reason;
                        exclusion.AppealResult = appealResult;

                        return exclusion;
                    }, sql.NamedBindings, Transaction);

            return exclusions;
        }

        public async Task<int> GetCountByStudent(Guid studentId)
        {
            var query = GenerateQuery().AsCount();

            query.Where($"{TblAlias}.StudentId", studentId);

            return await ExecuteQueryIntResult(query) ?? 0;
        }

        public async Task<IEnumerable<Exclusion>> GetByStudent(Guid studentId)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.StudentId", studentId);

            return await ExecuteQuery(query);
        }

        public async Task Update(Exclusion entity)
        {
            var exclusion = await Context.Exclusions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (exclusion == null)
            {
                throw new EntityNotFoundException("Exclusion not found.");
            }

            exclusion.ExclusionTypeId = entity.ExclusionTypeId;
            exclusion.ExclusionReasonId = entity.ExclusionReasonId;
            exclusion.StartDate = entity.StartDate;
            exclusion.EndDate = entity.EndDate;
            exclusion.Comments = entity.Comments;
        }
    }
}
