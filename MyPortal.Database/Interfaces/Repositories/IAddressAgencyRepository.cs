using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories;

public interface IAddressAgencyRepository : IReadWriteRepository<AddressAgency>, IUpdateRepository<AddressAgency>
{
    
}