using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IDiscountRepository : IReadWriteRepository<Discount>, IUpdateRepository<Discount>
    {
    }
}