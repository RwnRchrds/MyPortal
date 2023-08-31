using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class StudentDetentionRepository : BaseReadWriteRepository<StudentDetention>,
        IStudentDetentionRepository
    {
        public StudentDetentionRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");
            query.LeftJoin("StudentIncidents as SI", "SI.Id", $"{TblAlias}.LinkedIncidentId");
            query.LeftJoin("Detentions as D", "D.Id", $"{TblAlias}.DetentionId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StudentIncident), "SI");
            query.SelectAllColumns(typeof(Detention), "D");

            return query;
        }

        protected override async Task<IEnumerable<StudentDetention>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var incidentDetentions =
                await DbUser.Transaction.Connection
                    .QueryAsync<StudentDetention, Student, StudentIncident, Detention, StudentDetention>(
                        sql.Sql,
                        (studentDetention, student, incident, detention) =>
                        {
                            studentDetention.Student = student;
                            studentDetention.LinkedIncident = incident;
                            studentDetention.Detention = detention;

                            return studentDetention;
                        }, sql.NamedBindings, DbUser.Transaction);

            return incidentDetentions;
        }

        public async Task<StudentDetention> GetSpecific(Guid detentionId, Guid studentId)
        {
            var query = GetDefaultQuery();

            query.Where("D.Id", detentionId);
            query.Where("S.Id", studentId);

            return await ExecuteQueryFirstOrDefault(query);
        }

        public async Task<IEnumerable<StudentDetention>> GetByStudentIncident(Guid studentIncidentId)
        {
            var query = GetDefaultQuery();

            query.Where("SI.Id", studentIncidentId);

            return await ExecuteQuery(query);
        }
    }
}