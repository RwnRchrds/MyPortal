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
    public class BuildingFloorRepository : BaseReadWriteRepository<BuildingFloor>, IBuildingFloorRepository
    {
        public BuildingFloorRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Buildings as B", "B.Id", $"{TblAlias}.BuildingId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Building), "B");

            return query;
        }

        protected override async Task<IEnumerable<BuildingFloor>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var buildingFloors = await Transaction.Connection.QueryAsync<BuildingFloor, Building, BuildingFloor>(
                sql.Sql,
                (floor, building) =>
                {
                    floor.Building = building;

                    return floor;
                }, sql.NamedBindings, Transaction);

            return buildingFloors;
        }

        public async Task Update(BuildingFloor entity)
        {
            var floor = await Context.BuildingFloors.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (floor == null)
            {
                throw new EntityNotFoundException("Floor not found.");
            }

            floor.Description = entity.Description;
            floor.Active = entity.Active;
        }
    }
}