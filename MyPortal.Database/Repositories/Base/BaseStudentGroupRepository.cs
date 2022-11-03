using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories.Base
{
    public abstract class BaseStudentGroupRepository<TEntity> : BaseReadWriteRepository<TEntity>, IBaseStudentGroupRepository<TEntity> where TEntity : class, IStudentGroupEntity
    {
        protected BaseStudentGroupRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }
        
        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("StudentGroups as SG", "SG.Id", $"{TblAlias}.StudentGroupId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StudentGroup), "SG");

            return query;
        }

        protected override async Task<IEnumerable<TEntity>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var houses = await Transaction.Connection.QueryAsync<TEntity, StudentGroup, TEntity>(sql.Sql, (entity, group) =>
            {
                entity.StudentGroup = group;

                return entity;
            }, sql.NamedBindings, Transaction);

            return houses;
        }

        public async Task<TEntity> GetByStudent(Guid studentId)
        {
            var query = GenerateQuery();

            query.LeftJoin("StudentGroupMemberships as SGM", "SGM.StudentGroupId", "SG.Id");

            query.Where("SGM.StudentId", studentId);

            return await ExecuteQueryFirstOrDefault(query);
        }
    }
}