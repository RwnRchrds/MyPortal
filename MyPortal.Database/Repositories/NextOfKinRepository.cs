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
    public class NextOfKinRepository : BaseReadWriteRepository<NextOfKin>, INextOfKinRepository
    {
        public NextOfKinRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "StaffMember", "SM", "StaffMemberId");
            JoinEntity(query, "Person", "P", "PersonId");
            JoinEntity(query, "NextOfKinRelationshipTypes", "NKRT", "RelationshipTypeId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StaffMember), "SM");
            query.SelectAllColumns(typeof(Person), "P");
            query.SelectAllColumns(typeof(NextOfKinRelationshipType), "NKRT");

            return query;
        }

        protected override async Task<IEnumerable<NextOfKin>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var nextOfKin = await Transaction.Connection
                .QueryAsync<NextOfKin, StaffMember, Person, NextOfKinRelationshipType, NextOfKin>(sql.Sql,
                    (kin, staff, person, relationshipType) =>
                    {
                        kin.StaffMember = staff;
                        kin.NextOfKinPerson = person;
                        kin.RelationshipType = relationshipType;

                        return kin;
                    }, sql.NamedBindings, Transaction);

            return nextOfKin;
        }

        public async Task Update(NextOfKin entity)
        {
            var nextOfKin = await Context.NextOfKin.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (nextOfKin == null)
            {
                throw new EntityNotFoundException("Next of kin not found.");
            }

            nextOfKin.RelationshipTypeId = entity.RelationshipTypeId;
        }
    }
}