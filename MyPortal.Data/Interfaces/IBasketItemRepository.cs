using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IBasketItemRepository : IReadWriteRepository<BasketItem>
    {
        Task<IEnumerable<BasketItem>> GetByStudent(int studentId);

        Task<decimal> GetTotalForStudent(int studentId);
    }
}
