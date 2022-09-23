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
    public class AgencyRepository : BaseReadWriteRepository<Agency>, IAgencyRepository
    {
        public AgencyRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "AgencyTypes", "AT", "TypeId");
            JoinEntity(query, "Addresses", "AD", "AddressId");
            JoinEntity(query, "Directory", "D", "DirectoryId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AgencyType), "AT");
            query.SelectAllColumns(typeof(Address), "AD");
            query.SelectAllColumns(typeof(Directory), "D");

            return query;
        }

        protected override async Task<IEnumerable<Agency>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var agencies = await Transaction.Connection.QueryAsync<Agency, AgencyType, Directory, Agency>(
                sql.Sql,
                (agency, type, dir) =>
                {
                    agency.AgencyType = type;
                    agency.Directory = dir;

                    return agency;
                }, sql.NamedBindings, Transaction);

            return agencies;
        }

        public async Task Update(Agency entity)
        {
            var agency = await Context.Agencies.FirstOrDefaultAsync(a => a.Id == entity.Id);

            if (agency == null)
            {
                throw new EntityNotFoundException("Agency not found.");
            }

            agency.TypeId = entity.TypeId;
            agency.Name = entity.Name;
            agency.Website = entity.Website;
        }
    }
}