using System;
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
    public class HouseRepository : BaseReadWriteRepository<House>, IHouseRepository
    {
        public HouseRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
           
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "StudentGroups", "SG", "StudentGroupId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StudentGroup), "SG");

            return query;
        }

        protected override async Task<IEnumerable<House>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var houses = await Transaction.Connection.QueryAsync<House, StudentGroup, House>(sql.Sql, (house, group) =>
            {
                house.StudentGroup = group;

                return house;
            }, sql.NamedBindings, Transaction);

            return houses;
        }

        public async Task Update(House entity)
        {
            var house = await Context.Houses.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (house == null)
            {
                throw new EntityNotFoundException("House not found.");
            }
            
            house.ColourCode = entity.ColourCode;
        }
    }
}