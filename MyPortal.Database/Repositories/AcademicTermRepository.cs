using System;
using System.Collections.Generic;
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
    public class AcademicTermRepository : BaseReadWriteRepository<AcademicTerm>, IAcademicTermRepository
    {
        public AcademicTermRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("AcademicYears as AY", "AY.Id", $"{TblAlias}.AcademicYearId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AcademicYear), "AY");

            return query;
        }

        protected override async Task<IEnumerable<AcademicTerm>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var terms = await Transaction.Connection.QueryAsync<AcademicTerm, AcademicYear, AcademicTerm>(sql.Sql,
                (term, year) =>
                {
                    term.AcademicYear = year;

                    return term;
                }, sql.NamedBindings, Transaction);

            return terms;
        }

        public async Task Update(AcademicTerm entity)
        {
            var term = await Context.AcademicTerms.FirstOrDefaultAsync(t => t.Id == entity.Id);

            if (term == null)
            {
                throw new EntityNotFoundException("Academic term not found.");
            }

            term.AcademicYearId = entity.AcademicYearId;
            term.Name = entity.Name;
            term.StartDate = entity.StartDate;
            term.EndDate = entity.EndDate;
        }

        public async Task<IEnumerable<AcademicTerm>> GetByAcademicYear(Guid academicYearId)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.AcademicYearId", academicYearId);

            return await ExecuteQuery(query);
        }
    }
}
