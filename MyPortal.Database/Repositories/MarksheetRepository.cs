using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Enums;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Assessment;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class MarksheetRepository : BaseReadWriteRepository<Marksheet>, IMarksheetRepository
    {
        public MarksheetRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        private Query WithMarksheets(Query query, string alias)
        {
            var cteQuery = new Query("Marksheets as M").Distinct();

            cteQuery.Select("M.Id as MarksheetId", "M.MarksheetTemplateId as TemplateId",
                "M.StudentGroupId as StudentGroupId", "M.Completed as Completed", "SG.Code as StudentGroupCode",
                "MT.Name as TemplateName", "SM.Id as OwnerId", "MSN.Name as OwnerName");

            cteQuery.LeftJoin("MarksheetTemplates as MT", "MT.Id", "M.MarksheetTemplateId");
            cteQuery.LeftJoin("StudentGroups as SG", "SG.Id", "M.StudentGroupId");
            cteQuery.LeftJoin("StudentGroupSupervisors as MS", "SGS.Id", "SG.MainSupervisorId");
            cteQuery.LeftJoin("StaffMembers as SM", "SM.Id", "MS.SupervisorId");
            cteQuery.ApplyName("MSN", "SM.PersonId", NameFormat.FullNameAbbreviated);

            return query.With(alias, cteQuery);
        }

        private Query GenerateDetailsQuery(string alias = "MM")
        {
            var query = new Query();

            WithMarksheets(query, "MarksheetsMetadataCte");

            query.Select($"{alias}.MarksheetId", $"{alias}.TemplateId",
                $"{alias}.StudentGroupId", $"{alias}.Completed", $"{alias}.StudentGroupCode",
                $"{alias}.TemplateName", $"{alias}.OwnerId", $"{alias}.OwnerName");

            query.From($"MarksheetsMetadataCte as {alias}");

            return query;
        }

        public async Task<MarksheetDetailModel> GetMarksheetDetails(Guid marksheetId)
        {
            var query = GenerateDetailsQuery();

            query.Where("MM.MarksheetId", marksheetId);

            return await ExecuteQueryFirstOrDefault<MarksheetDetailModel>(query);
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("StudentGroups as SG", "SG.Id", $"{TblAlias}.StudentGroupId");
            query.LeftJoin("MarksheetTemplates as MT", "MT.Id", $"{TblAlias}.MarksheetTemplateId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StudentGroup), "SG");
            query.SelectAllColumns(typeof(MarksheetTemplate), "MT");

            return query;
        }

        protected override async Task<IEnumerable<Marksheet>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var templateGroups =
                await DbUser.Transaction.Connection
                    .QueryAsync<Marksheet, StudentGroup, MarksheetTemplate, Marksheet>(
                        sql.Sql,
                        (templateGroup, studentGroup, template) =>
                        {
                            templateGroup.StudentGroup = studentGroup;
                            templateGroup.Template = template;

                            return templateGroup;
                        }, sql.NamedBindings, DbUser.Transaction);

            return templateGroups;
        }
    }
}