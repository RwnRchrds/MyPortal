using System.Collections.Generic;
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
    public class AgentRepository : BaseReadWriteRepository<Agent>, IAgentRepository
    {
        public AgentRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Agencies as AG", "AG.Id", $"{TblAlias}.AgencyId");
            query.LeftJoin("AgentTypes as AT", "AT.Id", $"{TblAlias}.AgentTypeId");
            query.LeftJoin("People as P", "P.Id", $"{TblAlias}.PersonId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Agency), "AG");
            query.SelectAllColumns(typeof(AgentType), "AT");
            query.SelectAllColumns(typeof(Person), "P");

            return query;
        }

        protected override async Task<IEnumerable<Agent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var agents = await DbUser.Transaction.Connection.QueryAsync<Agent, Agency, AgentType, Person, Agent>(
                sql.Sql,
                (agent, agency, type, person) =>
                {
                    agent.Agency = agency;
                    agent.AgentType = type;
                    agent.Person = person;

                    return agent;
                }, sql.NamedBindings, DbUser.Transaction);

            return agents;
        }

        public async Task Update(Agent entity)
        {
            var agent = await DbUser.Context.Agents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (agent == null)
            {
                throw new EntityNotFoundException("Agent not found.");
            }

            agent.AgencyId = entity.AgencyId;
            agent.AgentTypeId = entity.AgentTypeId;
            agent.JobTitle = entity.JobTitle;
        }
    }
}