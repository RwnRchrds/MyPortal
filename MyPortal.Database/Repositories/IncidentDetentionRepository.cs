﻿using System;
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
    public class IncidentDetentionRepository : BaseReadWriteRepository<StudentIncidentDetention>, IIncidentDetentionRepository
    {
        public IncidentDetentionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("StudentIncidents as SI", "SI.Id", $"{TblAlias}.StudentIncidentId");
            query.LeftJoin("Detentions as D", "D.Id", $"{TblAlias}.DetentionId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StudentIncident), "SI");
            query.SelectAllColumns(typeof(Detention), "D");

            return query;
        }

        protected override async Task<IEnumerable<StudentIncidentDetention>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var incidentDetentions =
                await Transaction.Connection.QueryAsync<StudentIncidentDetention, StudentIncident, Detention, StudentIncidentDetention>(
                    sql.Sql,
                    (incidentDetention, incident, detention) =>
                    {
                        incidentDetention.StudentIncident = incident;
                        incidentDetention.Detention = detention;

                        return incidentDetention;
                    }, sql.NamedBindings, Transaction);

            return incidentDetentions;
        }

        public async Task<StudentIncidentDetention> GetSpecific(Guid detentionId, Guid studentIncidentId)
        {
            var query = GenerateQuery();

            query.Where("D.Id", detentionId);
            query.Where("SI.Id", studentIncidentId);

            return await ExecuteQueryFirstOrDefault(query);
        }

        public async Task<IEnumerable<StudentIncidentDetention>> GetByStudentIncident(Guid studentIncidentId)
        {
            var query = GenerateQuery();

            query.Where("SI.Id", studentIncidentId);

            return await ExecuteQuery(query);
        }
    }
}