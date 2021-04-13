using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
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
    public class SenProvisionRepository : BaseReadWriteRepository<SenProvision>, ISenProvisionRepository
    {
        public SenProvisionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "SenProvision")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(SenProvisionType), "SenProvisionType");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Students as Student", "Student.Id", "SenProvision.StudentId");
            query.LeftJoin("SenProvisionTypes as SenProvisionType", "SenProvisionType.Id", "SenProvision.ProvisionTypeId");
        }

        protected override async Task<IEnumerable<SenProvision>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<SenProvision, Student, SenProvisionType, SenProvision>(sql.Sql,
                (provision, student, type) =>
                {
                    provision.Student = student;
                    provision.Type = type;

                    return provision;
                }, sql.NamedBindings, Transaction);
        }

        public async Task Update(SenProvision entity)
        {
            var senProvision = await Context.SenProvisions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (senProvision == null)
            {
                throw new EntityNotFoundException("SEN provision not found.");
            }

            senProvision.ProvisionTypeId = entity.ProvisionTypeId;
            senProvision.StartDate = entity.StartDate;
            senProvision.EndDate = entity.EndDate;
            senProvision.Note = entity.Note;
        }
    }
}