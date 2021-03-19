using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class AddressRepository : BaseReadWriteRepository<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "A")
        {

        }

        public async Task<IEnumerable<Address>> GetAddressesByPerson(Guid personId)
        {
            var query = GenerateQuery();

            query.LeftJoin("AddressPeople AS AP", "AP.AddressId", "A.Id");

            query.Where("AP.PersonId", personId);

            return await ExecuteQuery(query);
        }
    }
}    