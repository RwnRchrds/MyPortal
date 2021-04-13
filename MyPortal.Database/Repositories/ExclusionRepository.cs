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
        public ExclusionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "E")
        {
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "S");
            query.SelectAllColumns(typeof(Person), "P");
            query.SelectAllColumns(typeof(ExclusionType), "ET");
            query.SelectAllColumns(typeof(ExclusionReason), "ER");
            query.SelectAllColumns(typeof(ExclusionAppealResult), "EAR");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Students as S", "S.Id", "E.StudentId");
            query.LeftJoin("People as P", "P.Id", "S.PersonId");
            query.LeftJoin("ExclusionTypes as ET", "ET.Id", "E.ExclusionTypeId");
            query.LeftJoin("ExclusionReasons as ER", "ER.Id", "E.ExclusionReasonId");
            query.LeftJoin("ExclusionAppealResults as EAR", "EAR.Id", "E.AppealResultId");
        }

        protected override async Task<IEnumerable<Exclusion>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);
            return await Transaction.Connection
                .QueryAsync<Exclusion, Student, Person, ExclusionType, ExclusionReason, ExclusionAppealResult, Exclusion
                >(sql.Sql,
                    (e, s, p, et, er, ear) =>
                    {
                        e.Student = s;
                        s.Person = p;
                        e.ExclusionReason = er;
                        e.ExclusionType = et;
                        e.AppealResult = ear;

                        return e;
                    }, sql.NamedBindings, Transaction);
        }

        public async Task<int> GetCountByStudent(Guid studentId)
        {
            var query = GenerateQuery(false, false).AsCount();

            query.Where("E.StudentId", studentId);

            return await ExecuteQueryIntResult(query) ?? 0;
        }

        public async Task<IEnumerable<Exclusion>> GetByStudent(Guid studentId)
        {
            var query = GenerateQuery();

            query.Where("E.StudentId", studentId);

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
