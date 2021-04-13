using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
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
        public AcademicTermRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "AT")
        {
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AcademicYear), "AY");
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("AcademicYears AS AY", "AY.Id", "AT.AcademicYearId");
        }

        public async Task Update(AcademicTerm entity)
        {
            var term = await Context.AcademicTerms.FirstOrDefaultAsync(t => t.Id == entity.Id);

            if (term == null)
            {
                throw new EntityNotFoundException("Academic term not found.");
            }

            term.Name = entity.Name;
        }

        public async Task<IEnumerable<AcademicTerm>> GetByAcademicYear(Guid academicYearId)
        {
            var query = GenerateQuery();

            query.Where("AY.AcademicYearId", academicYearId);

            return await ExecuteQuery(query);
        }
    }
}
