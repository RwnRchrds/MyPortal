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
    public class MedicalEventRepository : BaseReadWriteRepository<MedicalEvent>, IMedicalEventRepository
    {
        public MedicalEventRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Users", "U", "CreatedById");
            JoinEntity(query, "Students", "S", "StudentId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(User), "U");
            query.SelectAllColumns(typeof(Student), "S");

            return query;
        }

        protected override async Task<IEnumerable<MedicalEvent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var medicalEvents = await Transaction.Connection.QueryAsync<MedicalEvent, User, Student, MedicalEvent>(
                sql.Sql,
                (medicalEvent, user, student) =>
                {
                    medicalEvent.CreatedBy = user;
                    medicalEvent.Student = student;

                    return medicalEvent;
                }, sql.NamedBindings, Transaction);

            return medicalEvents;
        }

        public async Task Update(MedicalEvent entity)
        {
            var medicalEvent = await Context.MedicalEvents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (medicalEvent == null)
            {
                throw new EntityNotFoundException("Medical event not found.");
            }

            medicalEvent.Date = entity.Date;
            medicalEvent.Note = entity.Note;
        }
    }
}