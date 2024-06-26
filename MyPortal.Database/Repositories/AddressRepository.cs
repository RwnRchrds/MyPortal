﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AddressRepository : BaseReadWriteRepository<Address>, IAddressRepository
    {
        public AddressRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task<IEnumerable<Address>> GetAddressesByPerson(Guid personId)
        {
            var query = GetDefaultQuery();

            query.LeftJoin("AddressPeople AS AP", "AP.AddressId", $"{TblAlias}.Id");

            query.Where("AP.PersonId", personId);

            return await ExecuteQuery(query);
        }

        private void ApplySearch(Query query, AddressSearchOptions searchOptions)
        {
            if (!string.IsNullOrWhiteSpace(searchOptions.Apartment))
            {
                query.WhereStarts($"{TblAlias}.Apartment", searchOptions.Apartment);
            }

            if (!string.IsNullOrWhiteSpace(searchOptions.BuildingName))
            {
                query.WhereStarts($"{TblAlias}.BuildingName", searchOptions.BuildingName);
            }

            if (!string.IsNullOrWhiteSpace(searchOptions.BuildingNumber))
            {
                query.WhereStarts($"{TblAlias}.BuildingNumber", searchOptions.BuildingNumber);
            }

            if (!string.IsNullOrWhiteSpace(searchOptions.Street))
            {
                query.WhereStarts($"{TblAlias}.Street", searchOptions.Street);
            }

            if (!string.IsNullOrWhiteSpace(searchOptions.District))
            {
                query.WhereStarts($"{TblAlias}.District", searchOptions.District);
            }

            if (!string.IsNullOrWhiteSpace(searchOptions.Town))
            {
                query.WhereStarts($"{TblAlias}.Town", searchOptions.Town);
            }

            if (!string.IsNullOrWhiteSpace(searchOptions.County))
            {
                query.WhereStarts($"{TblAlias}.County", searchOptions.County);
            }

            if (!string.IsNullOrWhiteSpace(searchOptions.Country))
            {
                query.WhereStarts($"{TblAlias}.Country", searchOptions.Country);
            }

            if (!string.IsNullOrWhiteSpace(searchOptions.Postcode))
            {
                query.WhereStarts($"{TblAlias}.Postcode", searchOptions.Postcode);
            }
        }

        public async Task<IEnumerable<Address>> GetAll(AddressSearchOptions searchOptions)
        {
            var query = GetDefaultQuery();

            ApplySearch(query, searchOptions);

            return await ExecuteQuery(query);
        }

        public async Task Update(Address entity)
        {
            var address = await DbUser.Context.Addresses.FirstOrDefaultAsync(x => x.Id == entity.Id);

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