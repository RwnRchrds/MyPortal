using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AddressRepository : BaseReadWriteRepository<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        public async Task<IEnumerable<Address>> GetAddressesByPerson(Guid personId)
        {
            var query = GenerateQuery();

            query.LeftJoin("AddressPeople AS AP", "AP.AddressId", $"{TblAlias}.Id");

            query.Where("AP.PersonId", personId);

            return await ExecuteQuery(query);
        }

        private void ApplySearch(Query query, AddressSearchOptions searchOptions)
        {
            if (!string.IsNullOrWhiteSpace(searchOptions.Postcode))
            {
                query.Where($"{TblAlias}.Postcode", searchOptions.Postcode);
            }

            if (!string.IsNullOrWhiteSpace(searchOptions.BuildingNumber))
            {
                query.Where($"{TblAlias}.BuildingNumber", searchOptions.BuildingNumber);
            }

            if (!string.IsNullOrWhiteSpace(searchOptions.Street))
            {
                query.Where($"{TblAlias}.Street", searchOptions.Street);
            }

            if (!string.IsNullOrWhiteSpace(searchOptions.Town))
            {
                query.Where($"{TblAlias}.Town", searchOptions.Town);
            }
        }

        public async Task<IEnumerable<Address>> GetAll(AddressSearchOptions searchOptions)
        {
            var query = GenerateQuery();
            
            ApplySearch(query, searchOptions);

            return await ExecuteQuery(query);
        }

        public async Task Update(Address entity)
        {
            var address = await Context.Addresses.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (address == null)
            {
                throw new EntityNotFoundException("Address not found.");
            }

            address.BuildingNumber = entity.BuildingNumber;
            address.BuildingName = entity.BuildingName;
            address.Apartment = entity.Apartment;
            address.Street = entity.Street;
            address.District = entity.District;
            address.Town = entity.Town;
            address.County = entity.County;
            address.Postcode = entity.Postcode;
            address.Country = entity.Country;
            address.Validated = entity.Validated;
        }
    }
}    