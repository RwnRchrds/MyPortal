using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyPortal.Database.Constants;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AspectRepository : BaseReadWriteRepository<Aspect>, IAspectRepository
    {
        public AspectRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("AspectTypes as AT", "AT.Id", "Aspect.TypeId");
            query.LeftJoin("GradeSets as GS", "GS.Id", "Aspect.GradeSetId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AspectType), "AT");
            query.SelectAllColumns(typeof(GradeSet), "GS");

            return query;
        }

        protected override async Task<IEnumerable<Aspect>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var aspects = await Transaction.Connection.QueryAsync<Aspect, AspectType, GradeSet, Aspect>(sql.Sql,
                (aspect, type, gradeSet) =>
                {
                    aspect.Type = type;
                    aspect.GradeSet = gradeSet;

                    return aspect;
                }, sql.NamedBindings, Transaction);

            return aspects;
        }

        public async Task Update(Aspect entity)
        {
            var aspect = await Context.Aspects.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (aspect == null)
            {
                throw new EntityNotFoundException("Aspect not found.");
            }

            aspect.ColumnHeading = entity.ColumnHeading;
            aspect.GradeSetId = entity.GradeSetId;
            aspect.TypeId = entity.TypeId;

            if (entity.TypeId == AspectTypes.MarkDecimal || entity.TypeId == AspectTypes.MarkInteger)
            {
                aspect.MinMark = entity.MinMark;
                aspect.MaxMark = entity.MaxMark;   
            }
            else
            {
                aspect.MinMark = null;
                aspect.MaxMark = null;
            }

            aspect.Private = entity.Private;
            aspect.Active = entity.Active;
            aspect.Description = entity.Description;
            aspect.Name = entity.Name;
        }
    }
}