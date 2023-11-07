using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IBasketItemRepository : IReadWriteRepository<BasketItem>, IUpdateRepository<BasketItem>
    {
    }
}