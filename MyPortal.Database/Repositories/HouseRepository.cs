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
        public HouseRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "House")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StaffMember), "StaffMember");
            query.SelectAllColumns(typeof(Person), "Person");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("StaffMembers as StaffMember", "StaffMember.Id", "House.HeadId");
            query.LeftJoin("People as Person", "Person.Id", "StaffMember.PersonId");
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