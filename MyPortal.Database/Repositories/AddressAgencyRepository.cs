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

namespace MyPortal.Database.Repositories;

internal class AddressAgencyRepository : BaseReadWriteRepository<AddressAgency>, IAddressAgencyRepository
{
    public AddressAgencyRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
    {
    }

    protected override Query JoinRelated(Query query)
    {
        query.LeftJoin("Addresses as A", "A.Id", $"{TblAlias}.AddressId");
        query.LeftJoin("Agencies as AG", "AG.Id", $"{TblAlias}.AgencyId");

        return query;
    }

    protected override Query SelectAllRelated(Query query)
    {
        query.SelectAllColumns(typeof(Address), "A");
        query.SelectAllColumns(typeof(Agency), "AG");

        return query;
    }

    protected override async Task<IEnumerable<AddressAgency>> ExecuteQuery(Query query)
    {
        var sql = Compiler.Compile(query);

        var addressPeople = await Transaction.Connection.QueryAsync<AddressAgency, Address, Agency, AddressAgency>(
            sql.Sql,
            (addressAgency, address, agency) =>
            {
                addressAgency.Address = address;
                addressAgency.Agency = agency;

                return addressAgency;
            }, sql.NamedBindings, Transaction);

        return addressPeople;
    }

    public async Task Update(AddressAgency entity)
    {
        var addressPerson = await Context.AddressAgencies.FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (addressPerson == null)
        {
            throw new EntityNotFoundException("Address person not found.");
        }

        addressPerson.AddressId = entity.AddressId;
        addressPerson.AddressTypeId = entity.AddressTypeId;
        addressPerson.Main = entity.Main;
    }

    public async Task<IEnumerable<AddressAgency>> GetByAgency(Guid agencyId)
    {
        var query = GenerateQuery();

        query.Where("AG.Id", agencyId);

        return await ExecuteQuery(query);
    }

    public async Task<IEnumerable<AddressAgency>> GetByAddress(Guid addressId)
    {
        var query = GenerateQuery();

        query.Where("A.Id", addressId);

        return await ExecuteQuery(query);
    }
}