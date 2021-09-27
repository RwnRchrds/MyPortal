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
    public class ReportCardRepository : BaseReadWriteRepository<ReportCard>, IReportCardRepository
    {
        public ReportCardRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Students", "S", "StudentId");
            JoinEntity(query, "IncidentType", "IT", "BehaviourTypeId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "S");
            query.SelectAllColumns(typeof(IncidentType), "IT");

            return query;
        }

        protected override async Task<IEnumerable<ReportCard>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var reportCards = await Transaction.Connection.QueryAsync<ReportCard, Student, IncidentType, ReportCard>(
                sql.Sql,
                (card, student, type) =>
                {
                    card.Student = student;
                    card.BehaviourType = type;

                    return card;
                }, sql.NamedBindings, Transaction);

            return reportCards;
        }

        public async Task Update(ReportCard entity)
        {
            var reportCard = await Context.ReportCards.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (reportCard == null)
            {
                throw new EntityNotFoundException("Report card not found.");
            }

            reportCard.StartDate = entity.StartDate;
            reportCard.EndDate = entity.EndDate;
            reportCard.Comments = entity.Comments;
            reportCard.Active = entity.Active;
        }
    }
}