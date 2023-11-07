using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories;

public interface IAddressAgencyRepository : IReadWriteRepository<AddressAgency>, IUpdateRepository<AddressAgency>
{
    Task<IEnumerable<AddressAgency>> GetByAgency(Guid agencyId);
    Task<IEnumerable<AddressAgency>> GetByAddress(Guid addressId);
}