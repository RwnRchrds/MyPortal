using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class IncidentDetentionRepository : BaseReadWriteRepository<IncidentDetention>, IIncidentDetentionRepository
    {
        public IncidentDetentionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Incidents", "I", "IncidentId");
            JoinEntity(query, "Detentions", "D", "DetentionId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Incident), "I");
            query.SelectAllColumns(typeof(Detention), "D");

            return query;
        }

        protected override async Task<IEnumerable<IncidentDetention>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var incidentDetentions =
                await Transaction.Connection.QueryAsync<IncidentDetention, Incident, Detention, IncidentDetention>(
                    sql.Sql,
                    (incidentDetention, incident, detention) =>
                    {
                        incidentDetention.Incident = incident;
                        incidentDetention.Detention = detention;

                        return incidentDetention;
                    }, sql.NamedBindings, Transaction);

            return incidentDetentions;
        }

        public async Task<IncidentDetention> Get(Guid detentionId, Guid studentId)
        {
            var query = GenerateQuery();

            query.Where("Detention.Id", detentionId);
            query.Where("Student.Id", studentId);

            return await ExecuteQueryFirstOrDefault(query);
        }
    }
}