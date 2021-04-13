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
        public MedicalEventRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "MedicalEvent")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(User), "User");
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Students as Student", "Student.Id", "MedicalEvent.StudentId");
            query.LeftJoin("Users as User", "User.Id", "MedicalEvent.RecordedById");
        }

        protected override async Task<IEnumerable<MedicalEvent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<MedicalEvent, Student, User, MedicalEvent>(sql.Sql,
                (medEvent, student, recorder) =>
                {
                    medEvent.Student = student;
                    medEvent.RecordedBy = recorder;

                    return medEvent;
                }, sql.NamedBindings, Transaction);
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