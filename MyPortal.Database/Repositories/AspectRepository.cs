﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AspectRepository : BaseReadWriteRepository<Aspect>, IAspectRepository
    {
        public AspectRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(AspectType))},
{EntityHelper.GetAllColumns(typeof(GradeSet))}";

        JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspectType]", "[AspectType].[Id]", "[Aspect].[TypeId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[GradeSet]", "[GradeSet].[Id]", "[Aspect].[GradeSetId]")}";
        }

        protected override async Task<IEnumerable<Aspect>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Aspect, AspectType, GradeSet, Aspect>(sql, (aspect, type, gradeSet) =>
            {
                aspect.Type = type;
                aspect.GradeSet = gradeSet;

                return aspect;
            }, param);
        }

        public async Task Update(Aspect entity)
        {
            var aspectInDb = await Context.Aspects.FindAsync(entity.Id);

            aspectInDb.TypeId = entity.TypeId;
            aspectInDb.GradeSetId = entity.GradeSetId;
            aspectInDb.MaxMark = entity.MaxMark;
            aspectInDb.Description = entity.Description;
            aspectInDb.Name = entity.Name;
            aspectInDb.Active = entity.Active;
        }
    }
}