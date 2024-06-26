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
    public class MedicalEventRepository : BaseReadWriteRepository<MedicalEvent>, IMedicalEventRepository
    {
        public MedicalEventRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Users as U", "U.Id", $"{TblAlias}.CreatedById");
            query.LeftJoin("People as P", "P.Id", $"{TblAlias}.PersonId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(User), "U");
            query.SelectAllColumns(typeof(Person), "P");

            return query;
        }

        protected override async Task<IEnumerable<MedicalEvent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var medicalEvents =
                await DbUser.Transaction.Connection.QueryAsync<MedicalEvent, User, Person, MedicalEvent>(
                    sql.Sql,
                    (medicalEvent, user, student) =>
                    {
                        medicalEvent.CreatedBy = user;
                        medicalEvent.Person = student;

                        return medicalEvent;
                    }, sql.NamedBindings, DbUser.Transaction);

            return medicalEvents;
        }

        public async Task Update(MedicalEvent entity)
        {
            var medicalEvent = await DbUser.Context.MedicalEvents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (medicalEvent == null)
            {
                throw new EntityNotFoundException("Medical event not found.");
            }

            medicalEvent.Date = entity.Date;
            medicalEvent.Note = entity.Note;
        }
    }
}