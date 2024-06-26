﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class NextOfKinRepository : BaseReadWriteRepository<NextOfKin>, INextOfKinRepository
    {
        public NextOfKinRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("StaffMember as SM", "SM.Id", $"{TblAlias}.StaffMemberId");
            query.LeftJoin("Person as P", "P.Id", $"{TblAlias}.PersonId");
            query.LeftJoin("NextOfKinRelationshipTypes as NKRT", "NKRT.Id", $"{TblAlias}.RelationshipTypeId");

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

            var nextOfKin = await DbUser.Transaction.Connection
                .QueryAsync<NextOfKin, StaffMember, Person, NextOfKinRelationshipType, NextOfKin>(sql.Sql,
                    (kin, staff, person, relationshipType) =>
                    {
                        kin.StaffMember = staff;
                        kin.NextOfKinPerson = person;
                        kin.RelationshipType = relationshipType;

                        return kin;
                    }, sql.NamedBindings, DbUser.Transaction);

            return nextOfKin;
        }

        public async Task Update(NextOfKin entity)
        {
            var nextOfKin = await DbUser.Context.NextOfKin.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (nextOfKin == null)
            {
                throw new EntityNotFoundException("Next of kin not found.");
            }

            nextOfKin.RelationshipTypeId = entity.RelationshipTypeId;
        }
    }
}